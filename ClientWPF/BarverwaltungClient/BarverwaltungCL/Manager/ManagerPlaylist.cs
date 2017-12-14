using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BarverwaltungCL.Barverwaltung;

namespace BarverwaltungCL.Manager
{
    public static class ManagerPlaylist
    {
        private static List<Song> Playlist;
        
        public static IList<Song> getPlaylist()
        {
            return Playlist;
        }

        public static void init()
        {
            Playlist = new List<Song>();
        }

        public static void addSong(Song s)
        {
            Playlist.Add(s);
        }

        public static void addSong(int ID, int Tischnummer, string Titel, string Interpret)
        {
            Playlist.Add(new Song(ID, Tischnummer, Titel, Interpret));
        }

        public static void removeSong(Song s)
        {
            Playlist.Remove(s);
        }

        public static void removeSong(int ID)
        {
            foreach(Song s in Playlist)
            {
                if(s.IDSong == ID)
                {
                    Playlist.Remove(s);
                }
            }
        }
    }
}
