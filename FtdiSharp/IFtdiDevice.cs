namespace FtdiSharp
{
    public interface IFtdiDevice
    {
        FtdiDeviceFlags Flags { get; init; }
        FtdiDeviceType Type { get; init; }
        uint Id { get; init; }
        uint Location { get; init; }
        string Serial { get; init; }
        string Description { get; init; }
    }
}
