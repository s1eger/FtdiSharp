using FtdiSharp.Native;

namespace FtdiSharp
{
    public class FtdiChannel : IDisposable
    {
        private readonly FtdiSafeHandle _handle;
        private bool _disposed;

        public bool Disposed => _disposed;

        /// <summary>
        /// Initializes a channel with the specified settings and device handle.
        /// </summary>
        internal FtdiChannel(FtdiSettings settings, FtdiSafeHandle handle)
        {
            _handle = handle;

            ApplySettings(settings);

            _disposed = false;
        }

        /// <summary>
        /// Closes the channel and releases the underlying device handle.
        /// </summary>
        public void Dispose()
        {
            _handle.Dispose();
            _disposed = true;

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Closes the channel.
        /// </summary>
        public void Close() => Dispose();

        /// <summary>
        /// Resets the device and applies the specified communication settings.
        /// </summary>
        public void ApplySettings(FtdiSettings settings)
        {
            ResetDevice();

            SetBaudRate(settings.BaudRate);
            SetDataCharacteristics(settings.DataBits, settings.StopBits, settings.Parity);
            SetFlowControl(settings.FlowControl, settings.Xon, settings.Xoff);

            SetTimeouts(settings.ReadTimeout, settings.WriteTimeout);
            SetLatensyTimer(settings.Latency);

            if (settings.BitMode != FtdiBitMode.RESET)
                SetBitMode(settings.Mask, settings.BitMode);

            Purge(settings.PurgeFlags);
        }

        /// <summary>
        /// Reads bytes from the device into the buffer and returns the number of bytes read.
        /// </summary>
        public uint Read(byte[] buffer)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfLessThan(buffer.Length, 0);

            FtdiNative.Read(_handle, buffer, (uint)buffer.Length, out uint readed).ThrowIfNotOk();

            return readed;
        }

        /// <summary>
        /// Writes bytes from the buffer to the device and returns the number of bytes written.
        /// </summary>
        public uint Write(byte[] buffer)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfLessThan(buffer.Length, 0);

            FtdiNative.Write(_handle, buffer, (uint)buffer.Length, out uint written).ThrowIfNotOk();

            return written;
        }

        /// <summary>
        /// Gets the number of bytes waiting in the receive queue.
        /// </summary>
        public uint GetQueueStatus()
        {
            FtdiNative.GetQueueStatus(_handle, out uint queue).ThrowIfNotOk();

            return queue;
        }

        /// <summary>
        /// Gets the event status and the receive and transmit queue byte counts.
        /// </summary>
        public FtdiEventFlags GetStatus(out uint rxBytesCount, out uint txBytesCount)
        {
            FtdiNative.GetStatus(_handle, out rxBytesCount, out txBytesCount, out FtdiEventFlags status).ThrowIfNotOk();

            return status;
        }

        /// <summary>
        /// Gets the current modem and line status flags.
        /// </summary>
        public FtdiModemStatus GetModemStatus()
        {
            FtdiNative.GetModemStatus(_handle, out FtdiModemStatus status).ThrowIfNotOk();

            return status;
        }

        /// <summary>
        /// Sets the baud rate for the channel.
        /// </summary>
        public void SetBaudRate(uint baudRate) =>
            FtdiNative.SetBaudRate(_handle, baudRate).ThrowIfNotOk();

        /// <summary>
        /// Sets the number of data bits, stop bits, and parity mode.
        /// </summary>
        public void SetDataCharacteristics(FtdiDataBits dataBits, FtdiStopBits stopBits, FtdiParity parity) => 
            FtdiNative.SetDataCharacteristics(_handle, dataBits, stopBits, parity).ThrowIfNotOk();

        /// <summary>
        /// Sets the flow-control mode and the XON and XOFF characters.
        /// </summary>
        public void SetFlowControl(FtdiFlowControl flowControl, byte xon, byte xoff) =>
            FtdiNative.SetFlowControl(_handle, flowControl, xon, xoff).ThrowIfNotOk();

        /// <summary>
        /// Asserts the DTR signal.
        /// </summary>
        public void SetDtr() => FtdiNative.SetDtr(_handle).ThrowIfNotOk();

        /// <summary>
        /// Clears the DTR signal.
        /// </summary>
        public void ClearDtr() => FtdiNative.ClearDtr(_handle).ThrowIfNotOk();

        /// <summary>
        /// Asserts the RTS signal.
        /// </summary>
        public void SetRts() => FtdiNative.SetRts(_handle).ThrowIfNotOk();

        /// <summary>
        /// Clears the RTS signal.
        /// </summary>
        public void ClearRts() => FtdiNative.ClearRts(_handle).ThrowIfNotOk();

        /// <summary>
        /// Sets the read and write timeouts in milliseconds.
        /// </summary>
        public void SetTimeouts(uint readTimeout, uint writeTimeout) =>
            FtdiNative.SetTimeouts(_handle, readTimeout, writeTimeout).ThrowIfNotOk();

        /// <summary>
        /// Clears the receive queue, transmit queue, or both, according to the flags.
        /// </summary>
        public void Purge(FtdiPurgeFlags flags) =>
            FtdiNative.Purge(_handle, flags).ThrowIfNotOk();

        /// <summary>
        /// Resets the device.
        /// </summary>
        public void ResetDevice() => FtdiNative.ResetDevice(_handle).ThrowIfNotOk();

        /// <summary>
        /// Resets the device port.
        /// </summary>
        public void ResetPort() => FtdiNative.ResetPort(_handle).ThrowIfNotOk();

        /// <summary>
        /// Cycles the device port to simulate a disconnect and reconnect.
        /// </summary>
        public void CyclePort() => FtdiNative.CyclePort(_handle).ThrowIfNotOk();

        /// <summary>
        /// Stops the driver's input task.
        /// </summary>
        public void StopInTask() => FtdiNative.StopInTask(_handle).ThrowIfNotOk();

        /// <summary>
        /// Restarts the driver's input task.
        /// </summary>
        public void RestartInTask() => FtdiNative.RestartInTask(_handle).ThrowIfNotOk();

        /// <summary>
        /// Enables the serial break condition.
        /// </summary>
        public void SetBreakOn() => FtdiNative.SetBreakOn(_handle).ThrowIfNotOk();

        /// <summary>
        /// Disables the serial break condition.
        /// </summary>
        public void SetBreakOff() => FtdiNative.SetBreakOff(_handle).ThrowIfNotOk();

        /// <summary>
        /// Configures the event and error characters and enables or disables each one.
        /// </summary>
        public void SetChars(byte eventCharacter, byte eventCharacterEnabled, byte errorCharacter, byte errorCharacterEnabled) =>
            FtdiNative.SetChars(_handle, eventCharacter, eventCharacterEnabled, errorCharacter, errorCharacterEnabled)
                .ThrowIfNotOk();

        /// <summary>
        /// Sets the number of retries used when resetting a pipe.
        /// </summary>
        public void SetResetPipeReryCount(uint count) => FtdiNative.SetResetPipeRetryCount(_handle, count).ThrowIfNotOk();

        /// <summary>
        /// Sets the deadman timeout in milliseconds.
        /// </summary>
        public void SetDeadmanTimeout(uint milliseconds) => FtdiNative.SetDeadmanTimeout(_handle, milliseconds).ThrowIfNotOk();

        /// <summary>
        /// Sets the latency timer in milliseconds.
        /// </summary>
        public void SetLatensyTimer(byte milliseconds) => FtdiNative.SetLatencyTimer(_handle, milliseconds).ThrowIfNotOk();

        /// <summary>
        /// Gets the latency timer value in milliseconds.
        /// </summary>
        public byte GetLatensyTimer()
        {
            FtdiNative.GetLatencyTimer(_handle, out byte milliseconds).ThrowIfNotOk();

            return milliseconds;
        }

        /// <summary>
        /// Configures the bit mode and the I/O pin mask.
        /// </summary>
        public void SetBitMode(byte mask, FtdiBitMode mode) => FtdiNative.SetBitMode(_handle, mask, mode).ThrowIfNotOk();

        /// <summary>
        /// Gets the current states of the device's data pins.
        /// </summary>
        public byte GetBitMode()
        {
            FtdiNative.GetBitMode(_handle, out byte states).ThrowIfNotOk();

            return states;
        }

        /// <summary>
        /// Sets the USB input and output transfer sizes.
        /// </summary>
        public void SetUsbParameters(uint inTransferSize, uint outTransferSize) =>
            FtdiNative.SetUsbParameters(_handle, inTransferSize, outTransferSize).ThrowIfNotOk();

        /// <summary>
        /// Gets the version of the driver used by the device.
        /// </summary>
        public uint GetDriverVersion()
        {
            FtdiNative.GetDriverVersion(_handle, out uint version).ThrowIfNotOk();
            
            return version;
        }

        /// <summary>
        /// Gets the COM port number assigned to the device.
        /// </summary>
        public int GetComPortNumber()
        {
            FtdiNative.GetComPortNumber(_handle, out int port).ThrowIfNotOk();

            return port;
        }

        /// <summary>
        /// Reads a 16-bit word from the EEPROM at the specified word offset.
        /// </summary>
        public ushort ReadEeprom(uint wordOffset)
        {
            FtdiNative.ReadEeprom(_handle, wordOffset, out ushort value).ThrowIfNotOk();

            return value;
        }

        /// <summary>
        /// Writes a 16-bit word to the EEPROM at the specified word offset.
        /// </summary>
        public void WriteEeprom(uint wordOffset, ushort value) =>
            FtdiNative.WriteEeprom(_handle, wordOffset, value).ThrowIfNotOk();

        /// <summary>
        /// Erases the device EEPROM.
        /// </summary>
        public void EraseEeprom() => FtdiNative.EraseEeprom(_handle);

        /// <summary>
        /// Gets the size of the EEPROM user area in bytes.
        /// </summary>
        public uint GetEepromUserAreaSize()
        {
            FtdiNative.GetEepromUserAreaSize(_handle, out uint size).ThrowIfNotOk();

            return size;
        }

        /// <summary>
        /// Reads the EEPROM user area into the buffer and returns the number of bytes read.
        /// </summary>
        public uint ReadEepromUserArea(byte[] buffer)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            FtdiNative.ReadEepromUserArea(_handle, buffer, buffer.Length, out uint readed).ThrowIfNotOk();

            return readed;
        }

        /// <summary>
        /// Writes the buffer contents to the EEPROM user area.
        /// </summary>
        public void WriteEepromUserArea(byte[] buffer) => 
            FtdiNative.WriteEepromUserArea(_handle, buffer, buffer.Length);

        /// <summary>
        /// Sends a vendor command that reads data into the buffer.
        /// </summary>
        public void VendorCommandGet(byte request, byte[] buffer)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(buffer.Length, ushort.MaxValue);

            FtdiNative.VendorCommandGet(_handle, request, buffer, (ushort)buffer.Length).ThrowIfNotOk();
        }

        /// <summary>
        /// Sends a vendor command with data from the buffer.
        /// </summary>
        public void VendorCommandSet(byte request, byte[] buffer)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(buffer.Length, ushort.MaxValue);

            FtdiNative.VendorCommandSet(_handle, request, buffer, (ushort)buffer.Length);
        }
    }
}
