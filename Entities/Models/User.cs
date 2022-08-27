using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }

    public enum UserRole { Admin, ApiUser }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Used to pre-seed data or build relationships.
            builder.HasData(
                new User { Id = 1, Email = "apiuser@gmail.com", Password = "apiuser", Role = UserRole.ApiUser },
                new User { Id = 2, Email = "admin@gmail.com", Password = "admin", Role = UserRole.Admin }
            );
        }
    }
}
