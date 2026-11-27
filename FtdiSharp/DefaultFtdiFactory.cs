using FtdiSharp.Abstractions;

namespace FtdiSharp
{
    public class DefaultFtdiFactory : IFtdiDeviceFactory<FtdiDevice>
    {
        private readonly Ftdi _ftdi;

        /// <summary>
        /// Initializes a factory that creates device descriptors associated with the specified FTDI instance.
        /// </summary>
        public DefaultFtdiFactory(Ftdi ftdi)
        {
            _ftdi = ftdi;
        }

        /// <summary>
        /// Creates a device descriptor populated with the specified device information.
        /// </summary>
        public FtdiDevice Create(FtdiDeviceFlags flags, FtdiDeviceType type, uint id, uint location, string serial, string description) => new(_ftdi)
        {
            Flags = flags,
            Type = type,
            Id = id,
            Location = location,
            Serial = serial,
            Description = description
        };
    }
}
