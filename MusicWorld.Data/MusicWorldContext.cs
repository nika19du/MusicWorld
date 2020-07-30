using Microsoft.EntityFrameworkCore;
using MusicWorld.Data.Models;
using System;

namespace MusicWorld.Data
{
    public class MusicWorldContext :DbContext
    {
        public MusicWorldContext()
        {

        }
        public MusicWorldContext(DbContextOptions<MusicWorldContext> options):base(options)
        {}
        public DbSet<User> Users { get; set; }
        public DbSet<Album> Albums { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Catalog> Catalogs { get; 
set; }

        public DbSet<Song> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=MusicWorldContext;Trusted_Connection=true;"); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 

            modelBuilder.Entity<User>()
                .HasMany(user => user.Catalogs)
                .WithOne(catalog => catalog.User)
                .HasForeignKey(catalog => catalog.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Album>()
                .HasOne(album => album.Artist)
                .WithMany(artist => artist.Albums)
                .HasForeignKey(album => album.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Song>()
                .HasOne(song => song.Album)
                .WithMany(album => album.Songs)
                .HasForeignKey(song => song.AlbumId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Song>()
                .HasOne(song => song.Artist)
                .WithMany(artist => artist.Songs)
                .HasForeignKey(artist => artist.ArtistId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasData(new User()
                {
                    Id = "c2b43fad-0de9-4e30-94dd-9d8280f3e8b5",
                    Username = "admin",
                    Password = "123",
                    FirstName = "Admin",
                    LastName="Adminov",
                    IsAdmin=true
                },
            new User()
            {
                Id = "81c44422-e2bf-400d-bc76-a7485f985ac7",
                Username = "user",
                Password = "123",
                FirstName = "User",
                LastName = "Userov",
                IsAdmin = false
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
