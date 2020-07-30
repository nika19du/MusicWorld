using MusicWorld.Data;
using MusicWorld.Data.Models;
using MusicWorld.Services.Contracts;
using MusicWorld.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicWorld.Services
{ 
   
    public class UserServices : IUserServices
    {
        private readonly MusicWorldContext context; 

        public UserServices(MusicWorldContext context)
        {
            this.context = context;
        }

        public string CreateAccount(string username, string password, string firstName, string lastName)
        {
            var user = new User()
            {
                Username = username,
                Password = password,
                FirstName = firstName,
                LastName = lastName
            };
            if (context.Users.Count() == 0)
            {
                user.IsAdmin = true;
            }
            else
            {
                user.IsAdmin = false;
            } 
            this.context.Users.Add(user);
            this.context.SaveChanges();

            return user.Id;
        }

        public string Login(string username, string password)
        {
            var user = context.Users.FirstOrDefault(x => x.Username == username && x.Password == password);

            if (user.Id != null)
            {
                AccountService.UsrId = user.Id;
                return user.Id;
            }
            else return "not found";
        }
         
        public string Update(CreateUserViewModel userService, string id)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return "not found";
            }
            else
            {
                user.Username = userService.Username;
                user.FirstName = userService.FirstName;
                user.LastName = userService.LastName;
                user.Password = userService.Password;

                context.Users.Update(user);
                context.SaveChanges();

                return user.Id;
            }
        }

        public bool Delete(string id)
        {
            var u = context.Users.FirstOrDefault(x => x.Id == id);
            if (u != null)
            {
                context.Users.Remove(u);
                context.SaveChanges();
                return true;
            }
            else return false;
        }
    }
}
