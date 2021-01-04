using NPSMLib.Interop;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using static NPSMLib.Interop.COMInterop;

namespace NPSMLib
{
    public class MediaPlaybackDataSource
    {
        const string ID_MS_MEDIA_SCHEMA_PHOTO = "{6FB2E74A-B8CB-40BB-93F3-FAC5F00FA203}";
        const string ID_MS_MEDIA_SCHEMA_VIDEO = "{DB9830BD-3AB3-4FAB-8A37-1A995F7FF74B}";
        const string ID_MS_MEDIA_SCHEMA_MUSIC = "{D1607DBC-E323-4BE2-86A1-48A42A28441E}";

        static PROPERTYKEY PKEY_Title = new PROPERTYKEY { fmtid = new Guid("F29F85E0-4FF9-1068-AB91-08002B27B3D9"), pid = 0x2 };
        static PROPERTYKEY PKEY_Music_TrackNumber = new PROPERTYKEY { fmtid = new Guid("56A3372E-CE9C-11D2-9F0E-006097C686F6"), pid = 0x7 };
        static PROPERTYKEY PKEY_ThumbnailStream = new PROPERTYKEY { fmtid = new Guid("F29F85E0-4FF9-1068-AB91-08002B27B3D9"), pid = 0x1B };
        static PROPERTYKEY PKEY_Music_Genre = new PROPERTYKEY { fmtid = new Guid("56A3372E-CE9C-11D2-9F0E-006097C686F6"), pid = 0xB };
        static PROPERTYKEY PKEY_Music_Artist = new PROPERTYKEY { fmtid = new Guid("56A3372E-CE9C-11D2-9F0E-006097C686F6"), pid = 0x2 };
        static PROPERTYKEY PKEY_Music_AlbumTitle = new PROPERTYKEY { fmtid = new Guid("56A3372E-CE9C-11D2-9F0E-006097C686F6"), pid = 0x4 };
        static PROPERTYKEY PKEY_Media_ClassPrimaryID = new PROPERTYKEY { fmtid = new Guid("64440492-4C8B-11D1-8B70-080036B11A03"), pid = 0xD };

        //Untested
        static PROPERTYKEY PKEY_Music_AlbumArtist = new PROPERTYKEY { fmtid = new Guid("56A3372E-CE9C-11D2-9F0E-006097C686F6"), pid = 0xD };
        static PROPERTYKEY PKEY_AlbumTrackCount = new PROPERTYKEY { fmtid = new Guid("BAC8804B-BAA1-4E3F-9A11-EFB3EA519859"), pid = 0x2 };
        static PROPERTYKEY PKEY_Media_SubTitle = new PROPERTYKEY { fmtid = new Guid("56A3372E-CE9C-11D2-9F0E-006097C686F6"), pid = 0x26 };
        IMediaPlaybackDataSource playbackDataSource;
        internal IMediaPlaybackDataSource MediaPlaybackDataSourceInterface { get => playbackDataSource; }

        internal MediaPlaybackDataSource(IMediaPlaybackDataSource source) => playbackDataSource = source;

        public static MediaPlaybackType MediaSchemaToMediaPlaybackType(string mediaSchema)
        {
            if (mediaSchema == ID_MS_MEDIA_SCHEMA_MUSIC)
                return MediaPlaybackType.Music;
            else if (mediaSchema == ID_MS_MEDIA_SCHEMA_VIDEO)
                return MediaPlaybackType.Video;
            else if (mediaSchema == ID_MS_MEDIA_SCHEMA_PHOTO)
                return MediaPlaybackType.Image;
            else
                return MediaPlaybackType.Unknown;
        }

        public MediaObjectInfo GetMediaObjectInfo()
        {
            PROPVARIANT pVariant;

            string title = "", artist = "", albumTitle = "", mediaClassPrimaryID = "", albumArtist = "", subtitle = "";
            string[] genres = new string[0];
            uint trackNumber = 0, albumTrackCount = 0;

            playbackDataSource.GetMediaObjectInfo(out var propVar);

            if (propVar.GetValue(ref PKEY_Title, out pVariant) == 0 && pVariant.vt == VARTYPE.VT_LPWSTR)
            {
                title = Marshal.PtrToStringUni(pVariant.union.pwszVal);
                NativeMethods.PropVariantClear(ref pVariant);
            }

            if (propVar.GetValue(ref PKEY_Music_Artist, out pVariant) == 0 && pVariant.vt == VARTYPE.VT_LPWSTR)
            {
                artist = Marshal.PtrToStringUni(pVariant.union.pwszVal);
                NativeMethods.PropVariantClear(ref pVariant);
            }

            if (propVar.GetValue(ref PKEY_Music_AlbumTitle, out pVariant) == 0 && pVariant.vt == VARTYPE.VT_LPWSTR)
            {
                albumTitle = Marshal.PtrToStringUni(pVariant.union.pwszVal);
                NativeMethods.PropVariantClear(ref pVariant);
            }

            if (propVar.GetValue(ref PKEY_Music_TrackNumber, out pVariant) == 0 && pVariant.vt == VARTYPE.VT_UI4)
            {
                trackNumber = pVariant.union.ulVal;
                NativeMethods.PropVariantClear(ref pVariant);
            }

            if (propVar.GetValue(ref PKEY_Music_AlbumArtist, out pVariant) == 0 && pVariant.vt == VARTYPE.VT_LPWSTR)
            {
                albumArtist = Marshal.PtrToStringUni(pVariant.union.pwszVal);
                NativeMethods.PropVariantClear(ref pVariant);
            }

            if (propVar.GetValue(ref PKEY_AlbumTrackCount, out pVariant) == 0 && pVariant.vt == VARTYPE.VT_UI4)
            {
                albumTrackCount = pVariant.union.ulVal;
                NativeMethods.PropVariantClear(ref pVariant);
            }

            if (propVar.GetValue(ref PKEY_Media_SubTitle, out pVariant) == 0 && pVariant.vt == VARTYPE.VT_LPWSTR)
            {
                subtitle = Marshal.PtrToStringUni(pVariant.union.pwszVal);
                NativeMethods.PropVariantClear(ref pVariant);
            }

            if (propVar.GetValue(ref PKEY_Music_Genre, out pVariant) == 0 && pVariant.vt == (VARTYPE.VT_VECTOR | VARTYPE.VT_LPWSTR))
            {
                var countStr = pVariant.union.calpwstr.cElems;
                genres = new string[countStr];
                for (int i = 0; i < countStr; i++)
                    genres[i] = Marshal.PtrToStringUni(Marshal.ReadIntPtr(pVariant.union.calpwstr.pElems + (IntPtr.Size * i)));

                NativeMethods.PropVariantClear(ref pVariant);
            }

            if (propVar.GetValue(ref PKEY_Media_ClassPrimaryID, out pVariant) == 0 && pVariant.vt == VARTYPE.VT_LPWSTR)
            {
                mediaClassPrimaryID = Marshal.PtrToStringUni(pVariant.union.pwszVal);
                NativeMethods.PropVariantClear(ref pVariant);
            }

            return new MediaObjectInfo(albumArtist, albumTitle, subtitle, title, artist, mediaClassPrimaryID, genres, albumTrackCount, trackNumber);
        }

        public Stream GetThumbnailStream()
        {
            Stream outStream = null;

            playbackDataSource.GetMediaObjectInfo(out var propVar);
            if (propVar.GetValue(ref PKEY_ThumbnailStream, out PROPVARIANT pVariant) == 0 && pVariant.vt == VARTYPE.VT_STREAM)
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

                NativeMethods.PropVariantClear(ref pVariant);
            }

            return outStream;
        }

        public MediaPlaybackInfo GetMediaPlaybackInfo()
        {
            playbackDataSource.GetMediaPlaybackInfo(out var info);
            return info;
        }

        public MediaTimelineProperties GetMediaTimelineProperties()
        {
            playbackDataSource.GetMediaTimelineProperties(out var props);
            return props;
        }

        public string GetParentApplicationId()
        {
            playbackDataSource.GetParentApplicationId(out var id);
            return id;
        }

        public void SendMediaPlaybackCommand(MediaPlaybackCommands command) => playbackDataSource.SendMediaPlaybackCommand(command);

        public void SendPlaybackPositionChangeRequest(long requestedPlaybackPosition) => playbackDataSource.SendPlaybackPositionChangeRequest(requestedPlaybackPosition);

        public void SendPlaybackRateChangeRequest(double requestedPlaybackRate) => playbackDataSource.SendPlaybackRateChangeRequest(requestedPlaybackRate);

        public void SendRepeatModeChangeRequest(MediaPlaybackRepeatMode requestedRepeatMode) => playbackDataSource.SendRepeatModeChangeRequest(requestedRepeatMode);

        public void SendShuffleEnabledChangeRequest(bool requestedShuffle) => playbackDataSource.SendShuffleEnabledChangeRequest(requestedShuffle);

        #region Event

        MediaPlaybackDataChangedEventHandler eventHandler;

        int subscribers = 0;
        readonly object objectLock = new object();

        private event EventHandler<MediaPlaybackDataChangedArgs> bMediaPlaybackDataChanged;
        public event EventHandler<MediaPlaybackDataChangedArgs> MediaPlaybackDataChanged
        {
            add
            {
                lock (objectLock)
                {
                    if (subscribers == 0)
                    {
                        eventHandler = new MediaPlaybackDataChangedEventHandler(this);
                        var akka = playbackDataSource.RegisterEventHandler(eventHandler, out var token);
                        eventHandler.Token = token;
                    }
                    subscribers++;

                    bMediaPlaybackDataChanged += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    subscribers--;
                    if (subscribers == 0)
                    {
                        playbackDataSource.UnregisterEventHandler(eventHandler.Token);
                        eventHandler = null;
                    }

                    bMediaPlaybackDataChanged -= value;
                }
            }
        }

        class MediaPlaybackDataChangedEventHandler : IMediaPlaybackDataChangedEventHandler
        {
            internal lEventRegistrationToken Token { get; set; }
            private MediaPlaybackDataSource CurrentMediaPlaybackInstance { get; set; }
            public MediaPlaybackDataChangedEventHandler(MediaPlaybackDataSource currentSessionInstance)
            {
                CurrentMediaPlaybackInstance = currentSessionInstance;
            }

            public void OnMediaPlaybackDataChangedEvent(IMediaPlaybackDataSource source, MediaPlaybackDataChangedEvent dataChangedEvent)
            {
                CurrentMediaPlaybackInstance.bMediaPlaybackDataChanged?.Invoke(CurrentMediaPlaybackInstance, 
                    new MediaPlaybackDataChangedArgs { MediaPlaybackDataSource = new MediaPlaybackDataSource(source), DataChangedEvent = dataChangedEvent });
            }
        }

        #endregion
    }

    public class MediaPlaybackDataChangedArgs : EventArgs
    {
        public MediaPlaybackDataSource MediaPlaybackDataSource { get; internal set; }
        public MediaPlaybackDataChangedEvent DataChangedEvent { get; internal set; }
    }
}
