using NPSMLib.Interop;
using System;
using static NPSMLib.Interop.COMInterop;

namespace NPSMLib
{
    public class NowPlayingSessionInfo : IEquatable<NowPlayingSessionInfo>
    {
        INowPlayingSessionInfo pInfo;

        internal INowPlayingSessionInfo NowPlayingSessionInfoInterface { get => pInfo; }

        internal NowPlayingSessionInfo(INowPlayingSessionInfo info)
        {
            pInfo = info;
        }

        public void GetInfo(out IntPtr hWnd, out uint PID, out string DeviceId)
        {
            pInfo.GetInfo(out hWnd, out PID, out DeviceId);
        }

        public bool Equals(NowPlayingSessionInfo other)
        {
            pInfo.IsEqual(other.pInfo, out bool val);
            return val;
        }

        public bool IsRunningInContainerOS()
        {
            pInfo.IsRunningInContainerOS(out bool val);
            return val;
        }
    }
}
