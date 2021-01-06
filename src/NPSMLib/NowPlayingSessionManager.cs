using NPSMLib.Interop;
using System;
using System.Runtime.InteropServices;
using static NPSMLib.Interop.COMInterop;

namespace NPSMLib
{
    public class NowPlayingSessionManager
    {
        private readonly ushort osbuild;
        private readonly object sessionManagerIUnknown;
        //cache to prevent calling QueryInterface each time it casts
        private readonly INowPlayingSessionManager_19041 sessionManager_19041;
        private readonly INowPlayingSessionManager_10586 sessionManager_10586;
        
        /// <summary>
        /// Handles Now Playing sessions.
        /// Can throw if the underlaying COM interface fails to create
        /// </summary>
        public NowPlayingSessionManager(ushort OSBuild)
        {
            osbuild = OSBuild;
            Guid guid_INowPlayingSessionManager = OSBuild >= 19041 ? new Guid("3b6a7908-ce07-4ba9-878c-6e4a15db5e5b") : new Guid("A7052211-8B56-43C4-8F26-12852F7303A3");
            NativeMethods.CoCreateInstance(ref CLSID_NowPlayingSessionManager, null, 4 /* CLSCTX_LOCAL_SERVER */, ref guid_INowPlayingSessionManager, out sessionManagerIUnknown);

            if (osbuild >= 19041)
                sessionManager_19041 = (INowPlayingSessionManager_19041)sessionManagerIUnknown;
            else
                sessionManager_10586 = (INowPlayingSessionManager_10586)sessionManagerIUnknown;
        }

        /// <summary>
        /// Get the current active session
        /// </summary>
        public NowPlayingSession CurrentSession 
        { 
            get 
            {
                object sessionIUnknown = null;

                if (osbuild >= 19041)
                    sessionManager_19041.get_CurrentSession(out sessionIUnknown);
                else
                    sessionManager_10586.get_CurrentSession(out sessionIUnknown);

                return sessionIUnknown == null ? null : new NowPlayingSession(sessionIUnknown, osbuild);
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
                if (osbuild >= 19041)
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

            if (osbuild >= 19041)
                sessionManager_19041.GetSessions(out count, out sessionsIUnknown);
            else
                sessionManager_10586.GetSessions(out count, out sessionsIUnknown);

            NowPlayingSession[] array = new NowPlayingSession[count];
            for (uint i = 0; i < count; i++)
                array[i] = new NowPlayingSession(sessionsIUnknown[i], osbuild);
            return array;
        }

        /// <summary>
        /// Sets the next session as current.
        /// </summary>
        public void SetNextCurrentSession()
        {
            if (osbuild >= 19041)
                sessionManager_19041.SetCurrentNextSession();
            else
                sessionManager_10586.SetCurrentNextSession();
        }

        public void SetCurrentSession(NowPlayingSessionInfo pInfo)
        {
            if (osbuild >= 19041)
                sessionManager_19041.SetCurrentSession(pInfo.GetIUnknownInterface);
            else
                sessionManager_10586.SetCurrentSession(pInfo.GetIUnknownInterface);
        }

        public void RemoveSession(NowPlayingSessionInfo pInfo)
        {
            if (osbuild >= 19041)
                sessionManager_19041.RemoveSession(pInfo.GetIUnknownInterface);
            else
                sessionManager_10586.RemoveSession(pInfo.GetIUnknownInterface);
        }

        public NowPlayingSession FindSession(NowPlayingSessionInfo pInfo)
        {
            object sessionIUnknown;

            if (osbuild >= 19041)
                sessionManager_19041.FindSession(pInfo.GetIUnknownInterface, out sessionIUnknown);
            else
                sessionManager_10586.FindSession(pInfo.GetIUnknownInterface, out sessionIUnknown);

            return new NowPlayingSession(sessionIUnknown, osbuild);
        }

        /// <summary>
        /// ?
        /// </summary>
        /// <param name="hWnd">Handle to the window present in any session</param>
        public void Refresh(IntPtr hWnd)
        {
            if (osbuild >= 19041)
                sessionManager_19041.Refresh(hWnd);
            else
                sessionManager_10586.Refresh(hWnd);
        }

        public void Update(bool fEnabled, IntPtr hwnd, uint dwPID, ulong unknown, MediaPlaybackDataSource pSource)
        {
            if (osbuild >= 19041)
                sessionManager_19041.Update(fEnabled, hwnd, dwPID, unknown, pSource.GetIUnknownInterface);
            else
                sessionManager_10586.Update(fEnabled, hwnd, dwPID, pSource.GetIUnknownInterface);
        }

        //TODO
        //manager.AddSession

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
                        eventHandler = new NowPlayingSessionManagerEventHandler(this, osbuild);
                        NPSMEventRegistrationToken token;
                        if (osbuild >= 19041)
                            sessionManager_19041.RegisterEventHandler(eventHandler, out token);
                        else
                            sessionManager_10586.RegisterEventHandler(eventHandler, out token);
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
                        if (osbuild >= 19041)
                            sessionManager_19041.UnregisterEventHandler(eventHandler.Token);
                        else
                            sessionManager_10586.UnregisterEventHandler(eventHandler.Token);
                        eventHandler = null;
                    }

                    bSessionListChanged -= value;
                }
            }
        }

        class NowPlayingSessionManagerEventHandler : INowPlayingSessionManagerEventHandler
        {
            internal NPSMEventRegistrationToken Token { get; set; }
            private NowPlayingSessionManager CurrentSessionInstance { get; set; }
            private ushort OSBuild { get; set; }
            public NowPlayingSessionManagerEventHandler(NowPlayingSessionManager currentSessionInstance, ushort osbuild)
            {
                CurrentSessionInstance = currentSessionInstance;
                OSBuild = osbuild;
            }
            public void OnChange(NowPlayingSessionManagerNotificationType notificationType, object pINowPlayingSessionInfoIUnknown, [MarshalAs(UnmanagedType.LPWStr)] string unknown)
            {
                //forward to subscribers
                CurrentSessionInstance.bSessionListChanged?.Invoke(CurrentSessionInstance, 
                    new NowPlayingSessionManagerEventArgs { NotificationType = notificationType, NowPlayingSessionInfo = new NowPlayingSessionInfo(pINowPlayingSessionInfoIUnknown, OSBuild), SessionTypeString = unknown });
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
