namespace FtdiSharp
{
    public class FtdiDevice
    {
        public FtdiDeviceFlags Flags { get; init; }
        public FtdiDeviceType Type { get; init; }
        public uint Id { get; init; }
        public uint Location { get; init; }
        public string Serial { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public IntPtr Handle { get; set; }

        public FtdiSettings Settings { get; set; }

        public FtdiDevice(FtdiSettings settings)
        {
            Settings = settings;
        }

        public virtual FtdiChannel OpenBySerial()
        {
            FtdiNative.OpenEx(Serial, FtdiOpenFlags.SerialNumber, out FtdiSafeHandle handle).ThrowIfNotOk();

            return new FtdiChannel(Settings, handle);
        }

        public virtual FtdiChannel OpenByDescription()
        {
            FtdiNative.OpenEx(Description, FtdiOpenFlags.Description, out FtdiSafeHandle handle).ThrowIfNotOk();

            return new FtdiChannel(Settings, handle);
        }

        public virtual FtdiChannel OpenByLocation()
        {
            FtdiNative.OpenEx(Location, FtdiOpenFlags.Location, out FtdiSafeHandle handle).ThrowIfNotOk();

            return new FtdiChannel(Settings, handle);
        }
    }
}
