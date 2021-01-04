using NPSMLib.Interop;
using System;
using System.Runtime.InteropServices;
using static NPSMLib.Interop.COMInterop;

namespace NPSMLib
{
    public class NowPlayingSessionManager
    {
        static Guid CLSID_NowPlayingSessionManager = new Guid("BCBB9860-C012-4AD7-A938-6E337AE6ABA5");

        INowPlayingSessionManager manager;

        /// <summary>
        /// Handles Now Playing sessions.
        /// Can throw if the underlaying COM interface fails to create
        /// </summary>
        public NowPlayingSessionManager()
        {
            Guid guid_INowPlayingSessionManager = new Guid("3b6a7908-ce07-4ba9-878c-6e4a15db5e5b"); //.NET Standard 2.0+ -> typeof(INowPlayingSessionManager).GUID;
            NativeMethods.CoCreateInstance(ref CLSID_NowPlayingSessionManager, null, 4 /* CLSCTX_LOCAL_SERVER */, ref guid_INowPlayingSessionManager, out object INowPlayingSessionManagerObj);

            manager = (INowPlayingSessionManager)INowPlayingSessionManagerObj;
            
            //TODO ?
            //manager.AddSession
        }


        /// <summary>
        /// Get the current active session
        /// </summary>
        public NowPlayingSession CurrentSession 
        { 
            get 
            {
                manager.get_CurrentSession(out var session);
                return session == null ? null : new NowPlayingSession(session);
            } 
        }

        /// <summary>
        /// Get the number of sessions
        /// </summary>
        public ulong Count
        {
            get
            {
                manager.get_Count(out ulong count);
                return count;
            }
        }

        public NowPlayingSession[] GetSessions()
        {
            manager.GetSessions(out uint count, out var sessions);
            NowPlayingSession[] array = new NowPlayingSession[count];
            for (uint i = 0; i < count; i++)
                array[i] = new NowPlayingSession(sessions[i]);
            return array;
        }

        /// <summary>
        /// Sets the next session as current.
        /// </summary>
        public void SetNextCurrentSession()
        {
            manager.SetCurrentNextSession();
        }

        public void SetCurrentSession(NowPlayingSessionInfo pInfo)
        {
            manager.SetCurrentSession(pInfo.NowPlayingSessionInfoInterface);
        }

        public void RemoveSession(NowPlayingSessionInfo pInfo)
        {
            manager.RemoveSession(pInfo.NowPlayingSessionInfoInterface);
        }

        public NowPlayingSession FindSession(NowPlayingSessionInfo pInfo)
        {
            manager.FindSession(pInfo.NowPlayingSessionInfoInterface, out var session);
            return new NowPlayingSession(session);
        }

        /// <summary>
        /// ?
        /// </summary>
        /// <param name="hWnd">Handle to the window present in any session</param>
        public void Refresh(IntPtr hWnd)
        {
            manager.Refresh(hWnd);
        }

        public void Update(bool fEnabled, IntPtr hwnd, uint dwPID, ulong unknown, MediaPlaybackDataSource pSource)
        {
            manager.Update(fEnabled, hwnd, dwPID, unknown, pSource.MediaPlaybackDataSourceInterface);
        }

        #region Event

        NowPlayingSessionManagerEventHandler eventHandler;

        int subscribers = 0;
        readonly object objectLock = new object();

        private event EventHandler<NowPlayingSessionManagerEventArgs> bSessionListChanged;
        public event EventHandler<NowPlayingSessionManagerEventArgs> SessionListChanged
        {
            add
            {
                lock (objectLock)
                {
                    if (subscribers == 0)
                    {
                        eventHandler = new NowPlayingSessionManagerEventHandler(this);
                        var akka = manager.RegisterEventHandler(eventHandler, out var token);
                        eventHandler.Token = token;
                    }
                    subscribers++;

                    bSessionListChanged += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    subscribers--;
                    if (subscribers == 0)
                    {
                        manager.UnregisterEventHandler(eventHandler.Token);
                        eventHandler = null;
                    }

                    bSessionListChanged -= value;
                }
            }
        }

        class NowPlayingSessionManagerEventHandler : INowPlayingSessionManagerEventHandler
        {
            internal lEventRegistrationToken Token { get; set; }
            private NowPlayingSessionManager CurrentSessionInstance { get; set; }
            public NowPlayingSessionManagerEventHandler(NowPlayingSessionManager currentSessionInstance)
            {
                CurrentSessionInstance = currentSessionInstance;
            }
            public void OnChange(NowPlayingSessionManagerNotificationType notificationType, INowPlayingSessionInfo pInfo, [MarshalAs(UnmanagedType.LPWStr)] string unknown)
            {
                //forward to subscribers
                CurrentSessionInstance.bSessionListChanged?.Invoke(CurrentSessionInstance, 
                    new NowPlayingSessionManagerEventArgs { NotificationType = notificationType, NowPlayingSessionInfo = new NowPlayingSessionInfo(pInfo), SessionTypeString = unknown });
            }
        }

        #endregion

    }

    public class NowPlayingSessionManagerEventArgs : EventArgs
    {
        public NowPlayingSessionManagerNotificationType NotificationType { get; internal set; }
        public NowPlayingSessionInfo NowPlayingSessionInfo { get; internal set; }
        public string SessionTypeString { get; internal set; }
    }
}
