using System;
using System.Runtime.InteropServices;

namespace NPSMLib
{
    //VALIDATED FOR: 20279-19041-18362-17763-17134-16299-15063-14393-10586

    /// <summary>
    /// The structure that holds all the playback information about a media session.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MediaPlaybackInfo
    {
        private readonly MediaPlaybackProps propsValid;
        private readonly MediaPlaybackCapabilities playbackCaps;
        private readonly MediaPlaybackState playbackState;
        private readonly MediaPlaybackMode playbackMode;
        private readonly MediaPlaybackRepeatMode repeatMode;
        private readonly double playbackRate;
        private readonly int shuffleEnabled;

        //20279
        private readonly long lastPlayingFileTime;

        //For future changes etc.. (prevents crash btw)
        private readonly long padding1;

        private readonly long padding2;
        private readonly long padding3;
        private readonly long padding4;

        /// <summary>
        /// Gets a value indicating which properties of <see cref="MediaPlaybackInfo"/> are valid.
        /// </summary>
        /// <returns>
        /// A bitwise combination of the <see cref="MediaPlaybackProps"/> values
        /// indicating which properties of <see cref="MediaPlaybackInfo"/> are valid.
        /// </returns>
        /// <remarks>
        /// Use this property to assure which properties of <see cref="MediaPlaybackInfo"/> are valid
        /// before preforming any operation on the corresponding media playback.
        /// </remarks>
        public MediaPlaybackProps PropsValid { get => propsValid; }

        /// <summary>
        /// Gets the capabilities of the media playback.
        /// </summary>
        /// <returns>
        /// A bitwise combination of the <see cref="MediaPlaybackCapabilities"/> values
        /// that represents the capabilities of the media playback.
        /// </returns>
        public MediaPlaybackCapabilities PlaybackCaps { get => playbackCaps; }

        /// <summary>
        /// Gets the state of the media playback.
        /// </summary>
        /// <returns>
        /// A <see cref="MediaPlaybackState"/> that represents the current state of the media playback.
        /// </returns>
        public MediaPlaybackState PlaybackState { get => playbackState; }

        /// <summary>
        /// Gets the mode or type of the media playback.
        /// </summary>
        /// <returns>
        /// A <see cref="MediaPlaybackMode"/> that represents the mode or type of the media playback.
        /// </returns>
        public MediaPlaybackMode PlaybackMode { get => playbackMode; }

        /// <summary>
        /// Gets the auto-repeat mode of the media playback.
        /// </summary>
        /// <returns>
        /// A <see cref="MediaPlaybackRepeatMode"/> that represents the auto-repeat mode of the media playback.
        /// </returns>
        public MediaPlaybackRepeatMode RepeatMode { get => repeatMode; }

        /// <summary>
        /// Gets the rate of the media playback.
        /// </summary>
        /// <returns>A <see cref="double"/> that represents the rate of the media playback.</returns>
        public double PlaybackRate { get => playbackRate; }

        /// <summary>
        /// Gets the shuffle state of the media playback.
        /// </summary>
        /// <returns>A <see cref="bool"/> that represents the shuffle state of the media playback.</returns>
        public bool ShuffleEnabled { get => shuffleEnabled != 0; }

        //20279
        /// <summary>
        /// Gets the last playing time of the media playback.
        /// </summary>
        /// <returns>A <see cref="DateTime"/> that represents the last playing time of the media playback.</returns>
        public DateTime LastPlayingFileTime { get => DateTime.FromFileTime(lastPlayingFileTime); }
    }

    /// <summary>
    /// A structure that represents the timeline state of the session (Position, seek ranges etc.).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MediaTimelineProperties
    {
        private readonly TimeSpan startTime;
        private readonly TimeSpan endTime;
        private readonly TimeSpan minSeekTime;
        private readonly TimeSpan maxSeekTime;
        private readonly TimeSpan position;

        //17134+
        private readonly long positionSetFileTime;

        //For future changes etc.. (prevents crash btw)
        private readonly long padding1;

        private readonly long padding2;
        private readonly long padding3;
        private readonly long padding4;

        /// <summary>
        /// Gets the starting timestamp of the current media item.
        /// </summary>
        /// <returns>
        /// A <see cref="TimeSpan"/> that represents the starting timestamp of the current media item.
        /// </returns>
        public TimeSpan StartTime { get => startTime; }

        /// <summary>
        /// Gets the end timestamp of the current media item.
        /// </summary>
        /// <returns>
        /// A <see cref="TimeSpan"/> that represents the end timestamp of the current media item.
        /// </returns>
        public TimeSpan EndTime { get => endTime; }

        /// <summary>
        /// Gets the earliest timestamp at which the current media item can currently seek to.
        /// </summary>
        /// <returns>
        /// A <see cref="TimeSpan"/> that represents the earliest timestamp
        /// at which the current media item can currently seek to.
        /// </returns>
        public TimeSpan MinSeekTime { get => minSeekTime; }

        /// <summary>
        /// Gets the furthest timestamp at which the current media item can currently seek to.
        /// </summary>
        /// <returns>
        /// A <see cref="TimeSpan"/> that represents the furthest timestamp
        /// at which the current media item can currently seek to.
        /// </returns>
        public TimeSpan MaxSeekTime { get => maxSeekTime; }

        /// <summary>
        /// Gets the playback position, current as of <see cref="PositionSetFileTime"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="TimeSpan"/> that represents the playback position,
        /// current as of <see cref="PositionSetFileTime"/>.
        /// </returns>
        public TimeSpan Position { get => position; }

        //17134+
        /// <summary>
        /// Gets the <see cref="DateTime"/> at which the timeline properties were last updated.
        /// </summary>
        /// <returns>
        /// A <see cref="DateTime"/> that represents the last time the timeline properties were updated.
        /// </returns>
        public DateTime PositionSetFileTime { get => DateTime.FromFileTime(positionSetFileTime); }
    }

    //Not related to NPSM COM structs
    /// <summary>
    /// A structure that holds information about the content that the current session has.
    /// </summary>
    public struct MediaObjectInfo
    {
        private readonly string albumArtist;
        private readonly string albumTitle;
        private readonly string subtitle;
        private readonly string title;
        private readonly string artist;
        private readonly string mediaClassPrimaryID;
        private readonly string[] genres;
        private readonly uint albumTrackCount;
        private readonly uint trackNumber;

        /// <summary>
        /// Gets the album's artist.
        /// </summary>
        /// <returns>The name of the album artist.</returns>
        public string AlbumArtist { get => albumArtist; }

        /// <summary>
        /// Gets the title of the album.
        /// </summary>
        /// <returns>The title of the album.</returns>
        public string AlbumTitle { get => albumTitle; }

        /// <summary>
        /// Gets the subtitle.
        /// </summary>
        /// <returns>The subtitle of the media.</returns>
        public string Subtitle { get => subtitle; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <returns>The title of the media.</returns>
        public string Title { get => title; }

        /// <summary>
        /// Gets the artist's name.
        /// </summary>
        /// <returns>The name of the artist.</returns>
        public string Artist { get => artist; }

        /// <summary>
        /// Gets the primary class ID or schema of the media.
        /// </summary>
        /// <returns>The primary class ID or schema of the media.</returns>
        /// <remarks>
        /// Use the <see cref="MediaPlaybackDataSource.MediaSchemaToMediaPlaybackMode(string)"/> method
        /// to get the corresponding <see cref="MediaPlaybackMode"/>.
        /// This property is kept intact due to its flexibility which the <see cref="MediaPlaybackMode"/> enumerations can't provide.
        /// </remarks>
        public string MediaClassPrimaryID { get => mediaClassPrimaryID; }

        /// <summary>
        /// Gets the list of genres.
        /// </summary>
        /// <returns>An array of strings representing the genres.</returns>
        public string[] Genres { get => genres; }

        /// <summary>
        /// Gets the total number of tracks on the album.
        /// </summary>
        /// <returnss>The total number of tracks on the album.</returnss>
        public uint AlbumTrackCount { get => albumTrackCount; }

        /// <summary>
        /// Gets the track's number.
        /// </summary>
        /// <returns>The number associated with the current track.</returns>
        public uint TrackNumber { get => trackNumber; }

        internal MediaObjectInfo(string albumArtist,
            string albumTitle,
            string subtitle,
            string title,
            string artist,
            string mediaClassPrimaryID,
            string[] genres,
            uint albumTrackCount,
            uint trackNumber)
        {
            this.albumArtist = albumArtist;
            this.albumTitle = albumTitle;
            this.subtitle = subtitle;
            this.title = title;
            this.artist = artist;
            this.mediaClassPrimaryID = mediaClassPrimaryID;
            this.genres = genres;
            this.albumTrackCount = albumTrackCount;
            this.trackNumber = trackNumber;
        }
    }
}
