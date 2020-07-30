using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicWorld.View.Models
{
    public class CreateArtistViewModel
    { 
        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
