using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.Entity;

namespace Music.Service
{
    interface SongService
    {
        Song UploadSong(Song song);
        List<Song> GetNewSongs();
        List<Song> GetMySongs();
    }
}
