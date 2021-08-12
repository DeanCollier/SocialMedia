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
        [Required]
        public int CommentId { get; set; }

        public string CommentText { get; set; }
    ////a;sdfja;sldkgha[w

    }
}
