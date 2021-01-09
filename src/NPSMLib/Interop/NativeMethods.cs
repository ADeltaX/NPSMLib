using System;
using System.Runtime.InteropServices;

namespace NPSMLib.Interop
{
    internal static class NativeMethods
    {
        [DllImport("ole32.dll", PreserveSig = true, SetLastError = true)]
        internal static extern int CoCreateInstance(ref Guid clsid, [MarshalAs(UnmanagedType.IUnknown)] object inner,
            uint context, ref Guid uuid, [MarshalAs(UnmanagedType.IUnknown)] out object rReturnedComObject);

        [DllImport("ole32.dll")]
        internal extern static int PropVariantClear(ref PROPVARIANT propvar);

        [DllImport("ntdll.dll", PreserveSig = true)]
        internal extern static void RtlGetDeviceFamilyInfoEnum(out ulong pullUAPInfo, out uint pulDeviceFamily, out uint pulDeviceForm);
    }
}
