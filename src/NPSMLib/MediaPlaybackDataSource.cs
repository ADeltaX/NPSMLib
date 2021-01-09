using NPSMLib.Interop;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using static NPSMLib.Interop.COMInterop;

namespace NPSMLib
{
    /// <summary>
    /// Represents the media playback data of a media session and provides the ability to control its playback.
    /// </summary>
    public class MediaPlaybackDataSource
    {
        // The media schema of an image playback.
        private const string ID_MS_MEDIA_SCHEMA_PHOTO = "{6FB2E74A-B8CB-40BB-93F3-FAC5F00FA203}";

        // The media schema of a video playback.
        private const string ID_MS_MEDIA_SCHEMA_VIDEO = "{DB9830BD-3AB3-4FAB-8A37-1A995F7FF74B}";

        // The media schema of a audio music playback.
        private const string ID_MS_MEDIA_SCHEMA_MUSIC = "{D1607DBC-E323-4BE2-86A1-48A42A28441E}";

        private static PROPERTYKEY PKEY_Title = new PROPERTYKEY { fmtid = new Guid("F29F85E0-4FF9-1068-AB91-08002B27B3D9"), pid = 0x2 };
        private static PROPERTYKEY PKEY_Music_TrackNumber = new PROPERTYKEY { fmtid = new Guid("56A3372E-CE9C-11D2-9F0E-006097C686F6"), pid = 0x7 };
        private static PROPERTYKEY PKEY_ThumbnailStream = new PROPERTYKEY { fmtid = new Guid("F29F85E0-4FF9-1068-AB91-08002B27B3D9"), pid = 0x1B };
        private static PROPERTYKEY PKEY_Music_Genre = new PROPERTYKEY { fmtid = new Guid("56A3372E-CE9C-11D2-9F0E-006097C686F6"), pid = 0xB };
        private static PROPERTYKEY PKEY_Music_Artist = new PROPERTYKEY { fmtid = new Guid("56A3372E-CE9C-11D2-9F0E-006097C686F6"), pid = 0x2 };
        private static PROPERTYKEY PKEY_Music_AlbumTitle = new PROPERTYKEY { fmtid = new Guid("56A3372E-CE9C-11D2-9F0E-006097C686F6"), pid = 0x4 };
        private static PROPERTYKEY PKEY_Media_ClassPrimaryID = new PROPERTYKEY { fmtid = new Guid("64440492-4C8B-11D1-8B70-080036B11A03"), pid = 0xD };

        //Untested
        private static PROPERTYKEY PKEY_Music_AlbumArtist = new PROPERTYKEY { fmtid = new Guid("56A3372E-CE9C-11D2-9F0E-006097C686F6"), pid = 0xD };

        private static PROPERTYKEY PKEY_AlbumTrackCount = new PROPERTYKEY { fmtid = new Guid("BAC8804B-BAA1-4E3F-9A11-EFB3EA519859"), pid = 0x2 };
        private static PROPERTYKEY PKEY_Media_SubTitle = new PROPERTYKEY { fmtid = new Guid("56A3372E-CE9C-11D2-9F0E-006097C686F6"), pid = 0x26 };

        private readonly object playbackDataSourceIUnknown;
        private readonly IMediaPlaybackDataSource_20279 playbackDataSource_20279;
        private readonly IMediaPlaybackDataSource_10586 playbackDataSource_10586;
        private readonly int numSelectInterface = 0;

        internal object GetIUnknownInterface { get => playbackDataSourceIUnknown; }

        internal MediaPlaybackDataSource(object playbackDataSourceIUnknown)
        {
            this.playbackDataSourceIUnknown = playbackDataSourceIUnknown;

            //Since QI is a bit costly and 20279 isn't used (Windows Insider build ONLY), it's arranged to test 10586 interface FIRST
            if (playbackDataSourceIUnknown is IMediaPlaybackDataSource_10586 tPlaybackDataSource_10586)
            {
                numSelectInterface = 10586;
                playbackDataSource_10586 = tPlaybackDataSource_10586;
            }
            else if (playbackDataSourceIUnknown is IMediaPlaybackDataSource_20279 tPlaybackDataSource_20279)
            {
                numSelectInterface = 20279;
                playbackDataSource_20279 = tPlaybackDataSource_20279;
            }
            else
            {
                throw new NotSupportedException("QueryInterface failed due to non-available interface/guid");
            }
        }

        /// <summary>
        /// Converts a media schema (or primary class ID) string into the corresponding <see cref="MediaPlaybackMode"/> enumeration.
        /// </summary>
        /// <param name="mediaSchema">The media schema to convert.</param>
        /// <returns>The corresponding <see cref="MediaPlaybackMode"/> enumeration.</returns>
        public static MediaPlaybackMode MediaSchemaToMediaPlaybackMode(string mediaSchema)
        {
            if (mediaSchema == ID_MS_MEDIA_SCHEMA_MUSIC)
                return MediaPlaybackMode.Audio;
            else if (mediaSchema == ID_MS_MEDIA_SCHEMA_VIDEO)
                return MediaPlaybackMode.Video;
            else if (mediaSchema == ID_MS_MEDIA_SCHEMA_PHOTO)
                return MediaPlaybackMode.Image;
            else
                return MediaPlaybackMode.Unknown;
        }

        /// <summary>
        /// Gets the <see cref="MediaObjectInfo"/> for this session, accurate to the time of the call.
        /// </summary>
        /// <returns>A <see cref="MediaObjectInfo"/> that represents the current media info for this session.</returns>
        public MediaObjectInfo GetMediaObjectInfo()
        {
            PROPVARIANT pVariant;
            IPropertyStore propStore;

            string title = "", artist = "", albumTitle = "", mediaClassPrimaryID = "", albumArtist = "", subtitle = "";
            string[] genres = new string[0];
            uint trackNumber = 0, albumTrackCount = 0;

            if (numSelectInterface == 20279)
                playbackDataSource_20279.GetMediaObjectInfo(out propStore);
            else
                playbackDataSource_10586.GetMediaObjectInfo(out propStore);

            if (propStore.GetValue(ref PKEY_Title, out pVariant) == 0 && pVariant.vt == VARTYPE.VT_LPWSTR)
            {
                title = Marshal.PtrToStringUni(pVariant.union.pwszVal);
                NativeMethods.PropVariantClear(ref pVariant);
            }

            if (propStore.GetValue(ref PKEY_Music_Artist, out pVariant) == 0 && pVariant.vt == VARTYPE.VT_LPWSTR)
            {
                artist = Marshal.PtrToStringUni(pVariant.union.pwszVal);
                NativeMethods.PropVariantClear(ref pVariant);
            }

            if (propStore.GetValue(ref PKEY_Music_AlbumTitle, out pVariant) == 0 && pVariant.vt == VARTYPE.VT_LPWSTR)
            {
                albumTitle = Marshal.PtrToStringUni(pVariant.union.pwszVal);
                NativeMethods.PropVariantClear(ref pVariant);
            }

            if (propStore.GetValue(ref PKEY_Music_TrackNumber, out pVariant) == 0 && pVariant.vt == VARTYPE.VT_UI4)
            {
                trackNumber = pVariant.union.ulVal;
                NativeMethods.PropVariantClear(ref pVariant);
            }

            if (propStore.GetValue(ref PKEY_Music_AlbumArtist, out pVariant) == 0 && pVariant.vt == VARTYPE.VT_LPWSTR)
            {
                albumArtist = Marshal.PtrToStringUni(pVariant.union.pwszVal);
                NativeMethods.PropVariantClear(ref pVariant);
            }

            if (propStore.GetValue(ref PKEY_AlbumTrackCount, out pVariant) == 0 && pVariant.vt == VARTYPE.VT_UI4)
            {
                albumTrackCount = pVariant.union.ulVal;
                NativeMethods.PropVariantClear(ref pVariant);
            }

            if (propStore.GetValue(ref PKEY_Media_SubTitle, out pVariant) == 0 && pVariant.vt == VARTYPE.VT_LPWSTR)
            {
                subtitle = Marshal.PtrToStringUni(pVariant.union.pwszVal);
                NativeMethods.PropVariantClear(ref pVariant);
            }

            if (propStore.GetValue(ref PKEY_Music_Genre, out pVariant) == 0 && pVariant.vt == (VARTYPE.VT_VECTOR | VARTYPE.VT_LPWSTR))
            {
                var countStr = pVariant.union.calpwstr.cElems;
                genres = new string[countStr];
                for (int i = 0; i < countStr; i++)
                    genres[i] = Marshal.PtrToStringUni(Marshal.ReadIntPtr(pVariant.union.calpwstr.pElems + (IntPtr.Size * i)));

                NativeMethods.PropVariantClear(ref pVariant);
            }

            if (propStore.GetValue(ref PKEY_Media_ClassPrimaryID, out pVariant) == 0 && pVariant.vt == VARTYPE.VT_LPWSTR)
            {
                mediaClassPrimaryID = Marshal.PtrToStringUni(pVariant.union.pwszVal);
                NativeMethods.PropVariantClear(ref pVariant);
            }

            return new MediaObjectInfo(albumArtist, albumTitle, subtitle, title, artist, mediaClassPrimaryID, genres, albumTrackCount, trackNumber);
        }

        /// <summary>
        /// Gets the stream reference of the thumbnail image associated with this media playback.
        /// </summary>
        /// <returns>
        /// A <see cref="Stream"/> that is associated with the media playback's thumbnail image.
        /// </returns>
        public Stream GetThumbnailStream()
        {
            Stream outStream = null;

            IPropertyStore propStore;

            if (numSelectInterface == 20279)
                playbackDataSource_20279.GetMediaObjectInfo(out propStore);
            else
                playbackDataSource_10586.GetMediaObjectInfo(out propStore);

            if (propStore.GetValue(ref PKEY_ThumbnailStream, out PROPVARIANT pVariant) == 0 && pVariant.vt == VARTYPE.VT_STREAM)
            {
                var inStream = (IStream)Marshal.GetObjectForIUnknown(pVariant.union.pStream);
                outStream = new MemoryStream();

                int cb = 4096;
                byte[] buffer = new byte[cb];
                int read = 0;

                do
                {
                    IntPtr bytesRead = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(int)));
                    try
                    {
                        inStream.Read(buffer, cb, bytesRead);
                        read = Marshal.ReadInt32(bytesRead);
                    }
                    finally
                    {
                        Marshal.FreeCoTaskMem(bytesRead);
                    }
                    outStream.Write(buffer, 0, read);
                } while (read > 0);

                outStream.Seek(0, SeekOrigin.Begin);

                NativeMethods.PropVariantClear(ref pVariant);
            }

            return outStream;
        }

        /// <summary>
        /// Gets the <see cref="MediaPlaybackInfo"/> for this session, accurate to the time of the call.
        /// </summary>
        /// <returns>A <see cref="MediaPlaybackInfo"/> that represents the current playback info for this session.</returns>
        public MediaPlaybackInfo GetMediaPlaybackInfo()
        {
            MediaPlaybackInfo info;
            if (numSelectInterface == 20279)
                playbackDataSource_20279.GetMediaPlaybackInfo(out info);
            else
                playbackDataSource_10586.GetMediaPlaybackInfo(out info);
            return info;
        }

        /// <summary>
        /// Gets the <see cref="MediaTimelineProperties"/> instance that represents this sessions timeline state.
        /// </summary>
        /// <returns>A <see cref="MediaTimelineProperties"/> that represents the timeline property values.</returns>
        public MediaTimelineProperties GetMediaTimelineProperties()
        {
            MediaTimelineProperties props;
            if (numSelectInterface == 20279)
                playbackDataSource_20279.GetMediaTimelineProperties(out props);
            else
                playbackDataSource_10586.GetMediaTimelineProperties(out props);
            return props;
        }

        /// <summary>
        /// Gets the application Id of the source application of the session.
        /// </summary>
        /// <returns>The application Id.</returns>
        public string GetParentApplicationId()
        {
            string id;
            if (numSelectInterface == 20279)
                playbackDataSource_20279.GetParentApplicationId(out id);
            else
                playbackDataSource_10586.GetParentApplicationId(out id);
            return id;
        }

        /// <summary>
        /// Sends a media playback command to the media session.
        /// </summary>
        /// <param name="command">The media playback command to send.</param>
        public void SendMediaPlaybackCommand(MediaPlaybackCommands command)
        {
            if (numSelectInterface == 20279)
                playbackDataSource_20279.SendMediaPlaybackCommand(command);
            else
                playbackDataSource_10586.SendMediaPlaybackCommand(command);
        }

        /// <summary>
        /// Sends a request to the media session to change its playback position to the specified position.
        /// </summary>
        /// <param name="requestedPlaybackPosition">The requested playback position to seek to.</param>
        public void SendPlaybackPositionChangeRequest(TimeSpan requestedPlaybackPosition)
        {
            if (numSelectInterface == 20279)
                playbackDataSource_20279.SendPlaybackPositionChangeRequest(requestedPlaybackPosition.Ticks);
            else
                playbackDataSource_10586.SendPlaybackPositionChangeRequest(requestedPlaybackPosition.Ticks);
        }

        /// <summary>
        /// Sends a request to the media session to change its playback rate to the requested value.
        /// </summary>
        /// <param name="requestedPlaybackRate">The requested playback rate to change to.</param>
        public void SendPlaybackRateChangeRequest(double requestedPlaybackRate)
        {
            if (numSelectInterface == 20279)
                playbackDataSource_20279.SendPlaybackRateChangeRequest(requestedPlaybackRate);
            else
                playbackDataSource_10586.SendPlaybackRateChangeRequest(requestedPlaybackRate);
        }

        /// <summary>
        /// Sends a request to the media session to change its repeat mode to the requested value.
        /// </summary>
        /// <param name="requestedRepeatMode">The requested auto repeat mode.</param>
        public void SendRepeatModeChangeRequest(MediaPlaybackRepeatMode requestedRepeatMode)
        {
            if (numSelectInterface == 20279)
                playbackDataSource_20279.SendRepeatModeChangeRequest(requestedRepeatMode);
            else
                playbackDataSource_10586.SendRepeatModeChangeRequest(requestedRepeatMode);
        }

        /// <summary>
        /// Sends a request to the media session to change its shuffle state to the requested value.
        /// </summary>
        /// <param name="requestedShuffle">The requested shuffle state to switch to.</param>
        public void SendShuffleEnabledChangeRequest(bool requestedShuffle)
        {
            if (numSelectInterface == 20279)
                playbackDataSource_20279.SendShuffleEnabledChangeRequest(requestedShuffle);
            else
                playbackDataSource_10586.SendShuffleEnabledChangeRequest(requestedShuffle);
        }

        #region Event

        private MediaPlaybackDataChangedEventHandler eventHandler;

        private readonly object subscriptionLock = new object();

        private event EventHandler<MediaPlaybackDataChangedArgs> _mediaPlaybackDataChanged;

        /// <summary>
        /// Occurs when the session's media playback data have changed.
        /// </summary>
        public event EventHandler<MediaPlaybackDataChangedArgs> MediaPlaybackDataChanged
        {
            add
            {
                lock (subscriptionLock)
                {
                    if (_mediaPlaybackDataChanged == null)
                    {
                        eventHandler = new MediaPlaybackDataChangedEventHandler(this);
                        NPSMEventRegistrationToken token;
                        if (numSelectInterface == 20279)
                            playbackDataSource_20279.RegisterEventHandler(eventHandler, out token);
                        else
                            playbackDataSource_10586.RegisterEventHandler(eventHandler, out token);
                        eventHandler.Token = token;
                    }

                    _mediaPlaybackDataChanged += value;
                }
            }
            remove
            {
                lock (subscriptionLock)
                {
                    _mediaPlaybackDataChanged -= value;
                    if (_mediaPlaybackDataChanged == null)
                    {
                        if (numSelectInterface == 20279)
                            playbackDataSource_20279.UnregisterEventHandler(eventHandler.Token);
                        else
                            playbackDataSource_10586.UnregisterEventHandler(eventHandler.Token);
                        eventHandler = null;
                    }
                }
            }
        }

        private class MediaPlaybackDataChangedEventHandler : IMediaPlaybackDataChangedEventHandler
        {
            internal NPSMEventRegistrationToken Token { get; set; }
            private MediaPlaybackDataSource CurrentMediaPlaybackInstance { get; set; }

            public MediaPlaybackDataChangedEventHandler(MediaPlaybackDataSource currentSessionInstance)
            {
                CurrentMediaPlaybackInstance = currentSessionInstance;
            }

            public void OnMediaPlaybackDataChangedEvent(object source /* IMediaPlaybackDataSource */, MediaPlaybackDataChangedEvent dataChangedEvent)
            {
                CurrentMediaPlaybackInstance._mediaPlaybackDataChanged?.Invoke(CurrentMediaPlaybackInstance,
                    new MediaPlaybackDataChangedArgs { MediaPlaybackDataSource = new MediaPlaybackDataSource(source), DataChangedEvent = dataChangedEvent });
            }
        }

        #endregion
    }

    /// <summary>
    /// Represents arguments for a <see cref="MediaPlaybackDataSource.MediaPlaybackDataChanged"/> event.
    /// </summary>
    public class MediaPlaybackDataChangedArgs : EventArgs
    {
        /// <summary>
        /// Gets the <see cref="MediaPlaybackDataSource"/> which raised the event.
        /// </summary>
        /// <returns>
        /// The <see cref="MediaPlaybackDataSource"/> which raised the event.
        /// </returns>
        public MediaPlaybackDataSource MediaPlaybackDataSource { get; internal set; }

        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        /// <returns>A <see cref="MediaPlaybackDataChangedEvent"/> which represents the type of the event.</returns>
        public MediaPlaybackDataChangedEvent DataChangedEvent { get; internal set; }
    }
}
