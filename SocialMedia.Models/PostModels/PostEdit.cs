using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Models.PostModels
{
    public class PostEdit
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int CommentId { get; set; }
        public int LikeId { get; set; }
    }
}
