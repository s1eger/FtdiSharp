namespace FtdiSharp
{
    public interface IFtdiDevice
    {
        FtdiDeviceFlags Flags { get; }
        FtdiDeviceType Type { get; }
        uint Id { get; }
        uint Location { get; }
        string Serial { get; }
        string Description { get; }
    }
}
