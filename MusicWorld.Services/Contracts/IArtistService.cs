using MusicWorld.Data.Models;
using MusicWorld.Services.MusicWorld.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicWorld.Services.Contracts
{
    public interface IArtistService
    {
        string Create(ArtistServiceModel artistService);

        Artist GetArtistsById(string artistId);

        string Update(ArtistServiceModel artistService,string id);

        bool Delete(string id);
    }
}
