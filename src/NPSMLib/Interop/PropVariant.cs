using System;
using System.Runtime.InteropServices;

namespace NPSMLib.Interop
{
    internal struct PROPERTYKEY
    {
        public Guid fmtid;
        public uint pid;
    }

    internal enum VARTYPE : short
    {
        VT_EMPTY = 0,
        VT_NULL = 1,
        VT_I2 = 2,
        VT_I4 = 3,
        VT_R4 = 4,
        VT_R8 = 5,
        VT_CY = 6,
        VT_DATE = 7,
        VT_BSTR = 8,
        VT_DISPATCH = 9,
        VT_ERROR = 10,
        VT_BOOL = 11,
        VT_VARIANT = 12,
        VT_UNKNOWN = 13,
        VT_DECIMAL = 14,
        VT_I1 = 16,
        VT_UI1 = 17,
        VT_UI2 = 18,
        VT_UI4 = 19,
        VT_I8 = 20,
        VT_UI8 = 21,
        VT_INT = 22,
        VT_UINT = 23,
        VT_VOID = 24,
        VT_HRESULT = 25,
        VT_PTR = 26,
        VT_SAFEARRAY = 27,
        VT_CARRAY = 28,
        VT_USERDEFINED = 29,
        VT_LPSTR = 30,
        VT_LPWSTR = 31,
        VT_RECORD = 36,
        VT_INT_PTR = 37,
        VT_UINT_PTR = 38,
        VT_FILETIME = 64,
        VT_BLOB = 65,
        VT_STREAM = 66,
        VT_STORAGE = 67,
        VT_STREAMED_OBJECT = 68,
        VT_STORED_OBJECT = 69,
        VT_BLOB_OBJECT = 70,
        VT_CF = 71,
        VT_CLSID = 72,
        VT_VERSIONED_STREAM = 73,
        VT_BSTR_BLOB = 0xfff,
        VT_VECTOR = 0x1000,
        VT_ARRAY = 0x2000,
        VT_BYREF = 0x4000,
        VT_RESERVED = unchecked((short)0x8000),
        VT_ILLEGAL = unchecked((short)0xffff),
        VT_ILLEGALMASKED = 0xfff,
        VT_TYPEMASK = 0xfff
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CAC
    {
        internal uint cElems;
        internal IntPtr pElems; //sbyte
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CAUB
    {
        internal uint cElems;
        internal IntPtr pElems; //byte
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CAI
    {
        internal uint cElems;
        internal IntPtr pElems; //short
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CAUI
    {
        internal uint cElems;
        internal IntPtr pElems; //ushort
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CAL
    {
        internal uint cElems;
        internal IntPtr pElems; //int
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CAUL
    {
        internal uint cElems;
        internal IntPtr pElems; //uint
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CAFLT
    {
        internal uint cElems;
        internal IntPtr pElems; //float
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CADBL
    {
        internal uint cElems;
        internal IntPtr pElems; //double
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CACY
    {
        internal uint cElems;
        internal IntPtr pElems; //CY
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CADATE
    {
        internal uint cElems;
        internal IntPtr pElems; //double (date)
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CABSTR
    {
        internal uint cElems;
        internal IntPtr pElems; //string (bstr)
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CABSTRBLOB
    {
        internal uint cElems;
        internal IntPtr pElems; //BSTRBLOB
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CABOOL
    {
        internal uint cElems;
        internal IntPtr pElems; //short (BOOL)
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CASCODE
    {
        internal uint cElems;
        internal IntPtr pElems; //int (SCODE)
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CAPROPVARIANT
    {
        internal uint cElems;
        internal IntPtr pElems; //PROPVARIANT
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CAH
    {
        internal uint cElems;
        internal IntPtr pElems; //long
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CAUH
    {
        internal uint cElems;
        internal IntPtr pElems; //ulong
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CALPSTR
    {
        internal uint cElems;
        internal IntPtr pElems; //string (str)
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CALPWSTR
    {
        internal uint cElems;
        internal IntPtr pElems; //string (wstr)
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CAFILETIME
    {
        internal uint cElems;
        internal IntPtr pElems; //System.Runtime.InteropServices.ComTypes.FILETIME
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CACLIPDATA
    {
        internal uint cElems;
        internal IntPtr pElems; //CLIPDATA
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CACLSID
    {
        internal uint cElems;
        internal IntPtr pElems; //guid (CLSID)
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CLIPDATA
    {
        internal uint cbSize;
        internal int ulClipFmt;
        internal IntPtr pClipData; //byte
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct CY
    {
        public uint Lo;
        public int Hi;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct BSTRBLOB
    {
        public uint cbSize;
        public IntPtr pData;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct BLOB
    {
        public uint cbSize;
        public IntPtr pBlobData;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct CArray
    {
        public uint cElems;
        public IntPtr pElems;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct PROPVARIANTUNION
    {
        [FieldOffset(0)]
        internal sbyte cVal;

        [FieldOffset(0)]
        internal byte bVal;

        [FieldOffset(0)]
        internal short iVal;

        [FieldOffset(0)]
        internal ushort uiVal;

        [FieldOffset(0)]
        internal int lVal;

        [FieldOffset(0)]
        internal uint ulVal;

        [FieldOffset(0)]
        internal int intVal;

        [FieldOffset(0)]
        internal uint uintVal;

        [FieldOffset(0)]
        internal long hVal;

        [FieldOffset(0)]
        internal ulong uhVal;

        [FieldOffset(0)]
        internal float fltVal;

        [FieldOffset(0)]
        internal double dblVal;

        [FieldOffset(0)]
        internal short boolVal;

        [FieldOffset(0)]
        internal int scode;

        [FieldOffset(0)]
        internal CY cyVal;

        [FieldOffset(0)]
        internal double date; //yes, it's double

        [FieldOffset(0)]
        internal System.Runtime.InteropServices.ComTypes.FILETIME filetime;

        [FieldOffset(0)]
        internal IntPtr puuid; //guid (CLSID)

        [FieldOffset(0)]
        internal IntPtr pclipdata; //CLIPDATA

        [FieldOffset(0)]
        internal IntPtr bstrVal;

        [FieldOffset(0)]
        internal BSTRBLOB bstrblobVal;

        [FieldOffset(0)]
        internal BLOB blob;

        [FieldOffset(0)]
        internal IntPtr pszVal;

        [FieldOffset(0)]
        internal IntPtr pwszVal;

        [FieldOffset(0)]
        internal IntPtr punkVal;

        [FieldOffset(0)]
        internal IntPtr pdispVal;

        [FieldOffset(0)]
        internal IntPtr pStream;

        [FieldOffset(0)]
        internal IntPtr pStorage;

        [FieldOffset(0)]
        internal IntPtr pVersionedStream;

        [FieldOffset(0)]
        internal IntPtr parray;

        [FieldOffset(0)]
        internal CAC cac;

        [FieldOffset(0)]
        internal CAUB caub;

        [FieldOffset(0)]
        internal CAI cai;

        [FieldOffset(0)]
        internal CAUI caui;

        [FieldOffset(0)]
        internal CAL cal;

        [FieldOffset(0)]
        internal CAUL caul;

        [FieldOffset(0)]
        internal CAH cah;

        [FieldOffset(0)]
        internal CAUH cauh;

        [FieldOffset(0)]
        internal CAFLT caflt;

        [FieldOffset(0)]
        internal CADBL cadbl;

        [FieldOffset(0)]
        internal CABOOL cabool;

        [FieldOffset(0)]
        internal CASCODE cascode;

        [FieldOffset(0)]
        internal CACY cacy;

        [FieldOffset(0)]
        internal CADATE cadate;

        [FieldOffset(0)]
        internal CAFILETIME cafiletime;

        [FieldOffset(0)]
        internal CACLSID cauuid;

        [FieldOffset(0)]
        internal CACLIPDATA caclipdata;

        [FieldOffset(0)]
        internal CABSTR cabstr;

        [FieldOffset(0)]
        internal CABSTRBLOB cabstrblob;

        [FieldOffset(0)]
        internal CALPSTR calpstr;

        [FieldOffset(0)]
        internal CALPWSTR calpwstr;

        [FieldOffset(0)]
        internal CAPROPVARIANT capropvar;

        [FieldOffset(0)]
        internal CArray cArray;

        [FieldOffset(0)]
        internal IntPtr pcVal;

        [FieldOffset(0)]
        internal IntPtr pbVal;

        [FieldOffset(0)]
        internal IntPtr piVal;

        [FieldOffset(0)]
        internal IntPtr puiVal;

        [FieldOffset(0)]
        internal IntPtr plVal;

        [FieldOffset(0)]
        internal IntPtr pulVal;

        [FieldOffset(0)]
        internal IntPtr pintVal;

        [FieldOffset(0)]
        internal IntPtr puintVal;

        [FieldOffset(0)]
        internal IntPtr pfltVal;

        [FieldOffset(0)]
        internal IntPtr pdblVal;

        [FieldOffset(0)]
        internal IntPtr pboolVal;

        [FieldOffset(0)]
        internal IntPtr pdecVal;

        [FieldOffset(0)]
        internal IntPtr pscode;

        [FieldOffset(0)]
        internal IntPtr pcyVal;

        [FieldOffset(0)]
        internal IntPtr pdate;

        [FieldOffset(0)]
        internal IntPtr pbstrVal;

        [FieldOffset(0)]
        internal IntPtr ppunkVal;

        [FieldOffset(0)]
        internal IntPtr ppdispVal;

        [FieldOffset(0)]
        internal IntPtr pparray;

        [FieldOffset(0)]
        internal IntPtr pvarVal;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct PROPVARIANT
    {
        internal VARTYPE vt;
        internal ushort wReserved1;
        internal ushort wReserved2;
        internal ushort wReserved3;
        internal PROPVARIANTUNION union;
    }
}
