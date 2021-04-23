package com.dimitrihein.mtandroidapplication;


import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.GestureDetector;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.ScaleGestureDetector;
import android.view.View;
import android.view.ViewGroup;

import com.google.protobuf.HoloLensAndroidMessaging;

import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;


public class UIManipulationFragment extends Fragment {
    private TestBluetoothService bluetoothService;
    private ScheduledExecutorService scheduleTaskExecutor;

    private float previousX;
    private float previousY;

    private float currentX;
    private float currentY;

    private boolean touching = false;
    private boolean doubleTappedMode = false;


    private GestureDetector gestureDetector;

    public UIManipulationFragment() {

    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        this.bluetoothService = ((MainActivity)getActivity()).GetBluetoothService();
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_uimanipulation, container, false);
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        getActivity().findViewById(R.id.finishButton).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                HoloLensAndroidMessaging.UIManipulation uiManipulation = HoloLensAndroidMessaging.UIManipulation.newBuilder()
                        .setIsFinished(1)
                        .build();

                byte[] byteData = uiManipulation.toByteArray();
                bluetoothService.SendMessage(byteData);


            }
        });

        setupTouchListener();
        //startSendingInput();
    }

    private void setupTouchListener() {

        gestureDetector = new GestureDetector(getActivity(), new GestureDetector.SimpleOnGestureListener() {
            @Override
            public boolean onDoubleTap(MotionEvent e) {
                doubleTappedMode = !doubleTappedMode;
                HoloLensAndroidMessaging.UIManipulation uiManipulation = HoloLensAndroidMessaging.UIManipulation.newBuilder()
                        .setIsSelecting(1)
                        .setHasAndroidUIInput(0)
                        .build();

                byte[] byteData = uiManipulation.toByteArray();
                bluetoothService.SendMessage(byteData);

                previousX = e.getX();
                previousY = e.getY();
                currentX = previousX;
                currentY = previousY;

                return true;
            }
        });

        final View touchView = getActivity().findViewById(R.id.frame);
        touchView.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                int eventAction = event.getAction();
                gestureDetector.onTouchEvent(event);

                switch (eventAction) {
                    case MotionEvent.ACTION_DOWN:
                        touching  = true;
                        previousX = event.getX();
                        previousY = event.getY();
                        currentX = previousX;
                        currentY = previousY;
                        break;
                    case MotionEvent.ACTION_MOVE:
                        currentX = event.getX();
                        currentY = event.getY();

                        HoloLensAndroidMessaging.UIManipulation uiManipulation;

                        float translationX = (currentX - previousX) * 0.14f;
                        float translationY = (currentY - previousY) * 0.14f;

                        previousX = currentX;
                        previousY = currentY;

                        uiManipulation = HoloLensAndroidMessaging.UIManipulation.newBuilder()
                                .setScrollX(translationX)
                                .setScrollY(translationY)
                                .build();

                        byte[] byteData = uiManipulation.toByteArray();
                        bluetoothService.SendMessage(byteData);

                        break;
                    case MotionEvent.ACTION_UP:
                        touching  = false;
                        break;
                }

                return true;
            }
        });
    }

    private void startSendingInput() {
        if (scheduleTaskExecutor != null) {
            scheduleTaskExecutor.shutdownNow();
        }

        scheduleTaskExecutor = Executors.newScheduledThreadPool(5);

        scheduleTaskExecutor.scheduleAtFixedRate(new Runnable() {
            public void run() {

                if (touching) {
                    HoloLensAndroidMessaging.UIManipulation uiManipulation;

                    float translationX = (currentX - previousX) * 0.14f;
                    float translationY = (currentY - previousY) * 0.14f;

                    previousX = currentX;
                    previousY = currentY;

                    uiManipulation = HoloLensAndroidMessaging.UIManipulation.newBuilder()
                            .setScrollX(translationX)
                            .setScrollY(translationY)
                            .build();

                    byte[] byteData = uiManipulation.toByteArray();
                    bluetoothService.SendMessage(byteData);
                }
            }
        }, 0, 32, TimeUnit.MILLISECONDS);
    }
}
