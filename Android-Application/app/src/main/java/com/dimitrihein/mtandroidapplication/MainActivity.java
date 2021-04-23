package com.dimitrihein.mtandroidapplication;


import android.annotation.SuppressLint;
import android.os.Build;
import android.os.VibrationEffect;
import android.os.Vibrator;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.content.Context;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.support.v4.app.Fragment;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import com.google.protobuf.HoloLensAndroidMessaging;

public class MainActivity extends AppCompatActivity {
    private TestBluetoothService bluetoothService;
    private BluetoothConnectionHandler bluetoothHandler;
    private HoloLensHandler holoLensHandler;
    private Button button;
    private Fragment currentFragment;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        bluetoothHandler = new BluetoothConnectionHandler(this);
        holoLensHandler = new HoloLensHandler(this);
        bluetoothService = new TestBluetoothService(this, bluetoothHandler, holoLensHandler);

        button = findViewById(R.id.connectButton);
        button.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                bluetoothService.ConnectToRemoteDevice();
                button.setEnabled(false);
            }
        });

        //startImmersiveMode();

        //button.setVisibility(View.GONE);
        //StartUIManipulationAndroidUIFragment();
    }

    private void startImmersiveMode() {

        final int flags = View.SYSTEM_UI_FLAG_LAYOUT_STABLE
                | View.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION
                | View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN
                | View.SYSTEM_UI_FLAG_HIDE_NAVIGATION
                | View.SYSTEM_UI_FLAG_FULLSCREEN
                | View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY;

        getWindow().getDecorView().setSystemUiVisibility(flags);

        // Code below is to handle presses of Volume up or Volume down.
        // Without this, after pressing volume buttons, the navigation bar will
        // show up and won't hide
        final View decorView = getWindow().getDecorView();
        decorView
                .setOnSystemUiVisibilityChangeListener(new View.OnSystemUiVisibilityChangeListener()
                {

                    @Override
                    public void onSystemUiVisibilityChange(int visibility)
                    {
                        if((visibility & View.SYSTEM_UI_FLAG_FULLSCREEN) == 0)
                        {
                            decorView.setSystemUiVisibility(flags);
                        }
                    }
                });
    }

    @SuppressLint("NewApi")
    @Override
    public void onWindowFocusChanged(boolean hasFocus)
    {
        super.onWindowFocusChanged(hasFocus);
        if(android.os.Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT && hasFocus)
        {
            getWindow().getDecorView().setSystemUiVisibility(
                    View.SYSTEM_UI_FLAG_LAYOUT_STABLE
                            | View.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION
                            | View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN
                            | View.SYSTEM_UI_FLAG_HIDE_NAVIGATION
                            | View.SYSTEM_UI_FLAG_FULLSCREEN
                            | View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY);
        }
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
    }

    public TestBluetoothService GetBluetoothService() {
        return this.bluetoothService;
    }

    private void StartObjectManipulationFragment() {
        Fragment fragment = new ObjectManipulationFragment();
        FragmentManager manager = getSupportFragmentManager();
        FragmentTransaction transaction = manager.beginTransaction();

        if (currentFragment != null) {
            transaction.remove(currentFragment);
        }
        transaction.add(R.id.main_container, fragment, "ObjectManipulation State");
        transaction.addToBackStack(null);
        transaction.commit();

        currentFragment = fragment;
    }

    private void StartUIManipulationAndroidUIFragment() {
        Fragment fragment = new UIManipulationAndroidUIFragment();
        FragmentManager manager = getSupportFragmentManager();
        FragmentTransaction transaction = manager.beginTransaction();

        if (currentFragment != null) {
            transaction.remove(currentFragment);
        }
        transaction.add(R.id.main_container, fragment, "NonImmersive UI State");
        transaction.commit();

        currentFragment = fragment;
    }

    private void StartClearFragment() {
        Fragment fragment = new ClearFragment();
        FragmentManager manager = getSupportFragmentManager();
        FragmentTransaction transaction = manager.beginTransaction();

        if (currentFragment != null) {
            transaction.remove(currentFragment);
        }
        transaction.add(R.id.main_container, fragment, "Clear State");
        transaction.addToBackStack(null);
        transaction.commit();

        currentFragment = fragment;
    }

    private void StartUIManipulationFragment() {
        Fragment fragment = new UIManipulationFragment();
        FragmentManager manager = getSupportFragmentManager();
        FragmentTransaction transaction = manager.beginTransaction();

        if (currentFragment != null) {
            transaction.remove(currentFragment);
        }
        transaction.add(R.id.main_container, fragment, "Immersive UI State");
        transaction.addToBackStack(null);
        transaction.commit();

        currentFragment = fragment;
    }

    private class BluetoothConnectionHandler extends Handler {
        private Context context;

        BluetoothConnectionHandler(Context context) {
            this.context = context;
        }

        @Override
        public void handleMessage(Message message) {
            switch (message.what) {
                case TestBluetoothService.BluetoothMessageConstants.CONNECTED_TO_REMOTE:
                    button.setVisibility(View.GONE);
                    Toast.makeText(context, "Connection successful",
                            Toast.LENGTH_SHORT).show();
                    break;
                case TestBluetoothService.BluetoothMessageConstants.DISCONNECTED_FROM_REMOTE:
                    FragmentManager manager = getSupportFragmentManager();
                    FragmentTransaction transaction = manager.beginTransaction();
                    transaction.remove(manager.findFragmentById(currentFragment.getId()));
                    break;
                case TestBluetoothService.BluetoothMessageConstants.CONNECTION_FAILED:
                    button.setEnabled(true);
                    Toast.makeText(context, "Connection failed",
                            Toast.LENGTH_SHORT).show();
                    break;
                case TestBluetoothService.BluetoothMessageConstants.REMOTE_DEVICE_NOT_FOUND:
                    button.setEnabled(true);
                    Toast.makeText(context, "HoloLens not found",
                            Toast.LENGTH_SHORT).show();
                    break;
            }
        }
    }

    private class HoloLensHandler extends Handler {
        private Context context;

        HoloLensHandler(Context context)  {
            this.context = context;
        }


        @Override
        public void handleMessage(Message message) {
            switch (HoloLensAndroidMessaging.HoloLensMessage.Message.forNumber(message.what))
            {
                case USELESS_CONSTANT_NEVER_USE:
                    break;
                case USE_CASE_OBJECT_TRANSLATION:
                    StartObjectManipulationFragment();
                    break;
                case USE_CASE_CLEAR:
                    StartClearFragment();
                    break;
                case USE_CASE_UI_MANIPULATION:
                    StartUIManipulationFragment();
                    break;
                case USE_CASE_UI_MANIPULATION_ANDROID_UI:
                    StartUIManipulationAndroidUIFragment();
                    break;
                case OBJECT_MANIPULATION_FINISHED:
                    if (currentFragment instanceof ObjectManipulationFragment) {
                        ((ObjectManipulationFragment)getSupportFragmentManager().findFragmentByTag("ObjectManipulation State")).setSelectionActive(true);
                        ((ObjectManipulationFragment)getSupportFragmentManager().findFragmentByTag("ObjectManipulation State")).resetScale();
                    }
                    break;
                case OBJECT_FOR_MANIPULATION_SELECTED:
                    if (currentFragment instanceof ObjectManipulationFragment) {
                        ((ObjectManipulationFragment)getSupportFragmentManager().findFragmentByTag("ObjectManipulation State")).setSelectionActive(false);
                        //((ObjectManipulationFragment)currentFragment).setSelectionActive(false);
                    }
                    break;
                case UI_MANIPULATION_CORRECT:
                    if (currentFragment instanceof UIManipulationAndroidUIFragment) {
                        ((UIManipulationAndroidUIFragment)getSupportFragmentManager().findFragmentByTag("NonImmersive UI State")).resetParameters();
                    }
                    break;
                case UI_MANIPULATION_FALSE:
                    Vibrator vibrator = (Vibrator) getSystemService(Context.VIBRATOR_SERVICE);
                    vibrator.vibrate(VibrationEffect.createOneShot(500, VibrationEffect.DEFAULT_AMPLITUDE));
                    break;
                case UNRECOGNIZED:
                    break;
            }
        }
    }
}
