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
        private const string ID_MS_MEDIA_SCHEMA_PHOTO = "{6FB2E74A-B8CB-40BB-93F3-FAC5F00FA203}";
        private const string ID_MS_MEDIA_SCHEMA_VIDEO = "{DB9830BD-3AB3-4FAB-8A37-1A995F7FF74B}";
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

        private readonly ushort osbuild;
        private readonly object playbackDataSourceIUnknown;
        private readonly IMediaPlaybackDataSource_10586 playbackDataSource_10586;
        private readonly IMediaPlaybackDataSource_20279 playbackDataSource_20279;

        internal object GetIUnknownInterface { get => playbackDataSourceIUnknown; }

        internal MediaPlaybackDataSource(object playbackDataSourceIUnknown, ushort OSBuild)
        {
            this.osbuild = OSBuild;
            this.playbackDataSourceIUnknown = playbackDataSourceIUnknown;
            if (osbuild >= 20279)
                playbackDataSource_20279 = (IMediaPlaybackDataSource_20279)playbackDataSourceIUnknown;
            else
                playbackDataSource_10586 = (IMediaPlaybackDataSource_10586)playbackDataSourceIUnknown;
        }

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
            IPropertyStore propStore;

            string title = "", artist = "", albumTitle = "", mediaClassPrimaryID = "", albumArtist = "", subtitle = "";
            string[] genres = new string[0];
            uint trackNumber = 0, albumTrackCount = 0;

            if (osbuild >= 20279)
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

        public Stream GetThumbnailStream()
        {
            Stream outStream = null;

            IPropertyStore propStore;

            if (osbuild >= 20279)
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

                NativeMethods.PropVariantClear(ref pVariant);
            }

            return outStream;
        }

        public MediaPlaybackInfo GetMediaPlaybackInfo()
        {
            MediaPlaybackInfo info;
            if (osbuild >= 20279)
                playbackDataSource_20279.GetMediaPlaybackInfo(out info);
            else
                playbackDataSource_10586.GetMediaPlaybackInfo(out info);
            return info;
        }

        public MediaTimelineProperties GetMediaTimelineProperties()
        {
            MediaTimelineProperties props;
            if (osbuild >= 20279)
                playbackDataSource_20279.GetMediaTimelineProperties(out props);
            else
                playbackDataSource_10586.GetMediaTimelineProperties(out props);
            return props;
        }

        public string GetParentApplicationId()
        {
            string id;
            if (osbuild >= 20279)
                playbackDataSource_20279.GetParentApplicationId(out id);
            else
                playbackDataSource_10586.GetParentApplicationId(out id);
            return id;
        }

        public void SendMediaPlaybackCommand(MediaPlaybackCommands command)
        {
            if (osbuild >= 20279)
                playbackDataSource_20279.SendMediaPlaybackCommand(command);
            else
                playbackDataSource_10586.SendMediaPlaybackCommand(command);
        }

        public void SendPlaybackPositionChangeRequest(long requestedPlaybackPosition)
        {
            if (osbuild >= 20279)
                playbackDataSource_20279.SendPlaybackPositionChangeRequest(requestedPlaybackPosition);
            else
                playbackDataSource_10586.SendPlaybackPositionChangeRequest(requestedPlaybackPosition);
        }

        public void SendPlaybackRateChangeRequest(double requestedPlaybackRate)
        {
            if (osbuild >= 20279)
                playbackDataSource_20279.SendPlaybackRateChangeRequest(requestedPlaybackRate);
            else
                playbackDataSource_10586.SendPlaybackRateChangeRequest(requestedPlaybackRate);
        }

        public void SendRepeatModeChangeRequest(MediaPlaybackRepeatMode requestedRepeatMode)
        {
            if (osbuild >= 20279)
                playbackDataSource_20279.SendRepeatModeChangeRequest(requestedRepeatMode);
            else
                playbackDataSource_10586.SendRepeatModeChangeRequest(requestedRepeatMode);
        }

        public void SendShuffleEnabledChangeRequest(bool requestedShuffle)
        {
            if (osbuild >= 20279)
                playbackDataSource_20279.SendShuffleEnabledChangeRequest(requestedShuffle);
            else
                playbackDataSource_10586.SendShuffleEnabledChangeRequest(requestedShuffle);
        }

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
                        eventHandler = new MediaPlaybackDataChangedEventHandler(this, osbuild);
                        NPSMEventRegistrationToken token;
                        if (osbuild >= 20279)
                            playbackDataSource_20279.RegisterEventHandler(eventHandler, out token);
                        else
                            playbackDataSource_10586.RegisterEventHandler(eventHandler, out token);
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
                        if (osbuild >= 20279)
                            playbackDataSource_20279.UnregisterEventHandler(eventHandler.Token);
                        else
                            playbackDataSource_10586.UnregisterEventHandler(eventHandler.Token);
                        eventHandler = null;
                    }

                    bMediaPlaybackDataChanged -= value;
                }
            }
        }

        class MediaPlaybackDataChangedEventHandler : IMediaPlaybackDataChangedEventHandler
        {
            internal NPSMEventRegistrationToken Token { get; set; }
            private MediaPlaybackDataSource CurrentMediaPlaybackInstance { get; set; }
            private ushort OSBuild { get; set; }
            public MediaPlaybackDataChangedEventHandler(MediaPlaybackDataSource currentSessionInstance, ushort osbuild)
            {
                CurrentMediaPlaybackInstance = currentSessionInstance;
                OSBuild = osbuild;
            }

            public void OnMediaPlaybackDataChangedEvent(object source /* IMediaPlaybackDataSource */, MediaPlaybackDataChangedEvent dataChangedEvent)
            {
                CurrentMediaPlaybackInstance.bMediaPlaybackDataChanged?.Invoke(CurrentMediaPlaybackInstance, 
                    new MediaPlaybackDataChangedArgs { MediaPlaybackDataSource = new MediaPlaybackDataSource(source, OSBuild), DataChangedEvent = dataChangedEvent });
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
