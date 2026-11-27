using System.Runtime.InteropServices;

namespace FtdiSharp.Native;

internal static class FtdiNative
{
    public const string NativeName = "ftd2xx";

    private const CallingConvention Convention = CallingConvention.StdCall;


    /// <summary>
    /// Builds the device information list and returns the number of connected devices.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_CreateDeviceInfoList", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus CreateDeviceInfoList(out uint deviceCount);

    /// <summary>
    /// Retrieves detailed information for an entry in the device information list.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_GetDeviceInfoDetail", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus GetDeviceInfoDetail(
        uint index,
        out FtdiDeviceFlags flags,
        out FtdiDeviceType deviceType,
        out uint id,
        out uint locationId,
        [Out] byte[] serialNumber,
        [Out] byte[] description,
        out IntPtr handle);

    /// <summary>
    /// Opens a device by its index in the device information list.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_Open", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus Open(uint index, out FtdiSafeHandle handle);

    /// <summary>
    /// Opens a device by a string identifier such as its serial number or description.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_OpenEx", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus OpenEx(string parameter, FtdiOpenFlags flags, out FtdiSafeHandle handle);

    /// <summary>
    /// Opens a device by its location identifier.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_OpenEx", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus OpenEx(uint location, FtdiOpenFlags flags, out FtdiSafeHandle handle);

    /// <summary>
    /// Closes an open device handle.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_Close", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus Close(IntPtr handle);

    /// <summary>
    /// Reads data from the device into the supplied buffer.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_Read", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus Read(FtdiSafeHandle handle, [Out] byte[] buffer, uint bytesToRead, out uint bytesRead);

    /// <summary>
    /// Writes data from the supplied buffer to the device.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_Write", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus Write(FtdiSafeHandle handle, [In] byte[] buffer, uint bytesToWrite, out uint bytesWritten);

    /// <summary>
    /// Retrieves the number of bytes in the receive queue.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_GetQueueStatus", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus GetQueueStatus(FtdiSafeHandle handle, out uint rxQueueBytes);

    /// <summary>
    /// Retrieves the receive and transmit queue sizes and the event status.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_GetStatus", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus GetStatus(FtdiSafeHandle handle, out uint rxQueueBytes, out uint txQueueBytes, out FtdiEventFlags eventStatus);

    /// <summary>
    /// Retrieves the current modem and line status.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_GetModemStatus", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus GetModemStatus(FtdiSafeHandle handle, out FtdiModemStatus modemStatus);

    /// <summary>
    /// Sets the device baud rate.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_SetBaudRate", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetBaudRate(FtdiSafeHandle handle, uint baudRate);

    /// <summary>
    /// Sets the data bits, stop bits, and parity mode.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_SetDataCharacteristics", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetDataCharacteristics(FtdiSafeHandle handle, FtdiDataBits dataBits, FtdiStopBits stopBits, FtdiParity parity);

    /// <summary>
    /// Sets the flow-control mode and the XON and XOFF characters.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_SetFlowControl", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetFlowControl(FtdiSafeHandle handle, FtdiFlowControl flowControl, byte xon, byte xoff);

    /// <summary>
    /// Asserts the DTR signal.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_SetDtr", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetDtr(FtdiSafeHandle handle);

    /// <summary>
    /// Clears the DTR signal.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_ClrDtr", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus ClearDtr(FtdiSafeHandle handle);

    /// <summary>
    /// Asserts the RTS signal.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_SetRts", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetRts(FtdiSafeHandle handle);

    /// <summary>
    /// Clears the RTS signal.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_ClrRts", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus ClearRts(FtdiSafeHandle handle);

    /// <summary>
    /// Sets the read and write timeouts.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_SetTimeouts", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetTimeouts(FtdiSafeHandle handle, uint readTimeOut, uint writeTimeout);

    /// <summary>
    /// Purges the receive queue, transmit queue, or both.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_Purge", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus Purge(FtdiSafeHandle handle, FtdiPurgeFlags mask);

    /// <summary>
    /// Resets the device.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_ResetDevice", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus ResetDevice(FtdiSafeHandle handle);

    /// <summary>
    /// Resets the device port.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_ResetPort", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus ResetPort(FtdiSafeHandle handle);

    /// <summary>
    /// Cycles the device port to simulate a disconnect and reconnect.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_CyclePort", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus CyclePort(FtdiSafeHandle handle);

    /// <summary>
    /// Rescans the system for FTDI devices.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_Rescan", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus Rescan();

    /// <summary>
    /// Reloads the driver for the specified vendor and product IDs.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_Reload", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus Reload(ushort vendorId, ushort productId);

    /// <summary>
    /// Stops the driver's input task.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_StopInTask", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus StopInTask(FtdiSafeHandle handle);

    /// <summary>
    /// Restarts the driver's input task.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_RestartInTask", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus RestartInTask(FtdiSafeHandle handle);

    /// <summary>
    /// Enables the serial break condition.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_SetBreakOn", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetBreakOn(FtdiSafeHandle handle);

    /// <summary>
    /// Disables the serial break condition.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_SetBreakOff", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetBreakOff(FtdiSafeHandle handle);

    /// <summary>
    /// Configures the event and error characters and their enable flags.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_SetChars", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetChars(FtdiSafeHandle handle, byte eventCharacter, byte eventCharacterEnabled, byte errorCharacter, byte errorCharacterEnabled);

    /// <summary>
    /// Sets the number of retries used when resetting a pipe.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_SetResetPipeRetryCount", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetResetPipeRetryCount(FtdiSafeHandle handle, uint count);

    /// <summary>
    /// Sets the deadman timeout in milliseconds.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_SetDeadmanTimeout", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetDeadmanTimeout(FtdiSafeHandle handle, uint timeoutMilliseconds);

    /// <summary>
    /// Sets the latency timer in milliseconds.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_SetLatencyTimer", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetLatencyTimer(FtdiSafeHandle handle, byte latencyMilliseconds);

    /// <summary>
    /// Retrieves the latency timer value in milliseconds.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_GetLatencyTimer", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus GetLatencyTimer(FtdiSafeHandle handle, out byte latencyMilliseconds);

    /// <summary>
    /// Configures the bit mode and I/O pin mask.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_SetBitMode", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetBitMode(FtdiSafeHandle handle, byte mask, FtdiBitMode mode);

    /// <summary>
    /// Retrieves the current states of the device's data pins.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_GetBitMode", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus GetBitMode(FtdiSafeHandle handle, out byte pinStates);

    /// <summary>
    /// Sets the USB input and output transfer sizes.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_SetUSBParameters", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetUsbParameters(FtdiSafeHandle handle, uint inTransferSize, uint outTransferSize);

    /// <summary>
    /// Retrieves the driver version for the device.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_GetDriverVersion", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus GetDriverVersion(FtdiSafeHandle handle, out uint version);

    /// <summary>
    /// Retrieves the D2XX library version.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_GetLibraryVersion", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus GetLibraryVersion(out uint version);

    /// <summary>
    /// Retrieves the COM port number assigned to the device.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_GetComPortNumber", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus GetComPortNumber(FtdiSafeHandle handle, out int comPortNumber);

    /// <summary>
    /// Reads a 16-bit value from the EEPROM at the specified word offset.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_ReadEE", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus ReadEeprom(FtdiSafeHandle handle, uint wordOffset, out ushort value);

    /// <summary>
    /// Writes a 16-bit value to the EEPROM at the specified word offset.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_WriteEE", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus WriteEeprom(FtdiSafeHandle handle, uint wordOffset, ushort value);

    /// <summary>
    /// Erases the device EEPROM.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_EraseEE", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus EraseEeprom(FtdiSafeHandle handle);

    /// <summary>
    /// Retrieves the size of the EEPROM user area.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_EE_UASize", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus GetEepromUserAreaSize(FtdiSafeHandle handle, out uint size);

    /// <summary>
    /// Reads data from the EEPROM user area into the supplied buffer.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_EE_UARead", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus ReadEepromUserArea(FtdiSafeHandle handle, [Out] byte[] buffer, int length, out uint bytesRead);

    /// <summary>
    /// Writes data from the supplied buffer to the EEPROM user area.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_EE_UAWrite", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus WriteEepromUserArea(FtdiSafeHandle handle, [In] byte[] buffer, int length);

    /// <summary>
    /// Sends a vendor command that reads data into the supplied buffer.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_VendorCmdGet", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus VendorCommandGet(FtdiSafeHandle handle, byte request, [Out] byte[] buffer, ushort length);

    /// <summary>
    /// Sends a vendor command with data from the supplied buffer.
    /// </summary>
    [DllImport(NativeName, EntryPoint = "FT_VendorCmdSet", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus VendorCommandSet(FtdiSafeHandle handle, byte request, [In] byte[] buffer, ushort length);
}
