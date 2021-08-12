using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Models.ReplyModels
{
    public class ReplyCreate
    {
        public string ReplyText { get; set; }
        public Guid ReplyAuthorId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Replies need to be at least one character.")]
        [MaxLength(200, ErrorMessage = "Replies cannot exceed 200 characters.")]

        public int CommentId { get; set; }
    }
}
