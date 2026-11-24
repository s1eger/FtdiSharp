namespace FtdiSharp
{
    public interface IFtdiDeviceFactory<T> where T : IFtdiDevice
    {
        /// <summary>
        /// Creates an FTDI device descriptor from the specified device information.
        /// </summary>
        T Create(FtdiDeviceFlags flags, FtdiDeviceType type, uint id, uint location, string serial, string description);
    }
}
