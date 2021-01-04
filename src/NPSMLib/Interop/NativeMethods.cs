using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NPSMLib.Interop
{
    internal static class NativeMethods
    {
        [DllImport("ole32.Dll", PreserveSig = false, SetLastError = true)]
        internal static extern void CoCreateInstance(ref Guid clsid, [MarshalAs(UnmanagedType.IUnknown)] object inner,
            uint context, ref Guid uuid, [MarshalAs(UnmanagedType.IUnknown)] out object rReturnedComObject);

        [DllImport("ole32.dll")]
        internal extern static int PropVariantClear(ref PROPVARIANT propvar);
    }
}
