using SocialMedia.Data;
using SocialMedia.Models.ReplyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services
{
    public class ReplyService
    {
        private readonly Guid _userId;

        public ReplyService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateReply(ReplyCreate model)
        {
            var entity =
                new Reply()
                {
                    ReplyAuthorId = _userId,
                    ReplyText = model.ReplyText,
                    CommentId = model.CommentId
                };

            using (var context = new ApplicationDbContext())
            {
                var com =
                    context
                        .Comments
                        .Single(c => c.CommentId == entity.CommentId && c.CommentAuthor == _userId);

                context.Replies.Add(entity);
                com.Replies.Add(entity);

                return context.SaveChanges() == 1;
            }
        }

        public IEnumerable<ReplyListItem> GetReplies()
        {
            using (var context = new ApplicationDbContext())
            {
                var query =
                    context
                        .Replies
                        .Where(e => e.ReplyAuthorId == _userId)
                        .Select(
                            entity =>
                                new ReplyListItem
                                {
                                    ReplyId = entity.ReplyId,
                                    CommentId = entity.CommentId
                                }
                            );
                return query.ToArray();
            }
        }

        public ReplyDetail GetReplyByID(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity =
                    context
                        .Replies
                        .Single(e => e.ReplyId == id && e.ReplyAuthorId == _userId);

                return
                    new ReplyDetail
                    {
                        ReplyId = entity.ReplyId,
                        ReplyText = entity.ReplyText
                    };
            }
        }

        // get replies by comment id
        public IEnumerable<ReplyDetail> GetRepliesByCommentId(int commentId)
        {
            using (var context = new ApplicationDbContext())
            {
                var query =
                    context
                        .Replies
                        .Where(e => e.ReplyAuthorId == _userId && e.CommentId == commentId)
                        .Select(
                            entity =>
                                new ReplyDetail
                                {
                                    ReplyId = entity.ReplyId,
                                    ReplyText = entity.ReplyText
                                }
                            );
                return query.ToArray();
            }
        }

        public bool UpdateReply(ReplyEdit model)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity =
                    context
                        .Replies
                        .Single(e => e.ReplyId == model.ReplyId && e.ReplyAuthorId == _userId);

                entity.ReplyText = model.ReplyText;

                return context.SaveChanges() == 1;
            }
        }

        public bool DeleteReply(int replyId)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity =
                    context
                        .Replies
                        .Single(e => e.ReplyId == replyId && e.ReplyAuthorId == _userId);

                context.Replies.Remove(entity);

                return context.SaveChanges() == 1;
            }
        }

    }
}
