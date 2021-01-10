using System;
using System.Runtime.InteropServices;

namespace NPSMLib.Interop
{
    internal class COMInterop
    {
        //VALIDATED FOR: 20279-19041-18362-17763-17134-16299-15063-14393-10586

        internal static Guid CLSID_NowPlayingSessionManager = new Guid("BCBB9860-C012-4AD7-A938-6E337AE6ABA5");

        public struct NPSMEventRegistrationToken
        {
            private readonly long value;
        }

        #region INowPlayingSessionManager

        //19041+
        [ComImport, Guid("3b6a7908-ce07-4ba9-878c-6e4a15db5e5b"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface INowPlayingSessionManager_19041
        {
            [PreserveSig]
            int get_Count(out ulong pCount);

            [PreserveSig]
            int get_CurrentSession([MarshalAs(UnmanagedType.IUnknown)] out object ppSession /* INowPlayingSession */);

            [PreserveSig]
            int AddSession(NowPlayingSessionType type, IntPtr hwnd, uint dwPID, [MarshalAs(UnmanagedType.LPWStr)] string appId,
                [MarshalAs(UnmanagedType.LPWStr)] string szSourceDeviceId, [MarshalAs(UnmanagedType.LPWStr)] string szRenderDeviceId,
                [MarshalAs(UnmanagedType.LPWStr)] string source, [MarshalAs(UnmanagedType.IUnknown)] object pMediaControl /* IMediaPlaybackDataSource */,
                [MarshalAs(UnmanagedType.IUnknown)] object pConnection, [MarshalAs(UnmanagedType.Bool)] bool fMarkAsCurrentSession,
                IntPtr processHandle);

            [PreserveSig]
            int RemoveSession([MarshalAs(UnmanagedType.IUnknown)] object pInfo /* INowPlayingSessionInfo */);

            [PreserveSig]
            int GetSessions(out uint pdwCount, out IntPtr pppSessions /* INowPlayingSession[] */);

            [PreserveSig]
            int FindSession([MarshalAs(UnmanagedType.IUnknown)] object pInfo /* INowPlayingSessionInfo */, [MarshalAs(UnmanagedType.IUnknown)] out object ppSession /* INowPlayingSession */);

            [PreserveSig]
            int SetCurrentSession([MarshalAs(UnmanagedType.IUnknown)] object pInfo /* INowPlayingSessionInfo */);

            [PreserveSig]
            int SetCurrentNextSession();

            [PreserveSig]
            int Refresh(IntPtr hwnd);

            [PreserveSig]
            int Update([MarshalAs(UnmanagedType.Bool)] bool fEnabled, IntPtr hwnd, uint dwPID, ulong unknown, [MarshalAs(UnmanagedType.IUnknown)] object pSource /* IMediaPlaybackDataSource */); //DIFF

            [PreserveSig]
            int RegisterEventHandler(INowPlayingSessionManagerEventHandler pEventHandler, out NPSMEventRegistrationToken pToken);

            [PreserveSig]
            int UnregisterEventHandler(NPSMEventRegistrationToken token);
        }

        //18362-17763-17134-16299-15063-14393-10586
        [ComImport, Guid("A7052211-8B56-43C4-8F26-12852F7303A3"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface INowPlayingSessionManager_10586
        {
            [PreserveSig]
            int get_Count(out ulong pCount);

            [PreserveSig]
            int get_CurrentSession([MarshalAs(UnmanagedType.IUnknown)] out object ppSession /* INowPlayingSession */);

            [PreserveSig]
            int AddSession(NowPlayingSessionType type, IntPtr hwnd, uint dwPID, [MarshalAs(UnmanagedType.LPWStr)] string appId,
                [MarshalAs(UnmanagedType.LPWStr)] string szSourceDeviceId, [MarshalAs(UnmanagedType.LPWStr)] string szRenderDeviceId,
                [MarshalAs(UnmanagedType.LPWStr)] string source, [MarshalAs(UnmanagedType.IUnknown)] object pMediaControl /* IMediaPlaybackDataSource */,
                [MarshalAs(UnmanagedType.IUnknown)] object pConnection, [MarshalAs(UnmanagedType.Bool)] bool fMarkAsCurrentSession,
                IntPtr processHandle);

            [PreserveSig]
            int RemoveSession([MarshalAs(UnmanagedType.IUnknown)] object pInfo /* INowPlayingSessionInfo */);

            [PreserveSig]
            int GetSessions(out uint pdwCount, out IntPtr pppSessions /* INowPlayingSession[] */);

            [PreserveSig]
            int FindSession([MarshalAs(UnmanagedType.IUnknown)] object pInfo /* INowPlayingSessionInfo */, [MarshalAs(UnmanagedType.IUnknown)] out object ppSession /* INowPlayingSession */);

            [PreserveSig]
            int SetCurrentSession([MarshalAs(UnmanagedType.IUnknown)] object pInfo /* INowPlayingSessionInfo */);

            [PreserveSig]
            int SetCurrentNextSession();

            [PreserveSig]
            int Refresh(IntPtr hwnd);

            [PreserveSig]
            int Update([MarshalAs(UnmanagedType.Bool)] bool fEnabled, IntPtr hwnd, uint dwPID, [MarshalAs(UnmanagedType.IUnknown)] object pSource /* IMediaPlaybackDataSource */); //DIFF

            [PreserveSig]
            int RegisterEventHandler(INowPlayingSessionManagerEventHandler pEventHandler, out NPSMEventRegistrationToken pToken);
            
            [PreserveSig]
            int UnregisterEventHandler(NPSMEventRegistrationToken token);
        }

        #endregion

        #region INowPlayingSession

        //20279-19041-18362-17763-17134-16299-15063-14393
        [ComImport, Guid("431268cf-7477-4285-950b-6f892a944712"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface INowPlayingSession_14393
        {
            [PreserveSig]
            int get_SessionType(out NowPlayingSessionType pType);

            [PreserveSig]
            int get_SourceAppId([MarshalAs(UnmanagedType.LPWStr)] out string pszSrcAppId);

            [PreserveSig]
            int get_SourceDeviceId([MarshalAs(UnmanagedType.LPWStr)] out string pszSourceDeviceId);

            [PreserveSig]
            int get_RenderDeviceId([MarshalAs(UnmanagedType.LPWStr)] out string pszRenderId);

            [PreserveSig]
            int get_HWND(out IntPtr pHwnd);

            [PreserveSig]
            int get_PID(out uint pdwPID);

            [PreserveSig]
            int get_Info([MarshalAs(UnmanagedType.IUnknown)] out object ppInfo /* INowPlayingSessionInfo */);

            [PreserveSig]
            int get_Connection([MarshalAs(UnmanagedType.IUnknown)] out object ppUnknown);

            [PreserveSig]
            int ActivateMediaPlaybackDataSource([MarshalAs(UnmanagedType.IUnknown)] out object ppMediaControl /* IMediaPlaybackDataSource */);

            [PreserveSig]
            int BeginInteractionWithSession([MarshalAs(UnmanagedType.IUnknown)] out object ppSessionInteractionToken);
        }

        //10586
        [ComImport, Guid("B363BB85-9230-436B-92DC-516E7AD29A85"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface INowPlayingSession_10586
        {
            [PreserveSig]
            int get_SessionType(out NowPlayingSessionType pType);

            [PreserveSig]
            int get_SourceAppId([MarshalAs(UnmanagedType.LPWStr)] out string pszSrcAppId);

            [PreserveSig]
            int get_SourceDeviceId([MarshalAs(UnmanagedType.LPWStr)] out string pszSourceDeviceId);

            [PreserveSig]
            int get_RenderDeviceId([MarshalAs(UnmanagedType.LPWStr)] out string pszRenderId);

            [PreserveSig]
            int get_HWND(out IntPtr pHwnd);

            [PreserveSig]
            int get_PID(out uint pdwPID);

            [PreserveSig]
            int get_Info([MarshalAs(UnmanagedType.IUnknown)] out object ppInfo /* INowPlayingSessionInfo */);

            [PreserveSig]
            int get_Connection([MarshalAs(UnmanagedType.IUnknown)] out object ppUnknown);

            [PreserveSig]
            int ActivateMediaPlaybackDataSource([MarshalAs(UnmanagedType.IUnknown)] out object ppMediaControl /* IMediaPlaybackDataSource */);
        }

        #endregion

        #region INowPlayingSessionInfo

        //This interface IS required since the guid is the same as 19041 one but has 1 function less in 19582+
        [ComImport, Guid("443dcf9d-ce15-408c-866e-e6d0c434548a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface INowPlayingSessionInfo_19582
        {
            [PreserveSig]
            int GetInfo(out IntPtr pHwnd, out uint pdwPID, [MarshalAs(UnmanagedType.LPWStr)] out string pszDeviceId);
            
            [PreserveSig]
            int IsEqual([MarshalAs(UnmanagedType.IUnknown)] object pInfo /* INowPlayingSessionInfo */, [MarshalAs(UnmanagedType.Bool)] out bool pfIsEqual);
        }

        //19041+
        [ComImport, Guid("443dcf9d-ce15-408c-866e-e6d0c434548a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface INowPlayingSessionInfo_19041
        {
            [PreserveSig]
            int GetInfo(out IntPtr pHwnd, out uint pdwPID, [MarshalAs(UnmanagedType.LPWStr)] out string pszDeviceId);

            [PreserveSig]
            int IsRunningInContainerOS([MarshalAs(UnmanagedType.Bool)] out bool pfIsInContainer);

            [PreserveSig]
            int IsEqual([MarshalAs(UnmanagedType.IUnknown)] object pInfo /* INowPlayingSessionInfo */, [MarshalAs(UnmanagedType.Bool)] out bool pfIsEqual);
        }

        //18362-17763-17134-16299-15063-14393-10586
        [ComImport, Guid("59EE9066-BDE7-4EE3-8224-731FF9823E49"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface INowPlayingSessionInfo_10586
        {
            [PreserveSig]
            int GetInfo(out IntPtr pHwnd, out uint pdwPID, [MarshalAs(UnmanagedType.LPWStr)] out string pszDeviceId);

            [PreserveSig]
            int IsEqual([MarshalAs(UnmanagedType.IUnknown)] object pInfo /* INowPlayingSessionInfo */, [MarshalAs(UnmanagedType.Bool)] out bool pfIsEqual);
        }

        #endregion

        #region IMediaPlaybackDataSource

        //20279+
        [ComImport, Guid("c4f66b80-df04-4f79-afc2-bee3fc7c46e3"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IMediaPlaybackDataSource_20279
        {
            [PreserveSig]
            int GetMediaPlaybackInfo(out MediaPlaybackInfo pPlaybackInfo);

            [PreserveSig]
            int SendMediaPlaybackCommand(MediaPlaybackCommands command);

            [PreserveSig]
            int GetMediaObjectInfo(out IPropertyStore ppMediaObjectInfo);

            [PreserveSig]
            int GetMediaTimelineProperties(out MediaTimelineProperties pTimelineProperties);

            [PreserveSig]
            int RegisterEventHandler(IMediaPlaybackDataChangedEventHandler handler, out NPSMEventRegistrationToken token);

            [PreserveSig]
            int UnregisterEventHandler(NPSMEventRegistrationToken token);

            [PreserveSig]
            int SendRepeatModeChangeRequest(MediaPlaybackRepeatMode requestedRepeatMode);

            [PreserveSig]
            int SendPlaybackRateChangeRequest(double requestedPlaybackRate);

            [PreserveSig]
            int SendShuffleEnabledChangeRequest([MarshalAs(UnmanagedType.Bool)] bool requestedShuffle);

            [PreserveSig]
            int SendPlaybackPositionChangeRequest(long requestedPlaybackPosition);

            [PreserveSig]
            int GetParentApplicationId([MarshalAs(UnmanagedType.HString)] out string value);

            [PreserveSig]
            int GetMediaObjectInfoAsSet(Guid guid, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnknown); //not implemented by MS, returns 0x80004001

            [PreserveSig]
            int GetIsRunningInGuestContainer([MarshalAs(UnmanagedType.Bool)] bool runningInContainer);
        }

        //19041-18362-17763-17134-16299-15063-14393-10586
        [ComImport, Guid("0F4521BE-A0B8-4116-B3B1-BFECEBAEEBE6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IMediaPlaybackDataSource_10586
        {
            [PreserveSig]
            int GetMediaPlaybackInfo(out MediaPlaybackInfo pPlaybackInfo);

            [PreserveSig]
            int SendMediaPlaybackCommand(MediaPlaybackCommands command);

            [PreserveSig]
            int GetMediaObjectInfo(out IPropertyStore ppMediaObjectInfo);

            [PreserveSig]
            int GetMediaTimelineProperties(out MediaTimelineProperties pTimelineProperties);

            [PreserveSig]
            int RegisterEventHandler(IMediaPlaybackDataChangedEventHandler handler, out NPSMEventRegistrationToken token);

            [PreserveSig]
            int UnregisterEventHandler(NPSMEventRegistrationToken token);

            [PreserveSig]
            int SendRepeatModeChangeRequest(MediaPlaybackRepeatMode requestedRepeatMode);

            [PreserveSig]
            int SendPlaybackRateChangeRequest(double requestedPlaybackRate);

            [PreserveSig]
            int SendShuffleEnabledChangeRequest([MarshalAs(UnmanagedType.Bool)] bool requestedShuffle);

            [PreserveSig]
            int SendPlaybackPositionChangeRequest(long requestedPlaybackPosition);

            [PreserveSig]
            int GetParentApplicationId([MarshalAs(UnmanagedType.HString)] out string value);
        }

        #endregion

        #region Event handlers

        //20279-19041-18362-17763-17134-16299-15063-14393-10586
        [ComImport, Guid("d22f448d-39dc-415f-8325-91eddf0d5264"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IMediaPlaybackDataChangedEventHandler
        {
            [PreserveSig]
            void OnMediaPlaybackDataChangedEvent([MarshalAs(UnmanagedType.IUnknown)] object source /* IMediaPlaybackDataSource */, MediaPlaybackDataChangedEvent dataChangedEvent);
        }

        //20279-19041-18362-17763-17134-16299-15063-14393-10586
        [ComImport, Guid("d25f31a2-8cae-4058-967e-8a4497e1cdbc"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface INowPlayingSessionManagerEventHandler
        {
            [PreserveSig]
            void OnChange(NowPlayingSessionManagerNotificationType notificationType, [MarshalAs(UnmanagedType.IUnknown)] object pInfo /*INowPlayingSessionInfo pInfo*/, [MarshalAs(UnmanagedType.LPWStr)] string pszSource);
        }

        #endregion

        #region Property store interface

        //Property store never changes.
        [ComImport, Guid("886d8eeb-8cf2-4446-8d02-cdba1dbdcf99"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IPropertyStore
        {
            int GetCount(out uint cProps);

            int GetAt(uint iProp, out PROPERTYKEY pkey);

            int GetValue(ref PROPERTYKEY key, out PROPVARIANT pv);

            int SetValue(ref PROPERTYKEY key, ref PROPVARIANT propvar);

            int Commit();
        }

        #endregion
    }
}
