using NPSMLib;
using System;
using System.Timers;

namespace NPSM_CLI
{
    class Program
    {
        static readonly object lockObject = new object();
        static NowPlayingSessionManager manager;
        static NowPlayingSession session;
        static MediaPlaybackDataSource src;

        static void Main(string[] args)
        {
            manager = new NowPlayingSessionManager();
            manager.SessionListChanged += SessionListChanged;
            SessionListChanged(null, null);

            char letter;
            do
            {
                var key = Console.ReadKey(true);
                letter = key.KeyChar.ToString().ToLower()[0];

                if (letter == 'p' || key.Key == ConsoleKey.Spacebar)
                    src.SendMediaPlaybackCommand(MediaPlaybackCommands.PlayPauseToggle); //MediaPlaybackCommands.Play or MediaPlaybackCommands.Pause

                if (letter == 'd' || key.Key == ConsoleKey.RightArrow)
                    src.SendMediaPlaybackCommand(MediaPlaybackCommands.Next);

                if (letter == 'a' || key.Key == ConsoleKey.LeftArrow)
                    src.SendMediaPlaybackCommand(MediaPlaybackCommands.Previous);

                if (letter == 'r' && session != null)
                    src.SendMediaPlaybackCommand(MediaPlaybackCommands.Rewind);

                if (letter == 'f' && session != null)
                    src.SendMediaPlaybackCommand(MediaPlaybackCommands.FastForward);

            } while (letter != 'q');
        }

        private static void SessionListChanged(object sender, NowPlayingSessionManagerEventArgs e)
        {
            session = manager.CurrentSession;
            SetupEvents();
            PrintCurrentSession();
        }

        private static void SetupEvents()
        {
            if (session != null)
            {
                src = session.ActivateMediaPlaybackDataSource();
                src.MediaPlaybackDataChanged += M_MediaPlaybackDataChanged;
            }
        }

        private static void M_MediaPlaybackDataChanged(object sender, MediaPlaybackDataChangedArgs e)
        {
            PrintCurrentSession();
        }

        private static void PrintCurrentSession()
        {
            if (session != null)
            {
                lock (lockObject)
                {
                    Console.Clear();

                    var mediaDetails = src.GetMediaObjectInfo();
                    var mediaPlaybackInfo = src.GetMediaPlaybackInfo();
                    var mediaTimeline = src.GetMediaTimelineProperties();

                    Console.WriteLine("Title: " + mediaDetails.Title);
                    Console.WriteLine("Artist: " + mediaDetails.Artist);
                    Console.WriteLine("Subtitle: " + mediaDetails.Subtitle);
                    Console.WriteLine("Album title: " + mediaDetails.AlbumTitle);
                    Console.WriteLine("Media type: " + MediaPlaybackDataSource.MediaSchemaToMediaPlaybackMode(mediaDetails.MediaClassPrimaryID));
                    Console.WriteLine("Source App Id: " + session?.SourceAppId);
                    Console.WriteLine("Source Device Id: " + session?.SourceDeviceId);
                    Console.WriteLine("Render Device Id: " + session?.RenderDeviceId);
                    Console.WriteLine("HWND: 0x" + session?.Hwnd.ToString("X"));
                    Console.WriteLine("PID: 0x" + session?.PID.ToString("X"));
                    Console.WriteLine();
                    Console.WriteLine("Playback state: " + mediaPlaybackInfo.PlaybackState);
                    Console.WriteLine("Playback rate: " + mediaPlaybackInfo.PlaybackRate);
                    Console.WriteLine("Playback mode: " + mediaPlaybackInfo.PlaybackMode);
                    Console.WriteLine("Shuffle enabled: " + mediaPlaybackInfo.ShuffleEnabled);
                    Console.WriteLine("Repeat mode: " + mediaPlaybackInfo.RepeatMode);
                    Console.WriteLine("Playback CAPS: " + mediaPlaybackInfo.PlaybackCaps);
                    Console.WriteLine("Playback PROPS: " + mediaPlaybackInfo.PropsValid);
                    Console.WriteLine();
                    Console.WriteLine("Start time: " + mediaTimeline.StartTime);
                    Console.WriteLine("End time: " + mediaTimeline.EndTime);
                    Console.WriteLine("Min seek time: " + mediaTimeline.MinSeekTime);
                    Console.WriteLine("Max seek time: " + mediaTimeline.MaxSeekTime);
                    Console.WriteLine("Position: " + mediaTimeline.Position);
                    Console.WriteLine("---------------------------------");
                }
            }
            else
            {
                lock (lockObject)
                {
                    Console.Clear();
                    Console.WriteLine("There are no active sessions.");
                }
            }
        }
    }
}
