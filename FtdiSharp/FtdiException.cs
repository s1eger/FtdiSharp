namespace FtdiSharp;

public class FtdiException : Exception
{
    public FtdiStatus Status { get; }

    /// <summary>
    /// Initializes a new exception for the specified FTDI status.
    /// </summary>
    public FtdiException(FtdiStatus status) : base($"{status}")
    {
        Status = status;
    }
}
