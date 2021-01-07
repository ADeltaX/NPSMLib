using System;

namespace NPSMLib
{
    //VALIDATED FOR: 20279-19041-18362-17763-17134-16299-15063-14393-10586

    /// <summary>
    /// Specifies the now playing session type.
    /// </summary>
    public enum NowPlayingSessionType
    {
        /// <summary>
        /// The session type is unknown.
        /// </summary>
        Unknown = 0x0,

        /// <summary>
        /// The session is played remotely.
        /// </summary>
        PlayTo = 0x1,

        /// <summary>
        /// The session is local.
        /// </summary>
        Local = 0x2
    }

    /// <summary>
    /// Specifies the media playback commands that could be sent to a media session
    /// through the <see cref="MediaPlaybackDataSource.SendMediaPlaybackCommand(MediaPlaybackCommands)"/> method.
    /// </summary>
    public enum MediaPlaybackCommands
    {
        /// <summary>
        /// The play command.
        /// </summary>
        Play = 0x0,

        /// <summary>
        /// The pause command.
        /// </summary>
        Pause = 0x1,

        /// <summary>
        /// The stop command.
        /// </summary>
        Stop = 0x2,

        /// <summary>
        /// The record command.
        /// </summary>
        Record = 0x3,

        /// <summary>
        /// The fast forward command.
        /// </summary>
        FastForward = 0x4,

        /// <summary>
        /// The rewind command.
        /// </summary>
        Rewind = 0x5,

        /// <summary>
        /// The next command.
        /// </summary>
        Next = 0x6,

        /// <summary>
        /// The previous command.
        /// </summary>
        Previous = 0x7,

        /// <summary>
        /// The channel up command.
        /// </summary>
        ChannelUp = 0x8,

        /// <summary>
        /// The channel down command.
        /// </summary>
        ChannelDown = 0x9,

        /// <summary>
        /// The play/pause toggle command.
        /// </summary>
        PlayPauseToggle = 0xA,

        /// <summary>
        /// The max command.
        /// </summary>
        Max = 0xB
    }

    /// <summary>
    /// Specifies the repeat mode for media playback.
    /// </summary>
    public enum MediaPlaybackRepeatMode
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown = 0x0,

        /// <summary>
        /// No repeating.
        /// </summary>
        None = 0x1,

        /// <summary>
        /// Repeat the current track.
        /// </summary>
        Track = 0x2,

        /// <summary>
        /// Repeat the current list of tracks.
        /// </summary>
        List = 0x3
    }

    /// <summary>
    /// Specifies the type of media playback data changed event.
    /// </summary>
    public enum MediaPlaybackDataChangedEvent
    {
        /// <summary>
        /// The playback information of the media playback changed.
        /// </summary>
        PlaybackInfoChanged = 0x0,

        /// <summary>
        /// The media information of the media playback changed.
        /// </summary>
        MediaInfoChanged = 0x1,

        /// <summary>
        /// The timeline properties of the media playback changed.
        /// </summary>
        TimelinePropertiesChanged = 0x2
    }

    /// <summary>
    /// Specifies the type of sessions changed notification.
    /// </summary>
    public enum NowPlayingSessionManagerNotificationType
    {
        /// <summary>
        /// A new session was created.
        /// </summary>
        SessionCreated = 0x0,

        /// <summary>
        /// The current session changed.
        /// </summary>
        CurrentSessionChanged = 0x1,

        /// <summary>
        /// A session was disconnected.
        /// </summary>
        SessionDisconnected = 0x2
    }

    /// <summary>
    /// Specifies which properties of <see cref="MediaPlaybackInfo"/> are valid.
    /// </summary>
    [Flags]
    public enum MediaPlaybackProps
    {
        /// <summary>
        /// The <see cref="MediaPlaybackInfo.PlaybackCaps"/> property is valid.
        /// </summary>
        Capabilities = 0x1,

        /// <summary>
        /// The <see cref="MediaPlaybackInfo.PlaybackState"/> property is valid.
        /// </summary>
        State = 0x2,

        /// <summary>
        /// The <see cref="MediaPlaybackInfo.PlaybackMode"/> property is valid.
        /// </summary>
        Mode = 0x4,

        /// <summary>
        /// The <see cref="MediaPlaybackInfo.RepeatMode"/> property is valid.
        /// </summary>
        AutoRepeatMode = 0x8,

        /// <summary>
        /// The <see cref="MediaPlaybackInfo.PlaybackRate"/> property is valid.
        /// </summary>
        PlaybackRate = 0x10,

        /// <summary>
        /// The <see cref="MediaPlaybackInfo.ShuffleEnabled"/> property is valid.
        /// </summary>
        ShuffleEnabled = 0x20
    }

    /// <summary>
    /// Specifies the capabilities of a media playback.
    /// </summary>
    [Flags]
    public enum MediaPlaybackCapabilities
    {
        /// <summary>
        /// The media playback currently has no capabilities.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// The media playback currently supports the <see cref="MediaPlaybackCommands.Play"/> command.
        /// </summary>
        Play = 0x1,

        /// <summary>
        /// The media playback currently supports the <see cref="MediaPlaybackCommands.Pause"/> command.
        /// </summary>
        Pause = 0x2,

        /// <summary>
        /// The media playback currently supports the <see cref="MediaPlaybackCommands.Stop"/> command.
        /// </summary>
        Stop = 0x4,

        /// <summary>
        /// The media playback currently supports the <see cref="MediaPlaybackCommands.Record"/> command.
        /// </summary>
        Record = 0x8,

        /// <summary>
        /// The media playback currently supports the <see cref="MediaPlaybackCommands.FastForward"/> command.
        /// </summary>
        FastForward = 0x10,

        /// <summary>
        /// The media playback currently supports the <see cref="MediaPlaybackCommands.Rewind"/> command.
        /// </summary>
        Rewind = 0x20,

        /// <summary>
        /// The media playback currently supports the <see cref="MediaPlaybackCommands.Next"/> command.
        /// </summary>
        Next = 0x40,

        /// <summary>
        /// The media playback currently supports the <see cref="MediaPlaybackCommands.Previous"/> command.
        /// </summary>
        Previous = 0x80,

        /// <summary>
        /// The media playback currently supports the <see cref="MediaPlaybackCommands.ChannelUp"/> command.
        /// </summary>
        ChannelUp = 0x100,

        /// <summary>
        /// The media playback currently supports the <see cref="MediaPlaybackCommands.ChannelDown"/> command.
        /// </summary>
        ChannelDown = 0x200,

        /// <summary>
        /// The media playback currently supports the <see cref="MediaPlaybackCommands.PlayPauseToggle"/> command.
        /// </summary>
        PlayPauseToggle = 0x400,

        /// <summary>
        /// The media playback currently supports handling end-point changes.
        /// </summary>
        HandleEndpointChange = 0x800,

        /// <summary>
        /// The media playback currently supports changing its shuffle state.
        /// </summary>
        Shuffle = 0x1000,

        /// <summary>
        /// The media playback currently supports changing its repeat mode.
        /// </summary>
        Repeat = 0x2000,

        /// <summary>
        /// The media playback currently supports changing its playback rate.
        /// </summary>
        PlaybackRate = 0x4000,

        /// <summary>
        /// The media playback currently supports changing its playback position.
        /// </summary>
        PlaybackPosition = 0x8000
    }

    /// <summary>
    /// Specifies the state of a media playback.
    /// </summary>
    public enum MediaPlaybackState
    {
        /// <summary>
        /// The media playback state is unknown.
        /// </summary>
        Unknown = 0x0,

        /// <summary>
        /// The media playback closed.
        /// </summary>
        Closed = 0x1,

        /// <summary>
        /// The media playback opened.
        /// </summary>
        Opened = 0x2,

        /// <summary>
        /// The media playback is changing.
        /// </summary>
        Changing = 0x3,

        /// <summary>
        /// The media playback is stopped.
        /// </summary>
        Stopped = 0x4,

        /// <summary>
        /// The media playback is playing.
        /// </summary>
        Playing = 0x5,

        /// <summary>
        /// The media playback is paused.
        /// </summary>
        Paused = 0x6
    }

    /// <summary>
    /// Specifies the mode or type of a media playback.
    /// </summary>
    public enum MediaPlaybackMode
    {
        /// <summary>
        /// The media type is unknown.
        /// </summary>
        Unknown = 0x0,

        /// <summary>
        /// The media type is audio.
        /// </summary>
        Audio = 0x1,

        /// <summary>
        /// The media type is video.
        /// </summary>
        Video = 0x2,

        /// <summary>
        /// The media type is image.
        /// </summary>
        Image = 0x3,

        /// <summary>
        /// The media type is podcast.
        /// </summary>
        Podcast = 0x4,

        /// <summary>
        /// The media type is audio book.
        /// </summary>
        AudioBook = 0x5
    }
}
