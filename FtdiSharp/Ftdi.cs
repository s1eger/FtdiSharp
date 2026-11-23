using System.Text;

namespace FtdiSharp
{
    public class Ftdi
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
        public IReadOnlyList<FtdiDevice> GetDevices()
        {
            var count = DeviceCount;

            if (count == 0)
                return [];

            var devices = new List<FtdiDevice>();
            for (uint i = 0; i < count; i++)
            {
                devices.Add(GetDeviceInfoDetail(i));
            }

            return devices.AsReadOnly();
        }

        /// <summary>
        /// Gets detailed information about the device at the specified index.
        /// </summary>
        public FtdiDevice GetDeviceInfoDetail(uint index)
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

            return new FtdiDevice(Settings)
            {
                Flags = flags,
                Type = type,
                Id = id,
                Location = location,
                Serial = Encoding.ASCII.GetString(serial).Cut('\0'),
                Description = Encoding.ASCII.GetString(description).Cut('\0'),
                Handle = handle
            };
        }

        /// <summary>
        /// Opens the device at the specified index and returns a configured channel.
        /// </summary>
        public FtdiChannel OpenByIndex(uint index)
        {
            FtdiNative.Open(index, out FtdiSafeHandle handle).ThrowIfNotOk();

            return new FtdiChannel(Settings, handle);
        }

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
