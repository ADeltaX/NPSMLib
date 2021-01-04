using NPSMLib.Interop;
using System;
using System.Collections.Generic;
using System.Text;
using static NPSMLib.Interop.COMInterop;

namespace NPSMLib
{
    public class NowPlayingSession
    {
        INowPlayingSession session;
        internal NowPlayingSession(INowPlayingSession session)
        {
            this.session = session;
        }

        public IntPtr Hwnd 
        { 
            get 
            {
                session.get_HWND(out IntPtr hWnd);
                return hWnd;
            } 
        }
        public uint PID
        {
            get
            {
                session.get_PID(out uint pid);
                return pid;
            }
        }
        public string RenderDeviceId
        {
            get
            {
                session.get_RenderDeviceId(out string id);
                return id;
            }
        }
        public string SourceAppId
        {
            get
            {
                session.get_SourceAppId(out string id);
                return id;
            }
        }
        public string SourceDeviceId
        {
            get
            {
                session.get_SourceDeviceId(out string id);
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
                session.get_Connection(out var iunknown);
                return iunknown;
            }
        }

        public MediaPlaybackDataSource ActivateMediaPlaybackDataSource()
        {
            session.ActivateMediaPlaybackDataSource(out var t);
            return new MediaPlaybackDataSource(t);
        }

        public NowPlayingSessionType GetSessionType()
        {
            session.get_SessionType(out NowPlayingSessionType pType);
            return pType;
        }

        public NowPlayingSessionInfo GetSessionInfo()
        {
            session.get_Info(out INowPlayingSessionInfo pInfo);
            return new NowPlayingSessionInfo(pInfo);
        }


        /// <summary>
        /// ?
        /// </summary>
        /// <returns>Returns an IUnknown object (token)</returns>
        public object BeginInteractionWithSession()
        {
            session.BeginInteractionWithSession(out var token);
            return token;
        }
    }
}
