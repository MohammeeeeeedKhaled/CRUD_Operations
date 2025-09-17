using Blog.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) 
        {

        }
        // الاولforeign key  رتبهم حسب اللي معندهمش   
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        ////fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //    base.OnModelCreating(modelBuilder);
            //    modelBuilder.Entity<User>(entity =>
            //    {
            //        //PK
            //        entity.HasKey(user => user.Id);//PK
            //        //username
            //        entity.Property(user => user.UserName)
            //        .IsRequired()
            //        .HasMaxLength(150);

            //        //unique   isunique method appear when use HasIndex
            //        entity.HasIndex(user => user.Email).IsUnique();
            //    }
            //    );
            //    //modelBuilder.Entity<User>().HasIndex(user => user.Email).IsUnique();//2nd way


            ////Relations 1-1 1-M M-M
            ////post comment 1-M
            //modelBuilder.Entity<Post>().HasMany(post => post.Comments)
            //    .WithOne(Comment => Comment.Post).HasForeignKey(commet => commet.PostId);
            ////post category M-M
            //modelBuilder.Entity<Post>()
            //    .HasMany(post => post.Category)
            //    .WithMany(category => category.Posts)
            //    .UsingEntity(entity => entity.ToTable("PostCategories"));

        }

    }
}
