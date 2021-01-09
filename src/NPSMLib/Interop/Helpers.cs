using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NPSMLib.Interop
{
    internal struct OSVersion
    {
        private readonly ushort major;
        private readonly ushort minor;
        private readonly ushort build;
        private readonly ushort revision;

        public ushort Major { get => major; }
        public ushort Minor { get => minor; }
        public ushort Build { get => build; }
        public ushort Revision { get => revision; }

        public OSVersion(ushort major, ushort minor, ushort build, ushort revision)
        {
            this.major = major;
            this.minor = minor;
            this.build = build;
            this.revision = revision;
        }
    }

    internal class Helpers
    {
        internal static OSVersion GetOSVersion()
        {
            NativeMethods.RtlGetDeviceFamilyInfoEnum(out var version, out var b, out var c);

            return new OSVersion(
                major: (ushort)((version & 0xFFFF000000000000L) >> 48),
                minor: (ushort)((version & 0x0000FFFF00000000L) >> 32),
                build: (ushort)((version & 0x00000000FFFF0000L) >> 16),
                revision: (ushort)(version & 0x000000000000FFFFL)
            );
        }
    }
}
