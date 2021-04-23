package com.dimitrihein.mtandroidapplication;

import android.app.Activity;
import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.SeekBar;
import android.widget.Spinner;
import android.widget.TextView;

import com.google.protobuf.HoloLensAndroidMessaging;

public class UIManipulationAndroidUIFragment extends Fragment implements View.OnClickListener{
    private TestBluetoothService bluetoothService;

    private int currentObjectType = 0;
    private int currentQuality = 0;
    private int currentScale = 50;
    private int currentColor = 0;
    private Spinner dropdownSpinner;
    private RadioButton radioButtonHigh;
    private RadioButton radioButtonMiddle;
    private RadioButton radioButtonLow;
    private SeekBar scaleSeekbar;
    private boolean firstInputReceived = false;
    private boolean ignorechangesForBluetooth = true;
    private boolean dropdownSpinnerSelectionWasReset = true;

    private Activity activity;

    public UIManipulationAndroidUIFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        this.bluetoothService = ((MainActivity)activity).GetBluetoothService();
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_uimanipulation_android_ui, container, false);
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
        activity = getActivity();
        Log.d("LEL", "LEL");
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        dropdownSpinner = activity.findViewById(R.id.objectDropdown);
        radioButtonHigh = activity.findViewById(R.id.radioButtonHigh);
        radioButtonMiddle = activity.findViewById(R.id.radioButtonMiddle);
        radioButtonLow = activity.findViewById(R.id.radioButtonLow);
        scaleSeekbar = activity.findViewById(R.id.scaleSeekbar);

        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(activity,
                R.array.dropdownValues, android.R.layout.simple_spinner_item);

        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        dropdownSpinner.setAdapter(adapter);

        scaleSeekbar.setProgress(50);
        scaleSeekbar.refreshDrawableState();
        ((Button)activity.findViewById(R.id.finishButton)).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                HoloLensAndroidMessaging.UIManipulation uiManipulation = HoloLensAndroidMessaging.UIManipulation.newBuilder()
                        .setIsFinished(1)
                        .setHasAndroidUIInput(1)
                        .build();

                byte[] byteData = uiManipulation.toByteArray();
                bluetoothService.SendMessage(byteData);
            }
        });

        ((RadioGroup)activity.findViewById(R.id.qualityRadioGroup)).check(R.id.radioButtonHigh);

        //setupFirstInputReceivedClickHandler();
        setupTouchListeners();

        ignorechangesForBluetooth = false;
        Log.d("TEST", "ignorechangesForBluetooth is now false");
    }

    private void setupTouchListeners() {
        ((Spinner)activity.findViewById(R.id.objectDropdown)).setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                currentObjectType = position;
                //Log.d("ANDROID_UI", currentObjectType + "");
                if (!dropdownSpinnerSelectionWasReset) {
                    Log.d("TEST", "dropdown send input on select");
                    sendUpdatedInput();
                }

                dropdownSpinnerSelectionWasReset = false;
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });

        ((Spinner)activity.findViewById(R.id.objectDropdown)).setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                    Log.d("TEST", "dropdown send input on touch");
                    sendUpdatedInput();
                    firstInputReceived = true;
                return false;
            }
        });

        ((RadioGroup)activity.findViewById(R.id.qualityRadioGroup)).setOnCheckedChangeListener(new RadioGroup.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(RadioGroup group, int checkedId) {
                RadioButton radioButton = ((RadioButton)activity.findViewById(checkedId));

                switch (radioButton.getId()) {
                    case R.id.radioButtonHigh:
                        currentQuality = 0;
                        break;
                    case R.id.radioButtonMiddle:
                        currentQuality = 1;
                        break;
                    case R.id.radioButtonLow:
                        currentQuality = 2;
                        break;
                }
                Log.d("TEST", "Quality send input");
                sendUpdatedInput();
            }
        });

        ((SeekBar)activity.findViewById(R.id.scaleSeekbar)).setOnSeekBarChangeListener(new SeekBar.OnSeekBarChangeListener() {
            @Override
            public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {
                currentScale = progress;
                ((TextView)activity.findViewById(R.id.scaleText)).setText(currentScale + "%");
                Log.d("TEST", "scale send input");
                sendUpdatedInput();
            }

            @Override
            public void onStartTrackingTouch(SeekBar seekBar) {

            }

            @Override
            public void onStopTrackingTouch(SeekBar seekBar) {

            }
        });

        activity.findViewById(R.id.color1).setOnClickListener(this);
        activity.findViewById(R.id.color2).setOnClickListener(this);
        activity.findViewById(R.id.color3).setOnClickListener(this);
        activity.findViewById(R.id.color4).setOnClickListener(this);
        activity.findViewById(R.id.color5).setOnClickListener(this);
        activity.findViewById(R.id.color6).setOnClickListener(this);
        activity.findViewById(R.id.color7).setOnClickListener(this);
        activity.findViewById(R.id.color8).setOnClickListener(this);
    }

    private void sendUpdatedInput() {

        if (ignorechangesForBluetooth)
        {
            return;
        }

        HoloLensAndroidMessaging.UIManipulation.AndroidUIInput input = HoloLensAndroidMessaging.UIManipulation.AndroidUIInput.newBuilder()
                .setColor(currentColor)
                .setQuality(currentQuality)
                .setObjectType(currentObjectType)
                .setScale(currentScale)
                .build();

        HoloLensAndroidMessaging.UIManipulation uiManipulation = HoloLensAndroidMessaging.UIManipulation.newBuilder()
                .setAndroidUIInput(input)
                .setHasAndroidUIInput(1)
                .build();

        byte[] byteData = uiManipulation.toByteArray();
        bluetoothService.SendMessage(byteData);

        Log.d("TEST", " sent " + uiManipulation.toString());
    }

    public void resetParameters() {

        Log.d("TEST", "RESET PARA");
        ignorechangesForBluetooth = true;
        dropdownSpinnerSelectionWasReset = true;
        dropdownSpinner.setSelection(0);
        radioButtonHigh.setChecked(true);
        radioButtonMiddle.setChecked(false);
        radioButtonLow.setChecked(false);
        scaleSeekbar.setProgress(50);
        scaleSeekbar.refreshDrawableState();
        currentColor = 0;
        firstInputReceived = false;
        ignorechangesForBluetooth = false;
        Log.d("TEST", "RESET PARA DONE");
    }

    @Override
    public void onClick(View v) {
        boolean colorClicked = true;
        switch (v.getId()) {

            case R.id.color1:
                currentColor = 0;
                break;
            case R.id.color2:
                currentColor = 1;
                break;
            case R.id.color3:
                currentColor = 2;
                break;
            case R.id.color4:
                currentColor = 3;
                break;
            case R.id.color5:
                currentColor = 4;
                break;
            case R.id.color6:
                currentColor = 5;
                break;
            case R.id.color7:
                currentColor = 6;
                break;
            case R.id.color8:
                currentColor = 7;
                break;

            default:
                colorClicked = false;
                break;
        }
        Log.d("TEST", "on click color " + colorClicked);
        if (colorClicked) {
            sendUpdatedInput();
        }
    }
}
