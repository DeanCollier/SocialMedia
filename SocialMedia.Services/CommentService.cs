using SocialMedia.Data;
using SocialMedia.Models.CommentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services
{
    public class CommentService
    {
        // userId Guid for all services since a specific user posts/comments/replies/likes
        private readonly Guid _userId;
        public CommentService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateComment(CommentCreate model)
        {
            var entity =
                new Comment()
                {
                    CommentText = model.CommentText,
                    CommentAuthor = _userId,
                    PostId = model.PostId,
                };

            using (var context = new ApplicationDbContext())
            {
                context.Comments.Add(entity);
                return context.SaveChanges() == 1;
            }
        }
    }
}
