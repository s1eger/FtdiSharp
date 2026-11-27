namespace FtdiSharp.Abstractions
{
    public interface IFtdiChannel : IDisposable
    {
        /// <summary>
        /// Closes the channel.
        /// </summary>
        public void Close();

        /// <summary>
        /// Resets the device and applies the specified communication settings.
        /// </summary>
        public void ApplySettings(FtdiSettings settings);

        /// <summary>
        /// Reads bytes from the device into the buffer and returns the number of bytes read.
        /// </summary>
        public uint Read(byte[] buffer);

        /// <summary>
        /// Writes bytes from the buffer to the device and returns the number of bytes written.
        /// </summary>
        public uint Write(byte[] buffer);

        /// <summary>
        /// Gets the number of bytes waiting in the receive queue.
        /// </summary>
        public uint GetQueueStatus();

        /// <summary>
        /// Gets the event status and the receive and transmit queue byte counts.
        /// </summary>
        public FtdiEventFlags GetStatus(out uint rxBytesCount, out uint txBytesCount);

        /// <summary>
        /// Gets the current modem and line status flags.
        /// </summary>
        public FtdiModemStatus GetModemStatus();

        /// <summary>
        /// Sets the baud rate for the channel.
        /// </summary>
        public void SetBaudRate(uint baudRate);

        /// <summary>
        /// Sets the number of data bits, stop bits, and parity mode.
        /// </summary>
        public void SetDataCharacteristics(FtdiDataBits dataBits, FtdiStopBits stopBits, FtdiParity parity);

        /// <summary>
        /// Sets the flow-control mode and the XON and XOFF characters.
        /// </summary>
        public void SetFlowControl(FtdiFlowControl flowControl, byte xon, byte xoff);

        /// <summary>
        /// Asserts the DTR signal.
        /// </summary>
        public void SetDtr();

        /// <summary>
        /// Clears the DTR signal.
        /// </summary>
        public void ClearDtr();

        /// <summary>
        /// Asserts the RTS signal.
        /// </summary>
        public void SetRts();

        /// <summary>
        /// Clears the RTS signal.
        /// </summary>
        public void ClearRts();

        /// <summary>
        /// Sets the read and write timeouts in milliseconds.
        /// </summary>
        public void SetTimeouts(uint readTimeout, uint writeTimeout);

        /// <summary>
        /// Clears the receive queue, transmit queue, or both, according to the flags.
        /// </summary>
        public void Purge(FtdiPurgeFlags flags);

        /// <summary>
        /// Resets the device.
        /// </summary>
        public void ResetDevice();

        /// <summary>
        /// Resets the device port.
        /// </summary>
        public void ResetPort();

        /// <summary>
        /// Cycles the device port to simulate a disconnect and reconnect.
        /// </summary>
        public void CyclePort();

        /// <summary>
        /// Stops the driver's input task.
        /// </summary>
        public void StopInTask();

        /// <summary>
        /// Restarts the driver's input task.
        /// </summary>
        public void RestartInTask();

        /// <summary>
        /// Enables the serial break condition.
        /// </summary>
        public void SetBreakOn();

        /// <summary>
        /// Disables the serial break condition.
        /// </summary>
        public void SetBreakOff();

        /// <summary>
        /// Configures the event and error characters and enables or disables each one.
        /// </summary>
        public void SetChars(byte eventCharacter, byte eventCharacterEnabled, byte errorCharacter, byte errorCharacterEnabled);

        /// <summary>
        /// Sets the number of retries used when resetting a pipe.
        /// </summary>
        public void SetResetPipeReryCount(uint count);

        /// <summary>
        /// Sets the deadman timeout in milliseconds.
        /// </summary>
        public void SetDeadmanTimeout(uint milliseconds);

        /// <summary>
        /// Sets the latency timer in milliseconds.
        /// </summary>
        public void SetLatensyTimer(byte milliseconds);

        /// <summary>
        /// Gets the latency timer value in milliseconds.
        /// </summary>
        public byte GetLatensyTimer();

        /// <summary>
        /// Configures the bit mode and the I/O pin mask.
        /// </summary>
        public void SetBitMode(byte mask, FtdiBitMode mode);

        /// <summary>
        /// Gets the current states of the device's data pins.
        /// </summary>
        public byte GetBitMode();

        /// <summary>
        /// Sets the USB input and output transfer sizes.
        /// </summary>
        public void SetUsbParameters(uint inTransferSize, uint outTransferSize);

        /// <summary>
        /// Gets the version of the driver used by the device.
        /// </summary>
        public uint GetDriverVersion();

        /// <summary>
        /// Gets the COM port number assigned to the device.
        /// </summary>
        public int GetComPortNumber();

        /// <summary>
        /// Reads a 16-bit word from the EEPROM at the specified word offset.
        /// </summary>
        public ushort ReadEeprom(uint wordOffset);

        /// <summary>
        /// Writes a 16-bit word to the EEPROM at the specified word offset.
        /// </summary>
        public void WriteEeprom(uint wordOffset, ushort value);

        /// <summary>
        /// Erases the device EEPROM.
        /// </summary>
        public void EraseEeprom();

        /// <summary>
        /// Gets the size of the EEPROM user area in bytes.
        /// </summary>
        public uint GetEepromUserAreaSize();

        /// <summary>
        /// Reads the EEPROM user area into the buffer and returns the number of bytes read.
        /// </summary>
        public uint ReadEepromUserArea(byte[] buffer);

        /// <summary>
        /// Writes the buffer contents to the EEPROM user area.
        /// </summary>
        public void WriteEepromUserArea(byte[] buffer);

        /// <summary>
        /// Sends a vendor command that reads data into the buffer.
        /// </summary>
        public void VendorCommandGet(byte request, byte[] buffer);

        /// <summary>
        /// Sends a vendor command with data from the buffer.
        /// </summary>
        public void VendorCommandSet(byte request, byte[] buffer);
    }
}
