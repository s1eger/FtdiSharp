using System.Runtime.InteropServices;

namespace FtdiSharp;

internal static class FtdiNative
{
    public const string NativeName = "ftd2xx";

    private const CallingConvention Convention = CallingConvention.StdCall;


    [DllImport(NativeName, EntryPoint = "FT_CreateDeviceInfoList", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus CreateDeviceInfoList(out uint deviceCount);

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

    [DllImport(NativeName, EntryPoint = "FT_Open", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus Open(uint index, out FtdiSafeHandle handle);

    [DllImport(NativeName, EntryPoint = "FT_OpenEx", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus OpenEx(string parameter, FtdiOpenFlags flags, out FtdiSafeHandle handle);

    [DllImport(NativeName, EntryPoint = "FT_OpenEx", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus OpenEx(uint location, FtdiOpenFlags flags, out FtdiSafeHandle handle);

    [DllImport(NativeName, EntryPoint = "FT_Close", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus Close(IntPtr handle);

    [DllImport(NativeName, EntryPoint = "FT_Read", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus Read(FtdiSafeHandle handle, [Out] byte[] buffer, uint bytesToRead, out uint bytesRead);

    [DllImport(NativeName, EntryPoint = "FT_Write", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus Write(FtdiSafeHandle handle, [In] byte[] buffer, uint bytesToWrite, out uint bytesWritten);

    [DllImport(NativeName, EntryPoint = "FT_GetQueueStatus", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus GetQueueStatus(FtdiSafeHandle handle, out uint rxQueueBytes);

    [DllImport(NativeName, EntryPoint = "FT_GetStatus", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus GetStatus(FtdiSafeHandle handle, out uint rxQueueBytes, out uint txQueueBytes, out FtdiEventFlags eventStatus);

    [DllImport(NativeName, EntryPoint = "FT_GetModemStatus", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus GetModemStatus(FtdiSafeHandle handle, out FtdiModemStatus modemStatus);

    [DllImport(NativeName, EntryPoint = "FT_SetBaudRate", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetBaudRate(FtdiSafeHandle handle, uint baudRate);

    [DllImport(NativeName, EntryPoint = "FT_SetDataCharacteristics", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetDataCharacteristics(FtdiSafeHandle handle, FtdiDataBits dataBits, FtdiStopBits stopBits, FtdiParity parity);

    [DllImport(NativeName, EntryPoint = "FT_SetFlowControl", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetFlowControl(FtdiSafeHandle handle, FtdiFlowControl flowControl, byte xon, byte xoff);

    [DllImport(NativeName, EntryPoint = "FT_SetDtr", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetDtr(FtdiSafeHandle handle);

    [DllImport(NativeName, EntryPoint = "FT_ClrDtr", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus ClearDtr(FtdiSafeHandle handle);

    [DllImport(NativeName, EntryPoint = "FT_SetRts", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetRts(FtdiSafeHandle handle);

    [DllImport(NativeName, EntryPoint = "FT_ClrRts", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus ClearRts(FtdiSafeHandle handle);

    [DllImport(NativeName, EntryPoint = "FT_SetTimeouts", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetTimeouts(FtdiSafeHandle handle, uint readTimeOut, uint writeTimeout);

    [DllImport(NativeName, EntryPoint = "FT_Purge", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus Purge(FtdiSafeHandle handle, FtdiPurgeFlags mask);

    [DllImport(NativeName, EntryPoint = "FT_ResetDevice", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus ResetDevice(FtdiSafeHandle handle);

    [DllImport(NativeName, EntryPoint = "FT_ResetPort", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus ResetPort(FtdiSafeHandle handle);

    [DllImport(NativeName, EntryPoint = "FT_CyclePort", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus CyclePort(FtdiSafeHandle handle);

    [DllImport(NativeName, EntryPoint = "FT_Rescan", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus Rescan();

    [DllImport(NativeName, EntryPoint = "FT_Reload", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus Reload(ushort vendorId, ushort productId);

    [DllImport(NativeName, EntryPoint = "FT_StopInTask", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus StopInTask(FtdiSafeHandle handle);

    [DllImport(NativeName, EntryPoint = "FT_RestartInTask", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus RestartInTask(FtdiSafeHandle handle);

    [DllImport(NativeName, EntryPoint = "FT_SetBreakOn", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetBreakOn(FtdiSafeHandle handle);

    [DllImport(NativeName, EntryPoint = "FT_SetBreakOff", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetBreakOff(FtdiSafeHandle handle);

    [DllImport(NativeName, EntryPoint = "FT_SetChars", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetChars(FtdiSafeHandle handle, byte eventCharacter, byte eventCharacterEnabled, byte errorCharacter, byte errorCharacterEnabled);

    [DllImport(NativeName, EntryPoint = "FT_SetResetPipeRetryCount", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetResetPipeRetryCount(FtdiSafeHandle handle, uint count);

    [DllImport(NativeName, EntryPoint = "FT_SetDeadmanTimeout", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetDeadmanTimeout(FtdiSafeHandle handle, uint timeoutMilliseconds);

    [DllImport(NativeName, EntryPoint = "FT_SetLatencyTimer", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetLatencyTimer(FtdiSafeHandle handle, byte latencyMilliseconds);

    [DllImport(NativeName, EntryPoint = "FT_GetLatencyTimer", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus GetLatencyTimer(FtdiSafeHandle handle, out byte latencyMilliseconds);

    [DllImport(NativeName, EntryPoint = "FT_SetBitMode", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetBitMode(FtdiSafeHandle handle, byte mask, FtdiBitMode mode);

    [DllImport(NativeName, EntryPoint = "FT_GetBitMode", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus GetBitMode(FtdiSafeHandle handle, out byte pinStates);

    [DllImport(NativeName, EntryPoint = "FT_SetUSBParameters", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus SetUsbParameters(FtdiSafeHandle handle, uint inTransferSize, uint outTransferSize);

    [DllImport(NativeName, EntryPoint = "FT_GetDriverVersion", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus GetDriverVersion(FtdiSafeHandle handle, out uint version);

    [DllImport(NativeName, EntryPoint = "FT_GetLibraryVersion", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus GetLibraryVersion(out uint version);

    [DllImport(NativeName, EntryPoint = "FT_GetComPortNumber", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus GetComPortNumber(FtdiSafeHandle handle, out int comPortNumber);

    [DllImport(NativeName, EntryPoint = "FT_ReadEE", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus ReadEeprom(FtdiSafeHandle handle, uint wordOffset, out ushort value);

    [DllImport(NativeName, EntryPoint = "FT_WriteEE", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus WriteEeprom(FtdiSafeHandle handle, uint wordOffset, ushort value);

    [DllImport(NativeName, EntryPoint = "FT_EraseEE", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus EraseEeprom(FtdiSafeHandle handle);

    [DllImport(NativeName, EntryPoint = "FT_EE_UASize", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus GetEepromUserAreaSize(FtdiSafeHandle handle, out uint size);

    [DllImport(NativeName, EntryPoint = "FT_EE_UARead", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus ReadEepromUserArea(FtdiSafeHandle handle, [Out] byte[] buffer, int length, out uint bytesRead);

    [DllImport(NativeName, EntryPoint = "FT_EE_UAWrite", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus WriteEepromUserArea(FtdiSafeHandle handle, [In] byte[] buffer, int length);

    [DllImport(NativeName, EntryPoint = "FT_VendorCmdGet", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus VendorCommandGet(FtdiSafeHandle handle, byte request, [Out] byte[] buffer, ushort length);

    [DllImport(NativeName, EntryPoint = "FT_VendorCmdSet", ExactSpelling = true, CallingConvention = Convention)]
    internal static extern FtdiStatus VendorCommandSet(FtdiSafeHandle handle, byte request, [In] byte[] buffer, ushort length);
}
