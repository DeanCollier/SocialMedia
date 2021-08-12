using SocialMedia.Data;
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

        public virtual List<Reply> Replies { get; set; }

        public virtual Post Post { get; set; }

    }
}
