using System;
using static NPSMLib.Interop.COMInterop;

namespace NPSMLib
{
    /// <summary>
    /// Represents a media playback session from another application providing info about that session and possibly allowing control.
    /// </summary>
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

        /// <summary>
        /// Gets the handle of the window associated with this session's source application.
        /// </summary>
        /// <returns>An <see cref="IntPtr"/> that represents the handle of the window associated with this session's source application.</returns>
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

        /// <summary>
        /// Gets the process ID of this session's source application.
        /// </summary>
        /// <returns>The process ID of this session's source application.</returns>
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

        /// <summary>
        /// Gets the render device ID of this session's source application.
        /// </summary>
        /// <returns>The render device ID of this session's source application.</returns>
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

        /// <summary>
        /// Gets the application ID of this session's source application.
        /// </summary>
        /// <returns>The application ID of this session's source application.</returns>
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

        /// <summary>
        /// Gets the source device ID of this session's source application.
        /// </summary>
        /// <returns>The source device ID of this session's source application.</returns>
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
        /// <summary>
        /// Gets the <see cref="MediaPlaybackDataSource"/> for this session.
        /// </summary>
        /// <returns>The <see cref="MediaPlaybackDataSource"/> associated this session.</returns>
        public MediaPlaybackDataSource ActivateMediaPlaybackDataSource()
        {
            object imediaPlaybackDataSourceIUnknown;
            if (numSelectInterface == 14393)
                session_14393.ActivateMediaPlaybackDataSource(out imediaPlaybackDataSourceIUnknown);
            else
                session_10586.ActivateMediaPlaybackDataSource(out imediaPlaybackDataSourceIUnknown);
            return new MediaPlaybackDataSource(imediaPlaybackDataSourceIUnknown);
        }

        /// <summary>
        /// Gets the type of this session.
        /// </summary>
        /// <returns>A <see cref="NowPlayingSessionType"/> that represents the type of this session.</returns>
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
        /// <summary>
        /// Gets the <see cref="NowPlayingSessionInfo"/> for this session.
        /// </summary>
        /// <returns>The <see cref="NowPlayingSessionInfo"/> associated this session.</returns>
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
