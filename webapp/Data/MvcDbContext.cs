using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using webapp.Models;

namespace webapp.Data
{
    public class MvcDbContext : IdentityDbContext<
        AppUser, AppRole, string,
        AppUserClaim, AppUserRole, AppUserLogin,
        AppRoleClaim, AppUserToken>
    {
        public MvcDbContext(DbContextOptions<MvcDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<AppRole> AppRole { get; set; }
        public DbSet<AppUserRole> AppUserRole { get; set; }

        public DbSet<ItemUser> ItemUser { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<ContentItem> ContentItem { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<TemplateItem> TemplateItem { get; set; }
        public DbSet<Template> Template { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // FIX THIS: entity filter for OwnerId ???
            // @see https://docs.microsoft.com/en-us/ef/core/querying/filters

            modelBuilder.Entity<AppUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<AppRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            modelBuilder.Entity<Item>().ToTable("Item");  // +ContentItem +TemplateItem
            modelBuilder.Entity<Content>().ToTable("Content");
            modelBuilder.Entity<Template>().ToTable("Template");

            // Item
            modelBuilder.Entity<Item>()
                .HasDiscriminator<string>("ItemType")
                .HasValue<Item>("Item")
                .HasValue<ContentItem>("Content")
                .HasValue<TemplateItem>("Template");
            modelBuilder.Entity<Item>()
                .HasIndex(a => a.Keyname)
                .HasName("Index_Keyname")
                .IsUnique()
                .HasFilter("[Keyname] IS NOT NULL");
            modelBuilder.Entity<Item>()
                .HasIndex(a => a.ItemType)
                .HasName("Index_ItemType");
            modelBuilder.Entity<Item>()
                .Property(a => a.Keyname)
                .HasColumnType("varchar(63)");
            modelBuilder.Entity<Item>()
                .Property(a => a.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Item>()
                .Property(a => a.ModificationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
                //.IsRowVersion();

            // Item to AppUser, Many-to-many
            modelBuilder.Entity<ItemUser>()
                .HasKey(a => new { a.UserId, a.ItemId });
            modelBuilder.Entity<ItemUser>()
                .HasOne(a => a.User)
                .WithMany(b => b.ItemUsers)
                .HasForeignKey(a => a.UserId);
            modelBuilder.Entity<ItemUser>()
                .HasOne(a => a.Item)
                .WithMany(b => b.ItemUsers)
                .HasForeignKey(a => a.ItemId);

            // ContentItem
            modelBuilder.Entity<ContentItem>()
                .HasOne(a => a.Content)
                .WithOne(b => b.Item)
                .HasForeignKey<Content>(b => b.Id);

            // Content
            //modelBuilder.Entity<Content>()
                //.HasOne(a => a.Item)
                //.WithOne(b => b.Content);
            modelBuilder.Entity<Content>()
                .HasIndex(a => a.Title)
                .HasName("Content_Title");

            // TemplateItem
            modelBuilder.Entity<TemplateItem>()
                .HasOne(a => a.Template)
                .WithOne(b => b.Item)
                .HasForeignKey<Template>(b => b.Id);

            // Template
            //modelBuilder.Entity<Template>()
                //.HasOne(a => a.Item)
                //.WithOne(b => b.Template);
            modelBuilder.Entity<Template>()
                .HasIndex(a => a.Title)
                .HasName("Template_Title");
        }

   }
}
