
using MusicWorld.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicWorld.Services.Contracts
{
    public interface IAlbumService
    {
        string Create(string name, string artistId, DateTime releaseDate);
         
    }
}
