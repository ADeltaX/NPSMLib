using NPSMLib.Interop;
using System;
using System.Runtime.InteropServices;
using static NPSMLib.Interop.COMInterop;

namespace NPSMLib
{
    /// <summary>
    /// Provides access to playback sessions throughout the system that have integrated
    /// with SystemMediaTransportControls to provide playback info and allow remote control.
    /// </summary>
    public class NowPlayingSessionManager
    {
        private readonly object sessionManagerIUnknown;

        //cache to prevent calling QueryInterface each time it casts
        private readonly INowPlayingSessionManager_19041 sessionManager_19041;
        private readonly INowPlayingSessionManager_10586 sessionManager_10586;
        private readonly int numSelectInterface = 0;

        //global cache of OS version
        private static readonly OSVersion osVersion = Helpers.GetOSVersion();
        internal static OSVersion OSVersion { get => osVersion; }

        /// <summary>
        /// Creates an instance of the <see cref="NowPlayingSessionManager"/>.
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
        /// Gets the current session. This is the session that the system believes the user would most likely want to control.
        /// </summary>
        /// <returns>The <see cref="NowPlayingSession"/> that represents this remote session.</returns>
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
        /// Get the total number of sessions.
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

        /// <summary>
        /// Gets all of the available sessions.
        /// </summary>
        /// <returns>An array of all available <see cref="NowPlayingSession"/>.</returns>
        public NowPlayingSession[] GetSessions()
        {
            uint count;
            IntPtr sessionsIUnknownPtr;
            if (numSelectInterface == 19041)
                sessionManager_19041.GetSessions(out count, out sessionsIUnknownPtr);
            else
                sessionManager_10586.GetSessions(out count, out sessionsIUnknownPtr);

            NowPlayingSession[] array = new NowPlayingSession[count];
            for (int i = 0; i < count; i++)
                array[i] = new NowPlayingSession(Marshal.GetObjectForIUnknown(Marshal.ReadIntPtr(sessionsIUnknownPtr + (IntPtr.Size * i))));
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

        /// <summary>
        /// Sets the current session. This is the session that the system believes the user would most likely want to control.
        /// </summary>
        /// <param name="pInfo">The <see cref="NowPlayingSessionInfo"/> associated with the corresponding session to set as current.</param>
        public void SetCurrentSession(NowPlayingSessionInfo pInfo)
        {
            if (numSelectInterface == 19041)
                sessionManager_19041.SetCurrentSession(pInfo.GetIUnknownInterface);
            else
                sessionManager_10586.SetCurrentSession(pInfo.GetIUnknownInterface);
        }

        /// <summary>
        /// Removes the requested session from the sessions list.
        /// </summary>
        /// <param name="pInfo">The <see cref="NowPlayingSessionInfo"/> associated with the corresponding session to remove.</param>
        public void RemoveSession(NowPlayingSessionInfo pInfo)
        {
            if (numSelectInterface == 19041)
                sessionManager_19041.RemoveSession(pInfo.GetIUnknownInterface);
            else
                sessionManager_10586.RemoveSession(pInfo.GetIUnknownInterface);
        }

        /// <summary>
        /// Finds and returns the <see cref="NowPlayingSession"/> associated with a <see cref="NowPlayingSessionInfo"/>.
        /// </summary>
        /// <param name="pInfo">The <see cref="NowPlayingSessionInfo"/> to find the <see cref="NowPlayingSession"/> for.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates a session.
        /// </summary>
        /// <param name="fEnabled">The requested enabled state to apply.</param>
        /// <param name="hwnd">The window handle associated with the session's source application.</param>
        /// <param name="dwPID">The process ID of the session's source application.</param>
        /// <param name="unknown">Unknown.</param>
        /// <param name="pSource">The <see cref="MediaPlaybackDataSource"/> associated with the session.</param>
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

        private NowPlayingSessionManagerEventHandler eventHandler;

        private readonly object subscriptionLock = new object();

        private event EventHandler<NowPlayingSessionManagerEventArgs> _sessionListChanged;

        /// <summary>
        /// Occurs when the sessions list have changed.
        /// </summary>
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

        private class NowPlayingSessionManagerEventHandler : INowPlayingSessionManagerEventHandler
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

    /// <summary>
    /// Represents arguments for a <see cref="NowPlayingSessionManager.SessionListChanged"/> event.
    /// </summary>
    public class NowPlayingSessionManagerEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the change notification type of the event.
        /// </summary>
        /// <returns>
        /// A <see cref="NowPlayingSessionManagerNotificationType"/> that represents the
        /// the change notification type of the event.
        /// </returns>
        public NowPlayingSessionManagerNotificationType NotificationType { get; internal set; }

        /// <summary>
        /// Gets the changed session's information.
        /// </summary>
        /// <return>A <see cref="NowPlayingSessionInfo"/> that represents the changed session which gave raise to the event.</return>
        public NowPlayingSessionInfo NowPlayingSessionInfo { get; internal set; }

        /// <summary>
        /// Gets the type of the session.
        /// </summary>
        public string SessionTypeString { get; internal set; }
    }
}
