using NPSMLib.Interop;
using System;
using System.Runtime.InteropServices;
using static NPSMLib.Interop.COMInterop;

namespace NPSMLib
{
    public class NowPlayingSessionManager
    {
        private readonly object sessionManagerIUnknown;
        //cache to prevent calling QueryInterface each time it casts
        private readonly INowPlayingSessionManager_19041 sessionManager_19041;
        private readonly INowPlayingSessionManager_10586 sessionManager_10586;
        private readonly int numSelectInterface = 0;

        /// <summary>
        /// Handles Now Playing sessions.
        /// Can throw if the underlaying COM interface fails to create
        /// </summary>
        public NowPlayingSessionManager()
        {
            Guid guid_INowPlayingSessionManager_19041 = new Guid("3b6a7908-ce07-4ba9-878c-6e4a15db5e5b");
            Guid guid_INowPlayingSessionManager_10586 = new Guid("A7052211-8B56-43C4-8F26-12852F7303A3");

            if (NativeMethods.CoCreateInstance(ref CLSID_NowPlayingSessionManager, null, 4 | 1024 /* CLSCTX_LOCAL_SERVER | CLSCTX_NO_CODE_DOWNLOAD */, ref guid_INowPlayingSessionManager_19041, out sessionManagerIUnknown) == 0)
            {
                numSelectInterface = 19041;
                sessionManager_19041 = (INowPlayingSessionManager_19041)sessionManagerIUnknown;
            }
            else if (NativeMethods.CoCreateInstance(ref CLSID_NowPlayingSessionManager, null, 4 | 1024 /* CLSCTX_LOCAL_SERVER | CLSCTX_NO_CODE_DOWNLOAD */, ref guid_INowPlayingSessionManager_10586, out sessionManagerIUnknown) == 0)
            {
                numSelectInterface = 10586;
                sessionManager_10586 = (INowPlayingSessionManager_10586)sessionManagerIUnknown;
            }
            else
            {
                throw new NotSupportedException("CoCreateInstance failed to create an instance due to non-available guid");
            }                
        }

        /// <summary>
        /// Get the current active session
        /// </summary>
        public NowPlayingSession CurrentSession 
        { 
            get 
            {
                object sessionIUnknown = null;

                if (numSelectInterface == 19041)
                    sessionManager_19041.get_CurrentSession(out sessionIUnknown);
                else
                    sessionManager_10586.get_CurrentSession(out sessionIUnknown);

                return sessionIUnknown == null ? null : new NowPlayingSession(sessionIUnknown);
            } 
        }

        /// <summary>
        /// Get the number of sessions
        /// </summary>
        public ulong Count
        {
            get
            {
                ulong count;
                if (numSelectInterface == 19041)
                    sessionManager_19041.get_Count(out count);
                else
                    sessionManager_10586.get_Count(out count);

                return count;
            }
        }

        public NowPlayingSession[] GetSessions()
        {
            object[] sessionsIUnknown;
            uint count = 0;

            if (numSelectInterface == 19041)
                sessionManager_19041.GetSessions(out count, out sessionsIUnknown);
            else
                sessionManager_10586.GetSessions(out count, out sessionsIUnknown);

            NowPlayingSession[] array = new NowPlayingSession[count];
            for (uint i = 0; i < count; i++)
                array[i] = new NowPlayingSession(sessionsIUnknown[i]);
            return array;
        }

        /// <summary>
        /// Sets the next session as current.
        /// </summary>
        public void SetNextCurrentSession()
        {
            if (numSelectInterface == 19041)
                sessionManager_19041.SetCurrentNextSession();
            else
                sessionManager_10586.SetCurrentNextSession();
        }

        public void SetCurrentSession(NowPlayingSessionInfo pInfo)
        {
            if (numSelectInterface == 19041)
                sessionManager_19041.SetCurrentSession(pInfo.GetIUnknownInterface);
            else
                sessionManager_10586.SetCurrentSession(pInfo.GetIUnknownInterface);
        }

        public void RemoveSession(NowPlayingSessionInfo pInfo)
        {
            if (numSelectInterface == 19041)
                sessionManager_19041.RemoveSession(pInfo.GetIUnknownInterface);
            else
                sessionManager_10586.RemoveSession(pInfo.GetIUnknownInterface);
        }

        public NowPlayingSession FindSession(NowPlayingSessionInfo pInfo)
        {
            object sessionIUnknown;

            if (numSelectInterface == 19041)
                sessionManager_19041.FindSession(pInfo.GetIUnknownInterface, out sessionIUnknown);
            else
                sessionManager_10586.FindSession(pInfo.GetIUnknownInterface, out sessionIUnknown);

            return new NowPlayingSession(sessionIUnknown);
        }

        /// <summary>
        /// ?
        /// </summary>
        /// <param name="hWnd">Handle to the window present in any session</param>
        public void Refresh(IntPtr hWnd)
        {
            if (numSelectInterface == 19041)
                sessionManager_19041.Refresh(hWnd);
            else
                sessionManager_10586.Refresh(hWnd);
        }

        public void Update(bool fEnabled, IntPtr hwnd, uint dwPID, ulong unknown, MediaPlaybackDataSource pSource)
        {
            if (numSelectInterface == 19041)
                sessionManager_19041.Update(fEnabled, hwnd, dwPID, unknown, pSource.GetIUnknownInterface);
            else
                sessionManager_10586.Update(fEnabled, hwnd, dwPID, pSource.GetIUnknownInterface);
        }

        //TODO
        //manager.AddSession

        #region Event

        NowPlayingSessionManagerEventHandler eventHandler;

        readonly object subscriptionLock = new object();

        private event EventHandler<NowPlayingSessionManagerEventArgs> _sessionListChanged;
        public event EventHandler<NowPlayingSessionManagerEventArgs> SessionListChanged
        {
            add
            {
                lock (subscriptionLock)
                {
                    if (_sessionListChanged == null)
                    {
                        eventHandler = new NowPlayingSessionManagerEventHandler(this);
                        NPSMEventRegistrationToken token;
                        if (numSelectInterface == 19041)
                            sessionManager_19041.RegisterEventHandler(eventHandler, out token);
                        else
                            sessionManager_10586.RegisterEventHandler(eventHandler, out token);
                        eventHandler.Token = token;
                    }

                    _sessionListChanged += value;
                }
            }
            remove
            {
                lock (subscriptionLock)
                {
                    _sessionListChanged -= value;

                    if (_sessionListChanged == null)
                    {
                        if (numSelectInterface == 19041)
                            sessionManager_19041.UnregisterEventHandler(eventHandler.Token);
                        else
                            sessionManager_10586.UnregisterEventHandler(eventHandler.Token);
                        eventHandler = null;
                    }
                }
            }
        }

        class NowPlayingSessionManagerEventHandler : INowPlayingSessionManagerEventHandler
        {
            internal NPSMEventRegistrationToken Token { get; set; }
            private NowPlayingSessionManager CurrentSessionInstance { get; set; }
            public NowPlayingSessionManagerEventHandler(NowPlayingSessionManager currentSessionInstance)
            {
                CurrentSessionInstance = currentSessionInstance;
            }
            public void OnChange(NowPlayingSessionManagerNotificationType notificationType, object pINowPlayingSessionInfoIUnknown, [MarshalAs(UnmanagedType.LPWStr)] string unknown)
            {
                //forward to subscribers
                CurrentSessionInstance._sessionListChanged?.Invoke(CurrentSessionInstance, 
                    new NowPlayingSessionManagerEventArgs { NotificationType = notificationType, NowPlayingSessionInfo = new NowPlayingSessionInfo(pINowPlayingSessionInfoIUnknown), SessionTypeString = unknown });
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
