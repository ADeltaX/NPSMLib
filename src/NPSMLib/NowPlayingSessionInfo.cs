using System;
using static NPSMLib.Interop.COMInterop;

namespace NPSMLib
{
    public class NowPlayingSessionInfo : IEquatable<NowPlayingSessionInfo>
    {
        private readonly object infoIUnknown;
        private readonly INowPlayingSessionInfo_19041 info_19041;
        private readonly INowPlayingSessionInfo_10586 info_10586;
        private readonly int numSelectInterface = 0;

        internal object GetIUnknownInterface { get => infoIUnknown; }

        internal NowPlayingSessionInfo(object infoIUnknown)
        {
            this.infoIUnknown = infoIUnknown;

            if (infoIUnknown is INowPlayingSessionInfo_19041 tInfo_19041)
            {
                numSelectInterface = 19041;
                info_19041 = tInfo_19041;
            }
            else if (infoIUnknown is INowPlayingSessionInfo_10586 tInfo_10586)
            {
                numSelectInterface = 10586;
                info_10586 = tInfo_10586;
            }
            else
            {
                throw new NotSupportedException("QueryInterface failed due to non-available interface/guid");
            }
        }

        public void GetInfo(out IntPtr hWnd, out uint PID, out string DeviceId)
        {
            if (numSelectInterface == 19041)
                info_19041.GetInfo(out hWnd, out PID, out DeviceId);
            else
                info_10586.GetInfo(out hWnd, out PID, out DeviceId);
        }

        public bool Equals(NowPlayingSessionInfo other)
        {
            bool val;
            if (numSelectInterface == 19041)
                info_19041.IsEqual(other.infoIUnknown, out val);
            else
                info_10586.IsEqual(other.infoIUnknown, out val);
            return val;
        }

        /// <summary>
        /// 19041+
        /// </summary>
        /// <returns></returns>
        public bool IsRunningInContainerOS()
        {
            bool inContainerOS = false;
            if (numSelectInterface == 19041)
                info_19041.IsRunningInContainerOS(out inContainerOS);
            return inContainerOS;
        }
    }
}
