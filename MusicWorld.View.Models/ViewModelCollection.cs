using MusicWorld.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicWorld.View.Models
{
    public class ViewModelCollection
    {

        public IEnumerable<Artist> Artists { get; set; }
        public IEnumerable<Album> Albums { get; set; }
    }
}
