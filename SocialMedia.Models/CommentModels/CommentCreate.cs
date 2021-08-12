using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Models.CommentModels
{
    public class CommentCreate
    {
        [Required]
        [MinLength(1, ErrorMessage = "Comment cannot be empty.")]
        [MaxLength(200, ErrorMessage = "Comment is too long.")]
        public string CommentText { get; set; }
        
        [Required]
        public int PostId { get; set; }
    }
}
