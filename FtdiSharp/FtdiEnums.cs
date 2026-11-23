namespace FtdiSharp;


public enum FtdiStatus : uint
{
    OK,
    INVALID_HANDLE,
    DEVICE_NOT_FOUND,
    DEVICE_NOT_OPENED,
    IO_ERROR,
    INSUFFICIENT_RESOURCES,
    INVALID_PARAMETER,
    INVALID_BAUD_RATE,
    DEVICE_NOT_OPENED_FOR_ERASE,
    DEVICE_NOT_OPENED_FOR_WRITE,
    FAILED_TO_WRITE_DEVICE,
    EEPROM_READ_FAILED,
    EEPROM_WRITE_FAILED,
    EEPROM_ERASE_FAILED,
    EEPROM_NOT_PRESENT,
    EEPROM_NOT_PROGRAMMED,
    INVALID_ARGS,
    OTHER_ERROR
}

public enum FtdiDeviceType : uint
{
    BM = 0,
    AM = 1,
    DEVICE_100AX = 2,
    DEVICE_UNKNOWN = 3,
    DEVICE_2232 = 4,
    DEVICE_232R = 5,
    DEVICE_2232H = 6,
    DEVICE_4232H = 7,
    DEVICE_232H = 8,
    X_SERIES = 9,
    DEVICE_4222H0 = 10,
    DEVICE_4222H12 = 11,
    DEVICE_4222H3 = 12,
    DEVICE_4222_PROG = 13,
    FT900 = 14,
    FT930 = 15,
    UMFTPD3A = 16,
    DEVICE_2233HP = 17,
    DEVICE_4233HP = 18,
    DEVICE_2232HP = 19,
    DEVICE_4232HP = 20,
    DEVICE_233HP = 21,
    DEVICE_232HP = 22,
    DEVICE_2232HA = 23,
    DEVICE_4232HA = 24,
}

[Flags]
public enum FtdiDeviceFlags : uint
{
    NONE = 0,
    OPENED = 1,
    HISPEED = 2,
}


[Flags]
internal enum FtdiOpenFlags : uint
{
    SerialNumber = 1,
    Description = 2,
    Location = 4,
}

public enum FtdiDataBits : byte
{
    BITS_7 = 7,
    BITS_8 = 8,
}

public enum FtdiStopBits : byte
{
    ONE = 0,
    TWO = 2,
}

public enum FtdiParity : byte
{
    NONE = 0,
    ODD = 1,
    EVEN = 2,
    MARK = 3,
    SPACE = 4,
}

public enum FtdiFlowControl : ushort
{
    NONE = 0x0000,
    RTS_CTS = 0x0100,
    DTR_DSR = 0x0200,
    XON_XOFF = 0x0400,
}

[Flags]
public enum FtdiPurgeFlags : byte
{
    RX = 1,
    TX = 2,
    BOTH = RX | TX,
}

public enum FtdiBitMode : byte
{
    /// <summary>
    /// Reset bit mode
    /// </summary>
    RESET = 0x00,

    /// <summary>
    /// Asynchronous bit-bang mode
    /// </summary>
    ASYNC_BITBANG = 0x01,

    /// <summary>
    /// MPSSE bit mode - only available on FT2232, FT2232H, FT4232H and FT232H
    /// </summary>
    MPSSE = 0x02,

    /// <summary>
    /// Synchronous bit-bang mode
    /// </summary>
    SYNC_BITBANG = 0x04,

    /// <summary>
    /// MCU host bus emulation mode - only available on FT2232, FT2232H, FT4232H and
    /// FT232H
    /// </summary>
    MCU_HOST = 0x08,

    /// <summary>
    /// Fast opto-isolated serial mode - only available on FT2232, FT2232H, FT4232H and
    /// FT232H
    /// </summary>
    FAST_SERIAL = 0x10,

    /// <summary>
    /// CBUS bit-bang mode - only available on FT232R and FT232H
    /// </summary>
    CBUS_BITBANG = 0x20,

    /// <summary>
    /// Single channel synchronous 245 FIFO mode - only available on FT2232H channel
    /// A and FT232H
    /// </summary>
    SYNC_FIFO = 0x40,
}

[Flags]
public enum FtdiModemStatus : uint
{
    NONE = 0,

    /// <summary>
    /// Clear To Send (CTS) modem status
    /// </summary>
    CTS = 0x10,

    /// <summary>
    /// Data Set Ready (DSR) modem status
    /// </summary>
    DSR = 0x20,

    /// <summary>
    /// Ring Indicator (RI) modem status
    /// </summary>
    DI = 0x40,

    /// <summary>
    /// Data Carrier Detect (DCD) modem status
    /// </summary>
    DCD = 0x80,

    /// <summary>
    /// Overrun Error (OE) line status
    /// </summary>
    OE = 0x0200,

    /// <summary>
    /// Parity Error (PE) line status
    /// </summary>
    PE = 0x0400,

    /// <summary>
    /// Framing Error (FE) line status
    /// </summary>
    FE = 0x0800,

    /// <summary>
    /// Break Interrupt (BI) line status
    /// </summary>
    BI = 0x1000,
}

[Flags]
public enum FtdiEventFlags : uint
{
    NONE = 0,

    /// <summary>
    /// Event on receive character
    /// </summary>
    RXCHAR = 1,

    /// <summary>
    /// Event on modem status change
    /// </summary>
    MODEM_STATUS = 2,

    /// <summary>
    /// Event on line status change
    /// </summary>
    LINE_STATUS = 4,
}
