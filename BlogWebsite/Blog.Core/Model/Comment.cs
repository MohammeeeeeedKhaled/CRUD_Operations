using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Model
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Content Required")]//min length 1  (not null)
        [StringLength(150)] // 1 to 150 char
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }=DateTime.Now;
        //Relations
        //UserId
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        //PostId
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
