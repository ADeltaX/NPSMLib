using System;
using System.Runtime.InteropServices;

namespace NPSMLib.Interop
{
    internal class COMInterop
	{
		//TODO: set PreserveSig to prevent automatic convertion from HRESULT to COMException

		//VALIDATED FOR: 20279

		public struct lEventRegistrationToken
        {
			readonly long Value;
        }

		//20279-19041
		[ComImport, Guid("3b6a7908-ce07-4ba9-878c-6e4a15db5e5b"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface INowPlayingSessionManager
		{
			int get_Count(out ulong pCount);
			[PreserveSig]
			int get_CurrentSession(out INowPlayingSession ppSession);
			int AddSession(NowPlayingSessionType type, IntPtr hwnd, uint dwPID, [MarshalAs(UnmanagedType.LPWStr)] string appId,
				[MarshalAs(UnmanagedType.LPWStr)] string szSourceDeviceId, [MarshalAs(UnmanagedType.LPWStr)] string szRenderDeviceId,
				[MarshalAs(UnmanagedType.LPWStr)] string source, IMediaPlaybackDataSource pMediaControl, 
				[MarshalAs(UnmanagedType.IUnknown)] object pConnection, [MarshalAs(UnmanagedType.Bool)]  bool fMarkAsCurrentSession, 
				IntPtr processHandle);

			int RemoveSession(INowPlayingSessionInfo pInfo);

			int GetSessions(out uint pdwCount, out INowPlayingSession[] pppSessions);
			int FindSession(INowPlayingSessionInfo pInfo, out INowPlayingSession ppSession);
			int SetCurrentSession(INowPlayingSessionInfo pInfo);
			int SetCurrentNextSession();
			int Refresh(IntPtr hwnd);
			int Update([MarshalAs(UnmanagedType.Bool)] bool fEnabled, IntPtr hwnd, uint dwPID, ulong unknown, IMediaPlaybackDataSource pSource);
			int RegisterEventHandler(INowPlayingSessionManagerEventHandler pEventHandler, out lEventRegistrationToken pToken);
			int UnregisterEventHandler(lEventRegistrationToken token);
		}

		//20279-19041
		[ComImport, Guid("431268cf-7477-4285-950b-6f892a944712"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface INowPlayingSession
		{
			int get_SessionType(out NowPlayingSessionType pType);
			int get_SourceAppId([MarshalAs(UnmanagedType.LPWStr)] out string pszSrcAppId);
			int get_SourceDeviceId([MarshalAs(UnmanagedType.LPWStr)] out string pszSourceDeviceId);
			int get_RenderDeviceId([MarshalAs(UnmanagedType.LPWStr)] out string pszRenderId);
			int get_HWND(out IntPtr pHwnd);
			int get_PID(out uint pdwPID);
			int get_Info(out INowPlayingSessionInfo ppInfo);
			int get_Connection(out object ppUnknown);
			int ActivateMediaPlaybackDataSource(out IMediaPlaybackDataSource ppMediaControl);
			int BeginInteractionWithSession(out object ppSessionInteractionToken);
		}

		//20279-19041
		[ComImport, Guid("443dcf9d-ce15-408c-866e-e6d0c434548a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface INowPlayingSessionInfo
		{
			int GetInfo(out IntPtr pHwnd, out uint pdwPID, [MarshalAs(UnmanagedType.LPWStr)] out string pszDeviceId);
			int IsRunningInContainerOS([MarshalAs(UnmanagedType.Bool)] out bool pfIsInContainer);
			int IsEqual(INowPlayingSessionInfo pInfo, [MarshalAs(UnmanagedType.Bool)] out bool pfIsEqual);
		}

		//20279
		//19041 0F4521BE-A0B8-4116-B3B1-BFECEBAEEBE6
		[ComImport, Guid("c4f66b80-df04-4f79-afc2-bee3fc7c46e3"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IMediaPlaybackDataSource
		{
			int GetMediaPlaybackInfo(out MediaPlaybackInfo pPlaybackInfo);
			int SendMediaPlaybackCommand(MediaPlaybackCommands command);
			int GetMediaObjectInfo(out IPropertyStore ppMediaObjectInfo);
			int GetMediaTimelineProperties(out MediaTimelineProperties pTimelineProperties);
			int RegisterEventHandler(IMediaPlaybackDataChangedEventHandler handler, out lEventRegistrationToken token);
			int UnregisterEventHandler(lEventRegistrationToken token);
			int SendRepeatModeChangeRequest(MediaPlaybackRepeatMode requestedRepeatMode);
			int SendPlaybackRateChangeRequest(double requestedPlaybackRate);
			int SendShuffleEnabledChangeRequest([MarshalAs(UnmanagedType.Bool)] bool requestedShuffle);
			int SendPlaybackPositionChangeRequest(long requestedPlaybackPosition);
			int GetParentApplicationId([MarshalAs(UnmanagedType.HString)] out string value);

			//20279
			int GetMediaObjectInfoAsSet(Guid guid, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnknown); //not implemented by MS, returns 0x80004001
			int GetIsRunningInGuestContainer([MarshalAs(UnmanagedType.Bool)] bool runningInContainer);
		}

		//20279-19041
		[ComImport, Guid("d22f448d-39dc-415f-8325-91eddf0d5264"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IMediaPlaybackDataChangedEventHandler
		{
			void OnMediaPlaybackDataChangedEvent(IMediaPlaybackDataSource source, MediaPlaybackDataChangedEvent dataChangedEvent);
		}

		//20279-19041
		[ComImport, Guid("d25f31a2-8cae-4058-967e-8a4497e1cdbc"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface INowPlayingSessionManagerEventHandler
		{
			void OnChange(NowPlayingSessionManagerNotificationType notificationType, INowPlayingSessionInfo pInfo, [MarshalAs(UnmanagedType.LPWStr)] string pszSource);
		}

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
	}
}
