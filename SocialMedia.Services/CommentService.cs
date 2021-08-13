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
                    PostId = model.PostId
                };

            using (var context = new ApplicationDbContext())
            {
                var post =
                    context
                        .Posts
                        .Single(p => p.PostId == entity.PostId && p.AuthorId == _userId);


                context.Comments.Add(entity);
                post.Comments.Add(entity);

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

        // get by post id
        public IEnumerable<CommentNoPost> GetCommentsByPostId(int postId)
        {
            using (var context = new ApplicationDbContext())
            {
                var query =
                    context
                        .Comments
                        .Where(entity => entity.CommentAuthor == _userId && entity.PostId == postId)
                        .Select(
                            entity =>
                                new CommentNoPost
                                {
                                    CommentId = entity.CommentId,
                                    CommentText = entity.CommentText,
                                }
                        );
                return query.ToArray();
            }
        }

        // GET
        public CommentDetail GetCommentById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity =
                    context
                        .Comments
                        .Single(e => e.CommentAuthor == _userId && e.CommentId == id);

                return
                    new CommentDetail
                    {
                        CommentId = entity.CommentId,
                        CommentText = entity.CommentText,
                        Replies = entity.Replies,
                        Post = entity.Post
                    };
            }
        }

        // PUT
        public bool UpdateComment(CommentEdit model)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity =
                    context
                        .Comments
                        .Single(e => e.CommentId == model.CommentId && e.CommentAuthor == _userId);

                entity.CommentText = model.CommentText;

                return context.SaveChanges() == 1;
            }
        }

        // DELETE
        public bool DeleteCommentById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity =
                    context
                        .Comments
                        .Single(e => e.CommentAuthor == _userId && e.CommentId == id);

                context.Comments.Remove(entity);
                return context.SaveChanges() == 1;
            }
        }
    }
}
