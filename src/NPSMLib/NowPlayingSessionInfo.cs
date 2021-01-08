using System;
using static NPSMLib.Interop.COMInterop;

namespace NPSMLib
{
    /// <summary>
    /// Represents the information associated with a <see cref="NowPlayingSession"/>.
    /// </summary>
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

        /// <summary>
        /// Gets the information associated with the session.
        /// </summary>
        /// <param name="hWnd">The window handle associated with the session's source application.</param>
        /// <param name="PID">The process ID of the session's source application.</param>
        /// <param name="DeviceId">The device ID of the session's source application.</param>
        public void GetInfo(out IntPtr hWnd, out uint PID, out string DeviceId)
        {
            if (numSelectInterface == 19041)
                info_19041.GetInfo(out hWnd, out PID, out DeviceId);
            else
                info_10586.GetInfo(out hWnd, out PID, out DeviceId);
        }

        /// <inheritdoc/>
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
        /// Gets whether the session is running in a container OS.
        /// Only supported for Windows 10 19041+
        /// </summary>
        /// <returns>A <see cref="bool"/> that describes whether the session is running in a container OS or not.</returns>
        public bool IsRunningInContainerOS()
        {
            bool inContainerOS = false;
            if (numSelectInterface == 19041)
                info_19041.IsRunningInContainerOS(out inContainerOS);
            return inContainerOS;
        }
    }
}
