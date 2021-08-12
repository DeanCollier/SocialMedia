using SocialMedia.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Models.LikeModels
{
    public class LikeDetail
    {
        [Required]
        public int LikeId { get; set; }

        public virtual Post Post { get; set; }
    }
}
