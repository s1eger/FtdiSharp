using System.Runtime.InteropServices;

namespace FtdiSharp;

internal sealed class FtdiSafeHandle : SafeHandle
{
    /// <summary>
    /// Initializes an empty safe handle that owns the underlying FTDI handle.
    /// </summary>
    public FtdiSafeHandle() : base(IntPtr.Zero, ownsHandle: true) { }

    public override bool IsInvalid => handle == IntPtr.Zero;

    /// <summary>
    /// Closes the underlying FTDI handle and reports whether closing succeeded.
    /// </summary>
    protected override bool ReleaseHandle() => FtdiNative.Close(handle) == FtdiStatus.OK;
}
