using MsgPack.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;
using System.Runtime.InteropServices.WindowsRuntime;
#if WINDOWS_UWP
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
#endif

[RequireComponent(typeof(TargetManipulationManager))]
public class BluetoothManager : MonoBehaviour
{
#if WINDOWS_UWP
    private RfcommServiceProvider rfcommProvider;
    private StreamSocket socket;
    private DataWriter dataWriter;
#endif

    private MainManager mainManager;

    const uint SERVICE_VERSION_ATTRIBUTE_ID = 0x0300;
    const byte SERVICE_VERSION_ATTRIBUTE_TYPE = 0x0A;
    const uint SERVICE_VERSION = 200;

    private uint testDataSize;

    private bool bluetoothServerRunning = false;
    private BluetoothLatencyTest latencyTest;

    public event EventHandler<EventArgs> BluetoothConnectionEstablished;

    private void Awake()
    {
        mainManager = GetComponent<MainManager>();
        //latencyTest = FindObjectOfType<BluetoothLatencyTest>();
    }

    private void Start()
    {
        
    }

    public void SendMessageToSmartphone(byte[] messageData)
    {
#if WINDOWS_UWP
        SendData(messageData);
#endif
    }

#if WINDOWS_UWP
    private async void SendData(byte[] data)
    {
        dataWriter.WriteBytes(data);
        await dataWriter.StoreAsync();
        await dataWriter.FlushAsync();
    }
#endif

    public void ToggleBluetoothServer()
    {
#if WINDOWS_UWP
        bluetoothServerRunning = !bluetoothServerRunning;

        if (bluetoothServerRunning)
        {
            StartBluetoothServer();
        }
        else
        {
            StopBluetoothServer();
        }
#endif
    }

    private void StartBluetoothServer()
    {
#if WINDOWS_UWP
        Initialize();
#endif
    }

    private void StopBluetoothServer()
    {
#if WINDOWS_UWP
        CancelStreamAndDisconnectSocket();
#endif
    }

    private void CancelStreamAndDisconnectSocket()
    {
#if WINDOWS_UWP
        socket?.Dispose();
        socket = null;
#endif
    }

#if WINDOWS_UWP
    private async void Initialize()
    {
        // Initialize the provider for the hosted RFCOMM service
        var guid = new Guid();
        TryParseGuid("31216cfa-2762-4a36-9a9a-0925380a7bd6", out guid);
        rfcommProvider = await RfcommServiceProvider.CreateAsync(RfcommServiceId.FromUuid(guid));

        // Create a listener for this service and start listening
        StreamSocketListener listener = new StreamSocketListener();
        listener.ConnectionReceived += OnConnectionReceived;
        await listener.BindServiceNameAsync(
            rfcommProvider.ServiceId.AsString(),
            SocketProtectionLevel
                .BluetoothEncryptionAllowNullAuthentication);

        // Set the SDP attributes and start advertising
        InitializeServiceSdpAttributes(rfcommProvider);

        try
        {
            rfcommProvider.StartAdvertising(listener);
        }
        catch (Exception exception)
        {
            PopupText.Instance.ShowText(exception.Message, 3.0f);
            rfcommProvider.StopAdvertising();
        }
        
        Debug.Log("Started listening for connection");
    }
#endif

#if WINDOWS_UWP
    private void InitializeServiceSdpAttributes(RfcommServiceProvider provider)
    {
        Windows.Storage.Streams.DataWriter writer = new Windows.Storage.Streams.DataWriter();

        // First write the attribute type
        writer.WriteByte(SERVICE_VERSION_ATTRIBUTE_TYPE);
        // Then write the data
        writer.WriteUInt32(SERVICE_VERSION);

        Windows.Storage.Streams.IBuffer data = writer.DetachBuffer();
        provider.SdpRawAttributes.Add(SERVICE_VERSION_ATTRIBUTE_ID, data);
    }
#endif

#if WINDOWS_UWP
    private void OnConnectionReceived(StreamSocketListener listener, StreamSocketListenerConnectionReceivedEventArgs args)
    {
        // Stop advertising/listening so that we're only serving one client
        rfcommProvider.StopAdvertising();
        listener.Dispose();
        socket = args.Socket;

        var reader = new DataReader(socket.InputStream);
        dataWriter = new DataWriter(socket.OutputStream);
        reader.InputStreamOptions = InputStreamOptions.None;
        ReadSocketInputStreamLoop(reader);

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            BluetoothConnectionEstablished?.Invoke(this, null);
        });
    }
#endif

#if WINDOWS_UWP
    private async void ReadSocketInputStreamLoop(IInputStream inputStream)
    {
        try
        {
            //var dataBuffer = new byte[testDataSize];
            //await inputStream.ReadAsync(dataBuffer.AsBuffer(), testDataSize, InputStreamOptions.Partial);

            //try
            //{
            //    var receivedData = ObjectManipulation.Parser.ParseFrom(dataBuffer);
            //    UnityMainThreadDispatcher.Instance().Enqueue(() =>
            //    {
            //        manipulationManager.HandleManipulationInput(receivedData);

            //    });
            //}
            //catch (Exception ex)
            //{
            //    Debug.Log("MessagePackSerializer Parse failed with error: " + ex.Message);
            //}

            var dataBuffer = new byte[1];
            await inputStream.ReadAsync(dataBuffer.AsBuffer(), testDataSize, InputStreamOptions.None);

            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                latencyTest.CountUp();
            });

            ReadSocketInputStreamLoop(inputStream);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
#endif

#if WINDOWS_UWP
    private async void ReadSocketInputStreamLoop(DataReader dataReader)
    {
        try
        {
            while (true)
            {
                await dataReader.LoadAsync(sizeof(byte));
                byte messageLength = dataReader.ReadByte();

                var dataBuffer = new byte[messageLength];
                await dataReader.LoadAsync(messageLength);
                dataReader.ReadBytes(dataBuffer);

                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    mainManager.ProcessMessageFromAndroid(dataBuffer);
                });
            }
        }
        catch (Exception ex)
        {
            if (socket == null)
            {
                // the user closed the socket
                CancelStreamAndDisconnectSocket();
                Debug.Log("socket closed.");
            }
            else
            {
                Debug.Log("Read stream failed with error: " + ex.Message);
                socket.Dispose();
                socket = null;
            }
        }
    }
#endif

    public static bool TryParseGuid(string guidString, out Guid guid)
    {
        if (guidString == null) throw new ArgumentNullException("guidString");
        try
        {
            guid = new Guid(guidString);
            return true;
        }
        catch (FormatException e)
        {
            Debug.LogError(e.Message);
            guid = default(Guid);
            return false;
        }
    }
}
