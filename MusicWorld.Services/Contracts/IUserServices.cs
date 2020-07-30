using MusicWorld.Data.Models;
using MusicWorld.View.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicWorld.Services.Contracts
{
    public interface IUserServices
    {
        string CreateAccount(string username, string password, string firstName, string lastName);

        string Login(string username, string password);

        string Update(CreateUserViewModel userService, string id);

        bool Delete(string id);
    }
}
