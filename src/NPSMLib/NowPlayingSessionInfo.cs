using System;
using static NPSMLib.Interop.COMInterop;

namespace NPSMLib
{
    public class NowPlayingSessionInfo : IEquatable<NowPlayingSessionInfo>
    {
        private readonly ushort osbuild;
        private readonly object infoIUnknown;
        private readonly INowPlayingSessionInfo_19041 info_19041;
        private readonly INowPlayingSessionInfo_10586 info_10586;

        internal object GetIUnknownInterface { get => infoIUnknown; }

        internal NowPlayingSessionInfo(object infoIUnknown, ushort OSBuild)
        {
            this.osbuild = OSBuild;
            this.infoIUnknown = infoIUnknown;
            if (osbuild >= 19041)
                info_19041 = (INowPlayingSessionInfo_19041)infoIUnknown;
            else
                info_10586 = (INowPlayingSessionInfo_10586)infoIUnknown;
        }

        public void GetInfo(out IntPtr hWnd, out uint PID, out string DeviceId)
        {
            if (osbuild >= 19041)
                info_19041.GetInfo(out hWnd, out PID, out DeviceId);
            else
                info_10586.GetInfo(out hWnd, out PID, out DeviceId);
        }

        public bool Equals(NowPlayingSessionInfo other)
        {
            bool val;
            if (osbuild >= 19041)
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
            if (osbuild >= 19041)
                info_19041.IsRunningInContainerOS(out inContainerOS);
            return inContainerOS;
        }
    }
}
