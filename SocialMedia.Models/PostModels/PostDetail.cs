using SocialMedia.Data;
using SocialMedia.Models.CommentModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Models.PostModels
{
    public class PostDetail
    {
        public int PostId { get; set; }

        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public virtual IEnumerable<CommentNoPost> Comments { get; set; }
        public virtual List<Like> Likes { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
