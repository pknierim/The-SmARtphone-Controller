package com.dimitrihein.mtandroidapplication;

import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.GestureDetector;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.ScaleGestureDetector;
import android.view.VelocityTracker;
import android.view.View;
import android.view.ViewGroup;
import android.widget.SeekBar;

import com.google.protobuf.HoloLensAndroidMessaging.ObjectManipulation;

import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import static android.content.Context.SENSOR_SERVICE;

public class ObjectManipulationFragment extends Fragment implements RotationGestureDetector.OnRotationGestureListener {
    private TestBluetoothService bluetoothService;
    private ScheduledExecutorService scheduleTaskExecutor;

    private float previousX;
    private float previousY;

    private float currentX;
    private float currentY;

    private boolean touching = false;
    private boolean touchedInFrame = false;

    private float currentScale = 1.0f;
    private float tempScaleAdded = 0.0f;

    private boolean isScaling = false;
    private boolean isRotating = false;

    private boolean decidingIfRotationOrScale = false;
    final float decideValueScale = 0.125f;
    final float decideValueRotation = 7.0f;
    private float tempScaleAddedForDecision = 0.0f;

    private ScaleGestureDetector scaleDetector;
    private GestureDetector gestureDetector;

    private boolean doubleTapHold = false;
    private int currentTouchCount = 0;

    private SensorManager sensorManager;
    private Sensor gyroSensor;

    private float currentRotationY = 0;
    private float previousAngle = 0;
    private boolean rotationModeActive = false;

    private byte debugCounter = 0;
    private byte maxDebugCounter = 101;
    VelocityTracker velocityTracker;

    private RotationGestureDetector rotationGestureDetector;

    public static ObjectManipulationFragment newInstance() {
        return new ObjectManipulationFragment();
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        this.bluetoothService = ((MainActivity)getActivity()).GetBluetoothService();
        sensorManager = (SensorManager)getActivity().getSystemService(SENSOR_SERVICE);
        gyroSensor = sensorManager.getDefaultSensor(Sensor.TYPE_ROTATION_VECTOR);
        rotationGestureDetector = new RotationGestureDetector(this);
        velocityTracker = VelocityTracker.obtain();
    }

    private void setupTouchListener() {

        gestureDetector = new GestureDetector(getActivity(), new GestureDetector.SimpleOnGestureListener() {
            @Override
            public boolean onDoubleTap(MotionEvent e) {
                if (!rotationModeActive) {
                    doubleTapHold = true;
                } else {
                    rotationModeActive = false;
                    setRotationModeActive(false);
                }

                return super.onDoubleTap(e);
            }
        });

        scaleDetector = new ScaleGestureDetector(getActivity(), new ScaleGestureDetector.OnScaleGestureListener() {
            @Override
            public void onScaleEnd(ScaleGestureDetector detector) {
                isScaling = false;
                currentScale = getClampedScale();
                tempScaleAdded = 0.0f;
                tempScaleAddedForDecision = 0.0f;
                decidingIfRotationOrScale = false;
            }
            @Override
            public boolean onScaleBegin(ScaleGestureDetector detector) {
                if (!doubleTapHold && !isRotating) {

                    decidingIfRotationOrScale = true;
                }
                return true;
            }
            @Override
            public boolean onScale(ScaleGestureDetector detector) {
                if (!doubleTapHold) {
                    if (decidingIfRotationOrScale && !isRotating) {
                        tempScaleAddedForDecision = detector.getScaleFactor() - 1.0f;

                        if (Math.abs(tempScaleAddedForDecision) >= decideValueScale) {
                            isScaling = true;
                            decidingIfRotationOrScale = false;
                        }
                    } else if (isScaling){
                        tempScaleAdded = detector.getScaleFactor() - 1.0f - tempScaleAddedForDecision;
                        Log.d("TEST_DEC", "SCALE: " + detector.getScaleFactor());
                    }

                }
                return false;
            }
        });

        final View touchView = getActivity().findViewById(R.id.frame);
        touchView.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                int eventAction = event.getAction();
                gestureDetector.onTouchEvent(event);
                scaleDetector.onTouchEvent(event);
                rotationGestureDetector.onTouchEvent(event);
                velocityTracker.addMovement(event);

                // end of scale or
                if (event.getPointerCount() == 1 && currentTouchCount == 2)
                {
                    currentX = previousX = event.getX();
                    currentY = previousY = event.getY();
                    currentTouchCount = event.getPointerCount();
                    return true;
                }

                // end of rotation
                if ((isRotating || isScaling) && event.getPointerCount() < 2) {
                    isRotating = false;
                    isScaling = false;
                    previousAngle = 0.0f;
                }

                currentTouchCount = event.getPointerCount();

                switch (eventAction) {
                    case MotionEvent.ACTION_DOWN:
                        touching = touchedInFrame = true;
                        previousX = currentX = event.getX();
                        previousY = currentY = event.getY();
                        break;
                    case MotionEvent.ACTION_MOVE:
                        currentX = event.getX();
                        currentY = event.getY();
                        break;
                    case MotionEvent.ACTION_UP:
                        touching = touchedInFrame = false;
                        doubleTapHold = false;
                        break;
                }

                return true;
            }
        });

        getActivity().findViewById(R.id.rotationView).setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                gestureDetector.onTouchEvent(event);
                return true;
            }
        });
    }

    private void setupRotationListener() {

        sensorManager.registerListener(new SensorEventListener() {
            @Override
            public void onSensorChanged(SensorEvent sensorEvent) {
                float[] rotationMatrix = new float[16];
                SensorManager.getRotationMatrixFromVector(
                        rotationMatrix, sensorEvent.values);

                // Remap coordinate system
                float[] remappedRotationMatrix = new float[16];
                SensorManager.remapCoordinateSystem(rotationMatrix,
                        SensorManager.AXIS_X,
                        SensorManager.AXIS_Z,
                        remappedRotationMatrix);

                // Convert to orientations
                float[] orientations = new float[3];
                SensorManager.getOrientation(rotationMatrix, orientations);

                orientations[0] = (float)(Math.toDegrees(orientations[0]));
                currentRotationY = orientations[0];
            }

            @Override
            public void onAccuracyChanged(Sensor sensor, int i) {

            }
        }, gyroSensor, SensorManager.SENSOR_DELAY_FASTEST);

//        getActivity().findViewById(R.id.rotationToggle).setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View view) {
//                rotationModeActive = !rotationModeActive;
//                setRotationModeActive(rotationModeActive);
//
//                // sent to notify about rotation stop on holo lens
//                if (!rotationModeActive) {
//                    ObjectManipulation objectManipulation = ObjectManipulation.newBuilder()
//                            .setRotationY(1000)
//                            .setScale(getClampedScale())
//                            .build();
//
//                    byte[] byteData = objectManipulation.toByteArray();
//                    bluetoothService.SendMessage(byteData);
//                }
//            }
//        });
    }

    private void startSendingTestInput() {
        if (scheduleTaskExecutor != null) {
            scheduleTaskExecutor.shutdownNow();
        }

        scheduleTaskExecutor = Executors.newScheduledThreadPool(5);

        scheduleTaskExecutor.scheduleAtFixedRate(new Runnable() {
            public void run() {
                byte[] byteData = new byte[1];
                byteData[0] = debugCounter;
                debugCounter = (byte) ((debugCounter + 1) % maxDebugCounter);
                bluetoothService.SendMessage(byteData);
            }
        }, 0, 16, TimeUnit.MILLISECONDS);
    }

    private void startSendingInput() {
        if (scheduleTaskExecutor != null) {
            scheduleTaskExecutor.shutdownNow();
        }

        scheduleTaskExecutor = Executors.newScheduledThreadPool(5);

        scheduleTaskExecutor.scheduleAtFixedRate(new Runnable() {
            public void run() {

                if (touching || rotationModeActive) {
                    ObjectManipulation objectManipulation = null;

                    if (isRotating) {
//                        objectManipulation = ObjectManipulation.newBuilder()
//                                .setRotationY(currentRotationY)
//                                .setScale(getClampedScale())
//                                .build();
                    }
                    else if (doubleTapHold) {
                        velocityTracker.computeCurrentVelocity(1000, 100);
                        float velocityFactorY = Math.abs(velocityTracker.getYVelocity() / 1000.0f);

                        float translationY = (currentY - previousY) * velocityFactorY;
                        previousY = currentY;
                        objectManipulation = ObjectManipulation.newBuilder()
                                .setTranslationY(translationY)
                                .setScale(getClampedScale())
                                .setRotationY(0)
                                .build();
                    }
                    else if (isScaling) {
                        objectManipulation = ObjectManipulation.newBuilder()
                                .setScale(getClampedScale())
                                .setRotationY(0)
                                .build();
                    }
                    else if (currentTouchCount == 1){

                        velocityTracker.computeCurrentVelocity(1000, 100);
                        float velocityFactorX = Math.abs(velocityTracker.getXVelocity() / 600.0f);
                        float velocityFactorY = Math.abs(velocityTracker.getYVelocity() / 600.0f);

                        float translationX = (float)(currentX - previousX) * velocityFactorX;
                        float translationY = (float)(currentY - previousY) * velocityFactorY;

                        boolean isFirstTouchInFrame = false;

                        if (translationX + translationY != 0) {
                            isFirstTouchInFrame = touchedInFrame;
                            touchedInFrame = false;
                        }

                        previousX = currentX;
                        previousY = currentY;

                        objectManipulation = ObjectManipulation.newBuilder()
                                .setTranslationX(translationX)
                                .setTranslationZ(translationY)
                                .setScale(getClampedScale())
                                .setIsFirstTouch(isFirstTouchInFrame ? 1 : 0)
                                .setRotationY(0)
                                .build();

                    }

                    if (objectManipulation != null) {
                        byte[] byteData = objectManipulation.toByteArray();
                        bluetoothService.SendMessage(byteData);
                    }

                }
            }
        }, 0, 32, TimeUnit.MILLISECONDS);
    }

    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {
        return inflater.inflate(R.layout.object_manipulation_fragment, container, false);
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        getActivity().findViewById(R.id.selectButton).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                setSelectionActive(false);

                ObjectManipulation objectManipulation = ObjectManipulation.newBuilder()
                        .setRotationY(0)
                        .setScale(getClampedScale())
                        .setIsSelecting(1)
                        .build();

                byte[] byteData = objectManipulation.toByteArray();
                bluetoothService.SendMessage(byteData);
            }
        });

        getActivity().findViewById(R.id.finishButton).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ObjectManipulation objectManipulation = ObjectManipulation.newBuilder()
                        .setRotationY(0)
                        .setScale(getClampedScale())
                        .setIsFinished(1)
                        .build();

                byte[] byteData = objectManipulation.toByteArray();
                bluetoothService.SendMessage(byteData);
            }
        });

        setRotationModeActive(false);
        setSelectionActive(true);
        setupTouchListener();
        setupRotationListener();
        startSendingInput();
    }

    public void setSelectionActive(boolean value) {
        if (value) {
            getActivity().findViewById(R.id.manipulationContainer).setVisibility(View.GONE);
            getActivity().findViewById(R.id.selectButton).setVisibility(View.VISIBLE);
        } else {
            getActivity().findViewById(R.id.manipulationContainer).setVisibility(View.VISIBLE);
            getActivity().findViewById(R.id.selectButton).setVisibility(View.GONE);
        }
    }

    private void setRotationModeActive(boolean value) {
        if (value) {
            getActivity().findViewById(R.id.manipulationContainer).setVisibility(View.GONE);
            getActivity().findViewById(R.id.rotationView).setVisibility(View.VISIBLE);
        } else {
            getActivity().findViewById(R.id.manipulationContainer).setVisibility(View.VISIBLE);
            getActivity().findViewById(R.id.rotationView).setVisibility(View.GONE);
        }
    }

    private float getClampedScale() {
        float clamped = currentScale + tempScaleAdded;
        return clamp(clamped, 0.5f, 2.0f);
    }

    public static float clamp(float val, float min, float max) {
        return Math.max(min, Math.min(max, val));
    }

    public void resetScale() {
        currentScale = 1.0f;
        tempScaleAdded = 0.0f;
    }

    @Override
    public void OnRotation(RotationGestureDetector rotationDetector) {
        if (!decidingIfRotationOrScale && (!isRotating && !isScaling)) {
            decidingIfRotationOrScale = true;
        }
        else if (decidingIfRotationOrScale) {
            if (Math.abs(rotationDetector.getAngle()) >= decideValueRotation) {
                decidingIfRotationOrScale = false;
                isRotating = true;
                previousAngle = -rotationDetector.getAngle();
            }

        } else if (isRotating){
            float rotationDelta =  (-rotationDetector.getAngle()) - previousAngle;
            previousAngle = -rotationDetector.getAngle();
            ObjectManipulation objectManipulation = ObjectManipulation.newBuilder()
                    .setRotationY(rotationDelta)
                    .setScale(getClampedScale())
                    .build();

            byte[] byteData = objectManipulation.toByteArray();
            bluetoothService.SendMessage(byteData);
            Log.d("TEST_DEC","ROTATE: " + rotationDetector.getAngle());
        }
    }
}
