using NPSMLib;
using System;

namespace NPSM_CLI
{
    class Program
    {
        static readonly object lockObject = new object();
        static NowPlayingSessionManager manager;
        static NowPlayingSession session;
        static MediaPlaybackDataSource src;

        static ushort GetCurrentWindowsBuild() => (ushort)Environment.OSVersion.Version.Build;

        static void Main(string[] args)
        {
            manager = new NowPlayingSessionManager(GetCurrentWindowsBuild());
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

                //TODO: REWIND/FASTFORWARD

                //if (letter == 'r' && session != null)
                //    src.SendMediaPlaybackCommand(MediaPlaybackCommands.Rewind);

                //if (letter == 'f' && session != null)
                //    src.SendMediaPlaybackCommand(MediaPlaybackCommands.FastForward);

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

                    Console.WriteLine("Title: " + mediaDetails.Title);
                    Console.WriteLine("Artist: " + mediaDetails.Artist);
                    Console.WriteLine("Subtitle: " + mediaDetails.Subtitle);
                    Console.WriteLine("Album title: " + mediaDetails.AlbumTitle);
                    Console.WriteLine("Source AppId: " + session.SourceAppId);
                    Console.WriteLine("HWND: 0x" + session.Hwnd.ToString("X"));
                    Console.WriteLine("Status: " + src.GetMediaPlaybackInfo().PlaybackState);
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
