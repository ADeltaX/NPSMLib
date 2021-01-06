using System;
using static NPSMLib.Interop.COMInterop;

namespace NPSMLib
{
    public class NowPlayingSession
    {
        private readonly ushort osbuild;
        private readonly object sessionIUnknown;
        private readonly INowPlayingSession_10586 session_10586;
        private readonly INowPlayingSession_14393 session_14393;

        internal NowPlayingSession(object sessionIUnknown, ushort OSBuild)
        {
            this.osbuild = OSBuild;
            this.sessionIUnknown = sessionIUnknown;

            if (osbuild >= 14393)
                session_14393 = (INowPlayingSession_14393)sessionIUnknown;
            else
                session_10586 = (INowPlayingSession_10586)sessionIUnknown;
        }

        public IntPtr Hwnd 
        { 
            get 
            {
                IntPtr hWnd;
                if (osbuild >= 14393)
                    session_14393.get_HWND(out hWnd);
                else
                    session_10586.get_HWND(out hWnd);
                return hWnd;
            } 
        }
        public uint PID
        {
            get
            {
                uint pid;
                if (osbuild >= 14393)
                    session_14393.get_PID(out pid);
                else
                    session_10586.get_PID(out pid);
                return pid;
            }
        }
        public string RenderDeviceId
        {
            get
            {
                string id;
                if (osbuild >= 14393)
                    session_14393.get_RenderDeviceId(out id);
                else
                    session_10586.get_RenderDeviceId(out id);
                return id;
            }
        }
        public string SourceAppId
        {
            get
            {
                string id;
                if (osbuild >= 14393)
                    session_14393.get_SourceAppId(out id);
                else
                    session_10586.get_SourceAppId(out id);
                return id;
            }
        }
        public string SourceDeviceId
        {
            get
            {
                string id;
                if (osbuild >= 14393)
                    session_14393.get_SourceDeviceId(out id);
                else
                    session_10586.get_SourceDeviceId(out id);
                return id;
            }
        }

        /// <summary>
        /// Unknown.
        /// </summary>
        public object Connection
        {
            get
            {
                object iUnknown;
                if (osbuild >= 14393)
                    session_14393.get_Connection(out iUnknown);
                else
                    session_10586.get_Connection(out iUnknown);
                return iUnknown;
            }
        }

        public MediaPlaybackDataSource ActivateMediaPlaybackDataSource()
        {
            object imediaPlaybackDataSourceIUnknown;
            if (osbuild >= 14393)
                session_14393.ActivateMediaPlaybackDataSource(out imediaPlaybackDataSourceIUnknown);
            else
                session_10586.ActivateMediaPlaybackDataSource(out imediaPlaybackDataSourceIUnknown);
            return new MediaPlaybackDataSource(imediaPlaybackDataSourceIUnknown, osbuild);
        }

        public NowPlayingSessionType GetSessionType()
        {
            NowPlayingSessionType pType;
            if (osbuild >= 14393)
                session_14393.get_SessionType(out pType);
            else
                session_10586.get_SessionType(out pType);
            return pType;
        }

        public NowPlayingSessionInfo GetSessionInfo()
        {
            object pInfoIUnknown;
            if (osbuild >= 14393)
                session_14393.get_Info(out pInfoIUnknown);
            else
                session_10586.get_Info(out pInfoIUnknown);
            return new NowPlayingSessionInfo(pInfoIUnknown, osbuild);
        }


        /// <summary>
        /// Unknown, 14393+
        /// </summary>
        /// <returns>Returns an IUnknown object (token)</returns>
        public object BeginInteractionWithSession()
        {
            object IUnknownToken = null;
            if (osbuild >= 14393)
                session_14393.BeginInteractionWithSession(out IUnknownToken);

            return IUnknownToken;
        }
    }
}
