namespace FtdiSharp
{
    public class FtdiChannel : IDisposable
    {
        private readonly FtdiSafeHandle _handle;
        private bool _disposed;

        public bool Disposed => _disposed;

        internal FtdiChannel(FtdiSettings settings, FtdiSafeHandle handle)
        {
            _handle = handle;

            ApplySettings(settings);

            _disposed = false;
        }

        public void Dispose()
        {
            _handle.Dispose();
            _disposed = true;

            GC.SuppressFinalize(this);
        }

        public void Close() => Dispose();

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

        public uint Read(byte[] buffer)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfLessThan(buffer.Length, 0);

            FtdiNative.Read(_handle, buffer, (uint)buffer.Length, out uint readed).ThrowIfNotOk();

            return readed;
        }

        public uint Write(byte[] buffer)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfLessThan(buffer.Length, 0);

            FtdiNative.Write(_handle, buffer, (uint)buffer.Length, out uint written).ThrowIfNotOk();

            return written;
        }

        public uint GetQueueStatus()
        {
            FtdiNative.GetQueueStatus(_handle, out uint queue).ThrowIfNotOk();

            return queue;
        }

        public FtdiEventFlags GetStatus(out uint rxBytesCount, out uint txBytesCount)
        {
            FtdiNative.GetStatus(_handle, out rxBytesCount, out txBytesCount, out FtdiEventFlags status).ThrowIfNotOk();

            return status;
        }

        public FtdiModemStatus GetModemStatus()
        {
            FtdiNative.GetModemStatus(_handle, out FtdiModemStatus status).ThrowIfNotOk();

            return status;
        }

        public void SetBaudRate(uint baudRate) =>
            FtdiNative.SetBaudRate(_handle, baudRate).ThrowIfNotOk();

        public void SetDataCharacteristics(FtdiDataBits dataBits, FtdiStopBits stopBits, FtdiParity parity) => 
            FtdiNative.SetDataCharacteristics(_handle, dataBits, stopBits, parity).ThrowIfNotOk();

        public void SetFlowControl(FtdiFlowControl flowControl, byte xon, byte xoff) =>
            FtdiNative.SetFlowControl(_handle, flowControl, xon, xoff).ThrowIfNotOk();

        public void SetDtr() => FtdiNative.SetDtr(_handle).ThrowIfNotOk();
        public void ClearDtr() => FtdiNative.ClearDtr(_handle).ThrowIfNotOk();

        public void SetRts() => FtdiNative.SetRts(_handle).ThrowIfNotOk();
        public void ClearRts() => FtdiNative.ClearRts(_handle).ThrowIfNotOk();

        public void SetTimeouts(uint readTimeout, uint writeTimeout) =>
            FtdiNative.SetTimeouts(_handle, readTimeout, writeTimeout).ThrowIfNotOk();

        public void Purge(FtdiPurgeFlags flags) =>
            FtdiNative.Purge(_handle, flags).ThrowIfNotOk();

        public void ResetDevice() => FtdiNative.ResetDevice(_handle).ThrowIfNotOk();
        public void ResetPort() => FtdiNative.ResetPort(_handle).ThrowIfNotOk();
        public void CyclePort() => FtdiNative.CyclePort(_handle).ThrowIfNotOk();

        public void StopInTask() => FtdiNative.StopInTask(_handle).ThrowIfNotOk();
        public void RestartInTask() => FtdiNative.RestartInTask(_handle).ThrowIfNotOk();

        public void SetBreakOn() => FtdiNative.SetBreakOn(_handle).ThrowIfNotOk();
        public void SetBreakOff() => FtdiNative.SetBreakOff(_handle).ThrowIfNotOk();

        public void SetChars(byte eventCharacter, byte eventCharacterEnabled, byte errorCharacter, byte errorCharacterEnabled) =>
            FtdiNative.SetChars(_handle, eventCharacter, eventCharacterEnabled, errorCharacter, errorCharacterEnabled)
                .ThrowIfNotOk();

        public void SetResetPipeReryCount(uint count) => FtdiNative.SetResetPipeRetryCount(_handle, count).ThrowIfNotOk();
        public void SetDeadmanTimeout(uint milliseconds) => FtdiNative.SetDeadmanTimeout(_handle, milliseconds).ThrowIfNotOk();

        public void SetLatensyTimer(byte milliseconds) => FtdiNative.SetLatencyTimer(_handle, milliseconds).ThrowIfNotOk();
        public byte GetLatensyTimer()
        {
            FtdiNative.GetLatencyTimer(_handle, out byte milliseconds).ThrowIfNotOk();

            return milliseconds;
        }

        public void SetBitMode(byte mask, FtdiBitMode mode) => FtdiNative.SetBitMode(_handle, mask, mode).ThrowIfNotOk();
        public byte GetBitMode()
        {
            FtdiNative.GetBitMode(_handle, out byte states).ThrowIfNotOk();

            return states;
        }

        public void SetUsbParameters(uint inTransferSize, uint outTransferSize) =>
            FtdiNative.SetUsbParameters(_handle, inTransferSize, outTransferSize).ThrowIfNotOk();

        public uint GetDriverVersion()
        {
            FtdiNative.GetDriverVersion(_handle, out uint version).ThrowIfNotOk();
            
            return version;
        }

        public int GetComPortNumber()
        {
            FtdiNative.GetComPortNumber(_handle, out int port).ThrowIfNotOk();

            return port;
        }

        public ushort ReadEeprom(uint wordOffset)
        {
            FtdiNative.ReadEeprom(_handle, wordOffset, out ushort value).ThrowIfNotOk();

            return value;
        }
        public void WriteEeprom(uint wordOffset, ushort value) =>
            FtdiNative.WriteEeprom(_handle, wordOffset, value).ThrowIfNotOk();
        public void EraseEeprom() => FtdiNative.EraseEeprom(_handle);

        public uint GetEepromUserAreaSize()
        {
            FtdiNative.GetEepromUserAreaSize(_handle, out uint size).ThrowIfNotOk();

            return size;
        }
        public uint ReadEepromUserArea(byte[] buffer)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            FtdiNative.ReadEepromUserArea(_handle, buffer, buffer.Length, out uint readed).ThrowIfNotOk();

            return readed;
        }
        public void WriteEepromUserArea(byte[] buffer) => 
            FtdiNative.WriteEepromUserArea(_handle, buffer, buffer.Length);

        public void VendorCommandGet(byte request, byte[] buffer)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(buffer.Length, ushort.MaxValue);

            FtdiNative.VendorCommandGet(_handle, request, buffer, (ushort)buffer.Length).ThrowIfNotOk();
        }

        public void VendorCommandSet(byte request, byte[] buffer)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(buffer.Length, ushort.MaxValue);

            FtdiNative.VendorCommandSet(_handle, request, buffer, (ushort)buffer.Length);
        }
    }
}
