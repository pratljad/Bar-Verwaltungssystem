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

        private static void addSong(int ID, string Titel, string Interpret)
        {
            Playlist.Add(new Song(ID, Titel, Interpret));
        }
    }
}
