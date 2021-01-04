namespace NPSMLib
{
	public enum NowPlayingSessionType
	{
		Unknown = 0x0,
		PlayTo = 0x1,
		Local = 0x2
	}

	public enum MediaPlaybackCommands
	{
		Play = 0x0,
		Pause = 0x1,
		Stop = 0x2,
		Record = 0x3,
		FastForward = 0x4,
		Rewind = 0x5,
		Next = 0x6,
		Previous = 0x7,
		ChannelUp = 0x8,
		ChannelDown = 0x9,
		PlayPauseToggle = 0xA,
		Max = 0xB
	}

	public enum MediaPlaybackRepeatMode
	{
		Unknown = 0x0,
		None = 0x1,
		Track = 0x2,
		List = 0x3
	}

	public enum MediaPlaybackDataChangedEvent
	{
		PlaybackInfoChanged = 0x0,
		MediaInfoChanged = 0x1,
		TimelinePropertiesChanged = 0x2
	}

	public enum NowPlayingSessionManagerNotificationType
	{
		SessionCreated = 0x0,
		CurrentSessionChanged = 0x1,
		SessionDisconnected = 0x2
	}

	public enum MediaPlaybackProps
	{
		Capabilities = 0x1,
		State = 0x2,
		Mode = 0x4,
		AutoRepeatMode = 0x8,
		PlaybackRate = 0x10,
		ShuffleEnabled = 0x20
	}

	public enum MediaPlaybackCapabilities
	{
		None = 0x0,
		Play = 0x1,
		Pause = 0x2,
		Stop = 0x4,
		Record = 0x8,
		FastForward = 0x10,
		Rewind = 0x20,
		Next = 0x40,
		Previous = 0x80,
		ChannelUp = 0x100,
		ChannelDown = 0x200,
		PlayPauseToggle = 0x400,
		HandleEndpointChange = 0x800,
		Shuffle = 0x1000,
		Repeat = 0x2000,
		PlaybackRate = 0x4000,
		PlaybackPosition = 0x8000
	}

	public enum MediaPlaybackState
	{
		Unknown = 0x0,
		Closed = 0x1,
		Opened = 0x2,
		Changing = 0x3,
		Stopped = 0x4,
		Playing = 0x5,
		Paused = 0x6
	}

	public enum MediaPlaybackMode
	{
		Unknown = 0x0,
		Audio = 0x1,
		Video = 0x2,
		Image = 0x3,
		Podcast = 0x4,
		AudioBook = 0x5
	}

	//Not related to COM enums
	public enum MediaPlaybackType
    {
		Unknown,
		Music,
		Video,
		Image
    }
}
