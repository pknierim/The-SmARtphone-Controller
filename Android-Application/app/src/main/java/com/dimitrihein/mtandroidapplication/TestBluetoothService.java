package com.dimitrihein.mtandroidapplication;

import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothSocket;
import android.content.Context;
import android.os.Handler;
import android.os.Message;
import android.util.Log;

import com.google.protobuf.HoloLensAndroidMessaging;
import com.google.protobuf.HoloLensAndroidMessaging.HoloLensMessage;
import com.google.protobuf.InvalidProtocolBufferException;

import java.io.IOException;
import java.io.InputStream;

import java.io.OutputStream;
import java.util.Arrays;
import java.util.Set;

public class TestBluetoothService {
    private static final String TAG = "BLUETOOTH_D";
    private Handler bluetoothMessageHandler;
    private Handler hololensMessageHandler;
    private ConnectedThread connectedThread;
    private Context context;
    private BluetoothAdapter bluetoothAdapter;
    private BluetoothDevice bluetoothDevice;
    private ConnectThread connectThread;


    public interface BluetoothMessageConstants {
        int USELESS_CONSTANT_NEVER_USE = 0;
        int CONNECTED_TO_REMOTE = 1;
        int DISCONNECTED_FROM_REMOTE = 2;
        int CONNECTION_FAILED = 3;
        int REMOTE_DEVICE_NOT_FOUND = 4;
    }

    TestBluetoothService(Context context, Handler bluetoothMessageHandler, Handler hololensMessageHandler) {
        this.bluetoothMessageHandler = bluetoothMessageHandler;
        this.hololensMessageHandler = hololensMessageHandler;
        this.context = context;
        this.bluetoothAdapter = BluetoothAdapter.getDefaultAdapter();
    }

    void ConnectSocket(BluetoothSocket socket) {
        connectedThread = new ConnectedThread(socket);
        connectedThread.start();
        Message connectionSuccessfulMessage = bluetoothMessageHandler.obtainMessage(BluetoothMessageConstants.CONNECTED_TO_REMOTE);
        bluetoothMessageHandler.sendMessage(connectionSuccessfulMessage);
    }

    void SendMessage(byte[] data) {
        if (connectedThread != null) {
            connectedThread.write(data);
            //Log.d(TAG, "Message of size " + data.length + " sent");
        } else {
            Log.d(TAG, "Connected Thread not initialised");
        }
    }

    void ConnectToRemoteDevice() {
        Set<BluetoothDevice> pairedDevices = bluetoothAdapter.getBondedDevices();

        if (pairedDevices.size() > 0) {
            // There are paired devices. Get the name and address of each paired device.
            for (BluetoothDevice device : pairedDevices) {
                String deviceName = device.getName();

                if (deviceName.equals("HOLOLENS-02")) {
                    bluetoothDevice = device;
                    connectThread = new ConnectThread(device, bluetoothAdapter, this, this.bluetoothMessageHandler);
                    connectThread.start();
                    Log.d("BLUETOOTH", "FOUND");
                    return;
                }
            }
        }

        Message message = bluetoothMessageHandler.obtainMessage(BluetoothMessageConstants.REMOTE_DEVICE_NOT_FOUND);
        bluetoothMessageHandler.sendMessage(message);
    }

    private class ConnectedThread extends Thread {
        private final BluetoothSocket mmSocket;
        private final InputStream mmInStream;
        private final OutputStream mmOutStream;
        private byte[] mmBuffer; // mmBuffer store for the stream

        ConnectedThread(BluetoothSocket socket) {
            mmSocket = socket;
            InputStream tmpIn = null;
            OutputStream tmpOut = null;

            // Get the input and output streams; using temp objects because
            // member streams are final.
            try {
                tmpIn = socket.getInputStream();
            } catch (IOException e) {
                Log.e(TAG, "Error occurred when creating input stream", e);
            }
            try {
                tmpOut = socket.getOutputStream();
            } catch (IOException e) {
                Log.e(TAG, "Error occurred when creating output stream", e);
            }

            mmInStream = tmpIn;
            mmOutStream = tmpOut;
        }

        public void run() {

            mmBuffer = new byte[2];

            while (true) {
                try {
                    mmInStream.read(mmBuffer);

                    try {
                        HoloLensMessage messageData = HoloLensMessage.parseFrom(mmBuffer);
                        Message holoLensMessage = new Message();
                        holoLensMessage.what = messageData.getMessage().getNumber();
                        hololensMessageHandler.sendMessage(holoLensMessage);
                    }
                    catch (InvalidProtocolBufferException pe) {
                        Log.d(TAG, "Failed to parse received hololens message", pe);
                        break;
                    }

                } catch (IOException e) {
                    Log.d(TAG, "Input stream was disconnected", e);
                    break;
                }
            }
        }

        // Call this from the main activity to send data to the remote device.
        void write(byte[] bytes) {
            try {
                if (bytes.length == 0) {
                    return;
                }

                byte[] dataWithSize = new byte[bytes.length + 1];
                dataWithSize[0] = (byte)bytes.length;
                System.arraycopy(bytes, 0 , dataWithSize, 1, bytes.length);
                mmOutStream.write(dataWithSize);
                mmOutStream.flush();
            } catch (IOException e) {
                Log.e(TAG, "Error occurred when sending data", e);
                Message connectionSuccessfulMessage = bluetoothMessageHandler.obtainMessage(BluetoothMessageConstants.DISCONNECTED_FROM_REMOTE);
                bluetoothMessageHandler.sendMessage(connectionSuccessfulMessage);
                cancel();
            }
        }

        // Call this method from the main activity to shut down the connection.
        void cancel() {
            try {
                mmSocket.close();
            } catch (IOException e) {
                Log.e(TAG, "Could not close the connect socket", e);
            }
        }
    }
}

