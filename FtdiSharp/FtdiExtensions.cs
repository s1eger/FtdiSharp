namespace FtdiSharp;

internal static class FtdiExtensions
{
    /// <summary>
    /// Throws an <see cref="FtdiException"/> when the status is not <see cref="FtdiStatus.OK"/>.
    /// </summary>
    public static void ThrowIfNotOk(this FtdiStatus status)
    {
        if (status is not FtdiStatus.OK)
            throw new FtdiException(status);
    }

    /// <summary>
    /// Returns the part of the string before the first occurrence of the specified character.
    /// </summary>
    public static string Cut(this string context, char symbol) =>
        string.Concat(context.TakeWhile(x => x != symbol));
}
