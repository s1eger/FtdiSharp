using System.Text;

namespace FtdiSharp
{
    // TODO:
    // 1. Сделать класс настроек и хранить здесь
    // 2. Сделать получение списка устройств
    // 3. Для списка устройств сделать устройство
    public class Ftdi
    {
        public FtdiSettings Settings { get; set; }

        public Ftdi(FtdiSettings settings)
        {
            Settings = settings;
        }

        public uint DeviceCount => GetDeviceCount();

        public uint GetDeviceCount()
        {
            FtdiNative.CreateDeviceInfoList(out uint count).ThrowIfNotOk();

            return count;
        }

        public uint GetLibraryVersion()
        {
            FtdiNative.GetLibraryVersion(out uint version).ThrowIfNotOk();

            return version;
        }

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

        public FtdiChannel OpenByIndex(uint index)
        {
            FtdiNative.Open(index, out FtdiSafeHandle handle).ThrowIfNotOk();

            return new FtdiChannel(Settings, handle);
        }

        public void Rescan() => FtdiNative.Rescan().ThrowIfNotOk(); 
        public void Reload(ushort vendorId, ushort productId) => FtdiNative.Reload(vendorId, productId).ThrowIfNotOk();
    }
}
