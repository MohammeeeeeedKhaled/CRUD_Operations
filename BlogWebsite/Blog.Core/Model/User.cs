using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Model
{
    public class User
    {
        //public Guid Id { get; set; }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="UserName Required")]
        [StringLength(150 , MinimumLength =5)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } //Hashed pass
        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }
        //Relations
        public ICollection<Post> Posts { get; set; }=new List<Post>();//Reference
        public ICollection<Comment> Comments { get; set; }=new List<Comment>();//Reference


    }
}
