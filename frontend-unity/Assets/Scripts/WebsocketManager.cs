using System;
using System.IO;
using System.Threading.Tasks;
using NativeWebSocket;
using UnityEngine;

public class HeadlessWebSocketClient : MonoBehaviour
{
    [Header("Server Connection")]
    public string ServerUrl = "ws://localhost:8181";
    public bool AutoReconnect = true;
    public float InitialRetryDelaySeconds = 1f;
    public float MaxRetryDelaySeconds = 10f;

    [Header("Simulation Receivers")]
    [Tooltip("Target transforms in the same order as the server's SpheresToSync list")]
    public Transform[] SpheresToReceive;

    private WebSocket _webSocket;
    private bool _isQuitting;
    private bool _isConnecting;
    
    // ############################################################################################
    // ############################################################################################
    // ############################################################################################
    
    [Header("Smoothing")]
    public float SmoothSpeed = 15f;
        
    private Vector3[] _targetPositions;
    
    private void Awake()
    {
        _targetPositions = new Vector3[SpheresToReceive.Length];
    }

    private void Smooth()
    {
        // Smoothly interpolate towards target positions every render frame
        for (int i = 0; i < SpheresToReceive.Length; i++)
        {
            if (SpheresToReceive[i] != null)
            {
                SpheresToReceive[i].position = Vector3.Lerp(
                    SpheresToReceive[i].position, 
                    _targetPositions[i], 
                    Time.deltaTime * SmoothSpeed
                );
            }
        } 
    }
    
    // ############################################################################################
    // ############################################################################################
    // ############################################################################################
    
    private void Start()
    {
        // Start connection lifecycle loop
        _ = ConnectWithRetryAsync();
    }

    private void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        // Drive event queue dispatch for standalone builds / Editor
        _webSocket?.DispatchMessageQueue();
#endif
        
        Smooth();
    }

    // ############################################################################################
    // ############################################################################################
    // ############################################################################################
    
    private async Task ConnectWithRetryAsync()
    {
        if (_isConnecting || _isQuitting) return;
        _isConnecting = true;

        float currentDelay = InitialRetryDelaySeconds;

        while (!_isQuitting)
        {
            // Reset existing instance if re-entering loop
            if (_webSocket != null)
            {
                _webSocket.OnOpen -= OnConnected;
                _webSocket.OnMessage -= DeserializeAndApplyPositions;
                _webSocket.OnError -= OnError;
                _webSocket.OnClose -= OnConnectionClosed;
            }

            _webSocket = new WebSocket(ServerUrl);

            // Bind existing event logic
            _webSocket.OnOpen += OnConnected;
            _webSocket.OnMessage += DeserializeAndApplyPositions;
            _webSocket.OnError += OnError;
            _webSocket.OnClose += OnConnectionClosed;

            Debug.Log($"[Client] Attempting to connect to {ServerUrl}...");

            try
            {
                await _webSocket.Connect();

                // Check if connection succeeded
                if (_webSocket.State == WebSocketState.Open)
                {
                    _isConnecting = false;
                    return; // Successfully connected! The OnClose callback will handle reconnects if lost later.
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[Client] Connection failed: {ex.Message}");
            }

            if (!AutoReconnect) break;

            Debug.Log($"[Client] Retrying connection in {currentDelay}s...");
            await Task.Delay(TimeSpan.FromSeconds(currentDelay));

            // Exponential backoff up to max delay
            currentDelay = Mathf.Min(currentDelay * 1.5f, MaxRetryDelaySeconds);
        }

        _isConnecting = false;
    }

    private void OnConnected()
    {
        Debug.Log("[Client] Connected to WebSocket server.");
    }

    private void OnError(string errorMsg)
    {
        Debug.LogError($"[Client] Error: {errorMsg}");
    }

    private void OnConnectionClosed(WebSocketCloseCode closeCode)
    {
        Debug.LogWarning($"[Client] Connection closed (Code: {closeCode}).");

        if (AutoReconnect && !_isQuitting)
        {
            _ = ConnectWithRetryAsync();
        }
    }

    // ############################################################################################
    // ############################################################################################
    // ############################################################################################
    
    // Existing Deserialization & Position Logic (Unchanged)
    private void DeserializeAndApplyPositions(byte[] data)
    {
        if (data == null || data.Length < 4) return;

        using (MemoryStream ms = new MemoryStream(data))
        using (BinaryReader reader = new BinaryReader(ms))
        {
            byte magic = reader.ReadByte();
            if (magic != 0xAA) return;

            byte version = reader.ReadByte();
            if (version != 1) return;

            ushort sphereCount = reader.ReadUInt16();

            // for (int i = 0; i < sphereCount; i++)
            // {
            //     if (ms.Position + 16 > ms.Length) break;
            //
            //     int sphereId = reader.ReadInt32();
            //     float x = reader.ReadSingle();
            //     float y = reader.ReadSingle();
            //     float z = reader.ReadSingle();
            //
            //     if (sphereId >= 0 && sphereId < SpheresToReceive.Length)
            //     {
            //         if (SpheresToReceive[sphereId] != null)
            //         {
            //             SpheresToReceive[sphereId].position = new Vector3(x, y, z);
            //         }
            //     }
            // }
            
            for (int i = 0; i < sphereCount; i++)
            {
                int sphereId = reader.ReadInt32();
                float x = reader.ReadSingle();
                float y = reader.ReadSingle();
                float z = reader.ReadSingle();

                if (sphereId >= 0 && sphereId < _targetPositions.Length)
                {
                    // Update the target array instead of instantly moving the transform
                    _targetPositions[sphereId] = new Vector3(x, y, z);
                }
            }
            
        }
    }
    
    // ############################################################################################
    // ############################################################################################
    // ############################################################################################

    private async void OnDestroy()
    {
        _isQuitting = true;
        if (_webSocket != null)
        {
            _webSocket.OnClose -= OnConnectionClosed;
            await _webSocket.Close();
        }
    }

    private void OnApplicationQuit()
    {
        _isQuitting = true;
    }
}