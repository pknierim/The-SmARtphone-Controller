package com.dimitrihein.mtandroidapplication;

import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothSocket;
import android.os.Handler;
import android.os.Message;
import android.util.Log;

import java.io.IOException;
import java.util.UUID;

public class ConnectThread extends Thread {
    private final BluetoothSocket bluetoothSocket;
    private final BluetoothDevice mmDevice;
    private BluetoothAdapter bluetoothAdapter;
    private TestBluetoothService bluetoothService;
    private Handler handler;

    public ConnectThread(BluetoothDevice device, BluetoothAdapter adapter, TestBluetoothService bluetoothService, Handler handler) {
        // Use a temporary object that is later assigned to bluetoothSocket
        // because bluetoothSocket is final.
        BluetoothSocket tmp = null;
        mmDevice = device;
        this.handler = handler;

        try {
            // Get a BluetoothSocket to connect with the given BluetoothDevice.
            tmp = device.createRfcommSocketToServiceRecord(UUID.fromString("31216cfa-2762-4a36-9a9a-0925380a7bd6"));
        } catch (IOException e) {
            Log.e("BLUETOOTH", "Socket's create() method failed", e);
        }
        bluetoothSocket = tmp;
        bluetoothAdapter = adapter;
        this.bluetoothService = bluetoothService;
    }

    public void run() {
        // Cancel discovery because it otherwise slows down the connection.
        bluetoothAdapter.cancelDiscovery();

        try {
            // Connect to the remote device through the socket. This call blocks
            // until it succeeds or throws an exception.
            bluetoothSocket.connect();
        } catch (IOException connectException) {
            // Unable to connect; close the socket and return.
            try {
                bluetoothSocket.close();
            } catch (IOException closeException) {
                Log.e("BLUETOOTH", "Could not close the client socket", closeException);
            }
            Log.e("BLUETOOTH", connectException.getMessage());
            Message connectionSuccessfulMessage = handler.obtainMessage(TestBluetoothService.BluetoothMessageConstants.CONNECTION_FAILED);
            handler.sendMessage(connectionSuccessfulMessage);
            return;
        }

        // The connection attempt succeeded. Perform work associated with
        // the connection in a separate thread.
        Log.d("BLUETOOTH", "Connection Successful");

        bluetoothService.ConnectSocket(bluetoothSocket);
    }

    // Closes the client socket and causes the thread to finish.
    public void cancel() {
        try {
            bluetoothSocket.close();
        } catch (IOException e) {
            Log.e("BLUETOOTH", "Could not close the client socket", e);
        }
    }
}
