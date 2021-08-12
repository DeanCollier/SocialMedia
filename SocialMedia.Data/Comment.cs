using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Data
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string CommentText { get; set; }

        [Required]
        public Guid CommentAuthor { get; set; }

        public virtual List<Reply> Replies { get; set; } = new List<Reply>();

        [Required, ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        
        public virtual Post Post { get; set; }
     
    }
}
