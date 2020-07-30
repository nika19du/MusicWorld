using MusicWorld.Data;
using MusicWorld.Data.Models;
using MusicWorld.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicWorld.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly MusicWorldContext context;

        public AlbumService(MusicWorldContext context)
        {
            this.context = context;
        }
        public string Create(string name, string artistId, DateTime releaseDate)
        {
            var artis = context.Artists.FirstOrDefault(x => x.Name == artistId);
            if (artis==null)
            {
                return "not found";
            }
            Album album = new Album()
            {
                Name = name,
                ArtistId = artistId,
                Artist = artis,
                ReleaseDate = releaseDate
            };
            artis.Albums.Add(album);
            context.Albums.Add(album);
            context.SaveChanges();

            return album.Id;
        }
    }
}
