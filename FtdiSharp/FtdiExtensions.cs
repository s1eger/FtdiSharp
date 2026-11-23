namespace FtdiSharp;

internal static class FtdiExtensions
{
    public static void ThrowIfNotOk(this FtdiStatus status)
    {
        if (status is not FtdiStatus.OK)
            throw new FtdiException(status);
    }

    public static string Cut(this string context, char symbol) =>
        string.Concat(context.TakeWhile(x => x != symbol));
}
