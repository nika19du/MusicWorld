﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MusicWorld.Data.Models
{
    public class Song
    { 

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Duration { get; set; }

        [DisplayName("Album: ")]
        public string AlbumId { get; set; }

        public Album Album { get; set; }

        [DisplayName("Artist: ")]
        public string ArtistId { get; set; }

        public Artist Artist { get; set; } 
    }
}
