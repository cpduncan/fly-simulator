using System.Runtime.InteropServices;

// Pack = 1 ensures exact byte alignment without compiler padding (16 bytes total)
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SocketPacketStruct
{
    public int SphereId; // 4 bytes
    public float PositionX; // 4 bytes
    public float PositionY; // 4 bytes
    public float PositionZ; // 4 bytes
}