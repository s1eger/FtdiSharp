namespace FtdiSharp
{
    public class FtdiSettings
    {
        public uint BaudRate { get; init; } = 9600;

        public FtdiDataBits DataBits { get; init; } = FtdiDataBits.BITS_8;
        public FtdiStopBits StopBits { get; init; } = FtdiStopBits.ONE;
        public FtdiParity Parity { get; init; } = FtdiParity.NONE;
        public FtdiBitMode BitMode { get; init; } = FtdiBitMode.RESET;
        public FtdiPurgeFlags PurgeFlags { get; init; } = FtdiPurgeFlags.BOTH;

        public byte Mask { get; init; } = 0;
        public uint ReadTimeout { get; init; } = 1000;
        public uint WriteTimeout { get; init; } = 1000;
        public byte Latency { get; init; } = 16;

        public FtdiFlowControl FlowControl { get; init; } = FtdiFlowControl.NONE;
        public byte Xon { get; init; } = 0x11;
        public byte Xoff { get; init; } = 0x13;
    }
}
