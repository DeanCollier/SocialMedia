using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Models.CommentModels
{
    public class CommentListItem
    {
        public int CommentId { get; set; }

        public int PostId { get; set; }

        public int NumberOfReplies { get; set; }

    }
}
