﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tourixhub.Domain;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Friendship> Friends { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<Story> Stories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.SendMessages)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.ReceiveMessages)
                .WithOne(m => m.Receiver)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);

            // Define Relation
            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.SentRequests)
                .WithOne(fr => fr.Sender)
                .HasForeignKey(fr => fr.SenderId)
                .OnDelete(DeleteBehavior.NoAction);

            // 
            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.RecievedRequests)
                .WithOne(fr => fr.Receiver)
                .HasForeignKey(fr => fr.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);


            // Friends relations 
            modelBuilder.Entity<Friendship>()
                .HasIndex(f => new { f.UserOneId, f.UserTwoId })
                .IsUnique();
            // For Friendship
            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.UserOne)
                .WithMany(f => f.FriendshipsInit)
                .HasForeignKey(f => f.UserOneId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.UserTwo)
                .WithMany(f => f.FriendshipsReceived)
                .HasForeignKey(f => f.UserTwoId)
                .OnDelete(DeleteBehavior.NoAction);


            // For Post Table
            modelBuilder.Entity<Post>()
                .HasOne(p => p.AppUser)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);
           
            //For Post Image
            modelBuilder.Entity<PostImage>()
                .HasOne(pi => pi.Post)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // For Like Table
            modelBuilder.Entity<Like>()
                .HasKey(l => new { l.PostId, l.AppUserId });

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(p => p.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.AppUser)
                .WithMany(u => u.Likes)
                .HasForeignKey(u => u.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // For Comments table
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.AppUser)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Post Favorite Table
            modelBuilder.Entity<Favorite>()
                .HasKey(f => new { f.PostId, f.AppUserId });

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Post)
                .WithMany(p => p.Favorites)
                .HasForeignKey(f => f.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.AppUser)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Report Table
            modelBuilder.Entity<Report>()
                .HasKey(r => new { r.PostId, r.AppUserId });

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Post)
                .WithMany(p => p.Reports)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.AppUser)
                .WithMany(u => u.Reports)
                .HasForeignKey(r => r.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Story table 
            modelBuilder.Entity<Story>()
                .HasOne(s => s.AppUser)
                .WithMany(u => u.Stories)
                .HasForeignKey(s => s.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);



        }

    }
}
