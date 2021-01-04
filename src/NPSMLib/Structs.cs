using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NPSMLib
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MediaPlaybackInfo
    {
        readonly MediaPlaybackProps propsValid;
        readonly MediaPlaybackCapabilities playbackCaps;
        readonly MediaPlaybackState playbackState;
        readonly MediaPlaybackMode playbackMode;
        readonly MediaPlaybackRepeatMode repeatMode;
        readonly double playbackRate;
        readonly int shuffleEnabled;

        public MediaPlaybackProps PropsValid { get => propsValid; }
        public MediaPlaybackCapabilities PlaybackCaps { get => playbackCaps; }
        public MediaPlaybackState PlaybackState { get => playbackState; }
        public MediaPlaybackMode PlaybackMode { get => playbackMode; }
        public MediaPlaybackRepeatMode RepeatMode { get => repeatMode; }
        public double PlaybackRate { get => playbackRate; }
        public bool ShuffleEnabled { get => shuffleEnabled != 0; }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaTimelineProperties
    {
        readonly TimeSpan startTime;
        readonly TimeSpan endTime;
        readonly TimeSpan minSeekTime;
        readonly TimeSpan maxSeekTime;
        readonly TimeSpan position;
        readonly long positionSetFileTime;

        public TimeSpan StartTime { get => startTime; }
        public TimeSpan EndTime { get => endTime; }
        public TimeSpan MinSeekTime { get => minSeekTime; }
        public TimeSpan MaxSeekTime { get => maxSeekTime; }
        public TimeSpan Position { get => position; }
        public DateTime PositionSetFileTime { get => DateTime.FromFileTime(positionSetFileTime); }
    }


    //Not related to COM structs
    public struct MediaObjectInfo
    {
        readonly string albumArtist;
        readonly string albumTitle;
        readonly string subtitle;
        readonly string title;
        readonly string artist;
        readonly string mediaClassPrimaryID;
        readonly string[] genres;
        readonly uint albumTrackCount;
        readonly uint trackNumber;

        public string AlbumArtist { get => albumArtist; }
        public string AlbumTitle { get => albumTitle; }
        public string Subtitle { get => subtitle; }
        public string Title { get => title; }
        public string Artist { get => artist; }
        public string MediaClassPrimaryID { get => mediaClassPrimaryID; }
        public string[] Genres { get => genres; }
        public uint AlbumTrackCount { get => albumTrackCount; }
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
