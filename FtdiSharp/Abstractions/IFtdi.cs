namespace FtdiSharp.Abstractions
{
    public interface IFtdi
    {
        /// <summary>
        /// Gets the number of connected FTDI devices.
        /// </summary>
        uint GetDeviceCount();

        /// <summary>
        /// Gets the version of the FTDI D2XX library.
        /// </summary>
        uint GetLibraryVersion();

        /// <summary>
        /// Gets information about all connected FTDI devices using the specified factory.
        /// </summary>
        IReadOnlyList<T> GetDevices<T>(IFtdiDeviceFactory<T> factory) where T : IFtdiDevice;

        /// <summary>
        /// Gets detailed information about the device at the specified index using the specified factory.
        /// </summary>
        T GetDeviceInfoDetail<T>(uint index, IFtdiDeviceFactory<T> factory) where T : IFtdiDevice;

        /// <summary>
        /// Opens the device at the specified index from the list of devices and returns a configured channel.
        /// </summary>
        IFtdiChannel OpenByIndex(uint index);

        /// <summary>
        /// Opens the device by its serial number and returns a configured channel.
        /// </summary>
        IFtdiChannel OpenBySerial(uint serial);

        /// <summary>
        /// Opens the device by its serial number and returns a configured channel.
        /// </summary>
        IFtdiChannel OpenBySerial(IFtdiDevice device);

        /// <summary>
        /// Opens the device by its description and returns a configured channel.
        /// </summary>
        IFtdiChannel OpenByDescription(string description);

        /// <summary>
        /// Opens the device by its description and returns a configured channel.
        /// </summary>
        IFtdiChannel OpenByDescription(IFtdiDevice device);

        /// <summary>
        /// Opens the device by its location and returns a configured channel.
        /// </summary>
        IFtdiChannel OpenByLocation(string location);

        /// <summary>
        /// Opens the device by its location and returns a configured channel.
        /// </summary>
        IFtdiChannel OpenByLocation(IFtdiDevice device);

        /// <summary>
        /// Rescans the system for connected FTDI devices.
        /// </summary>
        public void Rescan();

        /// <summary>
        /// Reloads the driver for the device with the specified vendor and product IDs.
        /// </summary>
        public void Reload(ushort vendorId, ushort productId);
    }
}
