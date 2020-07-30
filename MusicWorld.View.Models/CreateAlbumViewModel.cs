using MusicWorld.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace MusicWorld.View.Models
{
    public class CreateAlbumViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ArtistId { get; set; }

        [Required]
        public Artist Artist { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }
         
    }
}
