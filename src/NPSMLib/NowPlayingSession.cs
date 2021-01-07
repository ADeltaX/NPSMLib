using System;
using static NPSMLib.Interop.COMInterop;

namespace NPSMLib
{
    public class NowPlayingSession
    {
        private readonly object sessionIUnknown;
        private readonly INowPlayingSession_14393 session_14393;
        private readonly INowPlayingSession_10586 session_10586;
        private readonly int numSelectInterface = 0;

        internal NowPlayingSession(object sessionIUnknown)
        {
            this.sessionIUnknown = sessionIUnknown;

            if (sessionIUnknown is INowPlayingSession_14393 tSession_14393)
            {
                numSelectInterface = 14393;
                session_14393 = tSession_14393;
            }
            else if (sessionIUnknown is INowPlayingSession_10586 tSession_10586)
            {
                numSelectInterface = 10586;
                session_10586 = tSession_10586;
            }
            else
            {
                throw new NotSupportedException("QueryInterface failed due to non-available interface/guid");
            }
        }

        public IntPtr Hwnd 
        { 
            get 
            {
                IntPtr hWnd;
                if (numSelectInterface == 14393)
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
                if (numSelectInterface == 14393)
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
                if (numSelectInterface == 14393)
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
                if (numSelectInterface == 14393)
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
                if (numSelectInterface == 14393)
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
                if (numSelectInterface == 14393)
                    session_14393.get_Connection(out iUnknown);
                else
                    session_10586.get_Connection(out iUnknown);
                return iUnknown;
            }
        }

        //TODO: Can throw if fail
        public MediaPlaybackDataSource ActivateMediaPlaybackDataSource()
        {
            object imediaPlaybackDataSourceIUnknown;
            if (numSelectInterface == 14393)
                session_14393.ActivateMediaPlaybackDataSource(out imediaPlaybackDataSourceIUnknown);
            else
                session_10586.ActivateMediaPlaybackDataSource(out imediaPlaybackDataSourceIUnknown);
            return new MediaPlaybackDataSource(imediaPlaybackDataSourceIUnknown);
        }

        public NowPlayingSessionType GetSessionType()
        {
            NowPlayingSessionType pType;
            if (numSelectInterface == 14393)
                session_14393.get_SessionType(out pType);
            else
                session_10586.get_SessionType(out pType);
            return pType;
        }

        //TODO: Can throw if fail
        public NowPlayingSessionInfo GetSessionInfo()
        {
            object pInfoIUnknown;
            if (numSelectInterface == 14393)
                session_14393.get_Info(out pInfoIUnknown);
            else
                session_10586.get_Info(out pInfoIUnknown);
            return new NowPlayingSessionInfo(pInfoIUnknown);
        }


        /// <summary>
        /// Unknown, 14393+
        /// </summary>
        /// <returns>Returns an IUnknown object (token)</returns>
        public object BeginInteractionWithSession()
        {
            object IUnknownToken = null;
            if (numSelectInterface == 14393)
                session_14393.BeginInteractionWithSession(out IUnknownToken);

            return IUnknownToken;
        }
    }
}
