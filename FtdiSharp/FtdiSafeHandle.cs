using System.Runtime.InteropServices;

namespace FtdiSharp;

internal sealed class FtdiSafeHandle : SafeHandle
{
    public FtdiSafeHandle() : base(IntPtr.Zero, ownsHandle: true) { }

    public override bool IsInvalid => handle == IntPtr.Zero;
    protected override bool ReleaseHandle() => FtdiNative.Close(handle) == FtdiStatus.OK;
}
