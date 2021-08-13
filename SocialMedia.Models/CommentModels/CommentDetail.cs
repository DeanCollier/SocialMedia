using SocialMedia.Data;
using SocialMedia.Models.PostModels;
using SocialMedia.Models.ReplyModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Models.CommentModels
{
    public class CommentDetail
    {   
        public int CommentId { get; set; }

        public string CommentText { get; set; }

        public virtual IEnumerable<ReplyDetail> Replies { get; set; }

        public PostListItem Post { get; set; }

    }
}
