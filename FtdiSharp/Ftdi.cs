using FtdiSharp.Abstractions;
using FtdiSharp.Native;
using System.Text;

namespace FtdiSharp
{
    public class Ftdi : IFtdi
    {
        public FtdiSettings Settings { get; set; }

        /// <summary>
        /// Initializes a new instance with the specified device settings.
        /// </summary>
        public Ftdi(FtdiSettings settings)
        {
            Settings = settings;
        }

        public uint DeviceCount => GetDeviceCount();

        /// <summary>
        /// Gets the number of connected FTDI devices.
        /// </summary>
        public uint GetDeviceCount()
        {
            FtdiNative.CreateDeviceInfoList(out uint count).ThrowIfNotOk();

            return count;
        }

        /// <summary>
        /// Gets the version of the FTDI D2XX library.
        /// </summary>
        public uint GetLibraryVersion()
        {
            FtdiNative.GetLibraryVersion(out uint version).ThrowIfNotOk();

            return version;
        }

        /// <summary>
        /// Gets information about all connected FTDI devices.
        /// </summary>
        public IReadOnlyList<FtdiDevice> GetDevices() => GetDevices(new DefaultFtdiFactory(this));

        /// <summary>
        /// Gets information about all connected FTDI devices using the specified factory.
        /// </summary>
        public IReadOnlyList<T> GetDevices<T>(IFtdiDeviceFactory<T> factory) where T: IFtdiDevice
        {
            var count = DeviceCount;

            if (count == 0)
                return [];

            var devices = new List<T>();
            for (uint i = 0; i < count; i++)
                devices.Add(GetDeviceInfoDetail(i, factory));

            return devices.AsReadOnly();
        }

        /// <summary>
        /// Gets detailed information about the device at the specified index.
        /// </summary>
        public FtdiDevice GetDeviceInfoDetail(uint index) => GetDeviceInfoDetail(index, new DefaultFtdiFactory(this));

        /// <summary>
        /// Gets detailed information about the device at the specified index using the specified factory.
        /// </summary>
        public T GetDeviceInfoDetail<T>(uint index, IFtdiDeviceFactory<T> factory) where T: IFtdiDevice
        {
            byte[] serial = new byte[16];
            byte[] description = new byte[64];

            FtdiNative.GetDeviceInfoDetail(
                index,
                out var flags,
                out var type,
                out var id,
                out var location,
                serial,
                description,
                out IntPtr handle
                ).ThrowIfNotOk();

            return factory.Create(
                flags: flags, 
                type: type, 
                id: id, 
                location: location, 
                serial: Encoding.ASCII.GetString(serial).Cut('\0'), 
                description: Encoding.ASCII.GetString(description).Cut('\0'));
        }

        /// <summary>
        /// Opens the device at the specified index from the list of devices and returns a configured channel.
        /// </summary>
        public IFtdiChannel OpenByIndex(uint index)
        {
            FtdiNative.Open(index, out FtdiSafeHandle handle).ThrowIfNotOk();

            return new FtdiChannel(Settings, handle);
        }

        /// <summary>
        /// Opens the device by its serial number and returns a configured channel.
        /// </summary>
        public IFtdiChannel OpenBySerial(string serial)
        {
            FtdiNative.OpenEx(serial, FtdiOpenFlags.SerialNumber, out FtdiSafeHandle handle).ThrowIfNotOk();

            return new FtdiChannel(Settings, handle);
        }

        /// <summary>
        /// Opens the device by its serial number and returns a configured channel.
        /// </summary>
        public IFtdiChannel OpenBySerial(IFtdiDevice device) => OpenBySerial(device.Serial);

        /// <summary>
        /// Opens the device by its description and returns a configured channel.
        /// </summary>
        public IFtdiChannel OpenByDescription(string description)
        {
            FtdiNative.OpenEx(description, FtdiOpenFlags.Description, out FtdiSafeHandle handle).ThrowIfNotOk();

            return new FtdiChannel(Settings, handle);
        }

        /// <summary>
        /// Opens the device by its description and returns a configured channel.
        /// </summary>
        public IFtdiChannel OpenByDescription(IFtdiDevice device) => OpenByDescription(device.Description);

        /// <summary>
        /// Opens the device by its location and returns a configured channel.
        /// </summary>
        public IFtdiChannel OpenByLocation(uint location)
        {
            FtdiNative.OpenEx(location, FtdiOpenFlags.Location, out FtdiSafeHandle handle).ThrowIfNotOk();

            return new FtdiChannel(Settings, handle);
        }

        /// <summary>
        /// Opens the device by its location and returns a configured channel.
        /// </summary>
        public IFtdiChannel OpenByLocation(IFtdiDevice device) => OpenByLocation(device.Location);

        /// <summary>
        /// Rescans the system for connected FTDI devices.
        /// </summary>
        public void Rescan() => FtdiNative.Rescan().ThrowIfNotOk(); 

        /// <summary>
        /// Reloads the driver for the device with the specified vendor and product IDs.
        /// </summary>
        public void Reload(ushort vendorId, ushort productId) => FtdiNative.Reload(vendorId, productId).ThrowIfNotOk();
    }
}
