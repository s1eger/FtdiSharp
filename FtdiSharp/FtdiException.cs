namespace FtdiSharp;

public class FtdiException : Exception
{
    public FtdiStatus Status { get; }

    public FtdiException(FtdiStatus status) : base($"{status}")
    {
        Status = status;
    }
}
