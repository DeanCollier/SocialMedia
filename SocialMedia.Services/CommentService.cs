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

        // POST
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

        // GET ALL
        public IEnumerable<CommentListItem> GetComments()
        {
            using (var context = new ApplicationDbContext())
            {
                var query =
                    context
                        .Comments
                        .Where(entity => entity.CommentAuthor == _userId)
                        .Select(
                            entity =>
                                new CommentListItem
                                {
                                    CommentId = entity.CommentId,
                                    PostId = entity.PostId,
                                    NumberOfReplies = entity.Replies.Count()
                                }
                        );
                return query.ToArray();
            }
        }
    }
}
