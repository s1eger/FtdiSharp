using FtdiSharp.Abstractions;

namespace FtdiSharp
{
    public class FtdiDevice : IFtdiDevice
    {
        public FtdiDeviceFlags Flags { get; init; }
        public FtdiDeviceType Type { get; init; }
        public uint Id { get; init; }
        public uint Location { get; init; }
        public string Serial { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;

        private readonly Ftdi _ftdi;

        /// <summary>
        /// Initializes a device descriptor that uses the specified FTDI instance to open channels.
        /// </summary>
        public FtdiDevice(Ftdi ftdi)
        {
            _ftdi = ftdi;
        }

        /// <summary>
        /// Opens the device by its serial number and returns a configured channel.
        /// </summary>
        public virtual FtdiChannel OpenBySerial() => _ftdi.OpenBySerial(this);

        /// <summary>
        /// Opens the device by its description and returns a configured channel.
        /// </summary>
        public virtual FtdiChannel OpenByDescription() => _ftdi.OpenByDescription(this);

        /// <summary>
        /// Opens the device by its location and returns a configured channel.
        /// </summary>
        public virtual FtdiChannel OpenByLocation() => _ftdi.OpenByLocation(this);
    }
}
