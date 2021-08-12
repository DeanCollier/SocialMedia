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
                    ReplyAuthorId = model.ReplyAuthorId,
                    ReplyText = model.ReplyText,
                    CommentId = model.CommentId
                };

            using (var context = new ApplicationDbContext())
            {
                context.Replies.Add(entity);
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
                        .Where(entity => entity.ReplyAuthorId == _userId)
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
                        .Single(entity => entity.ReplyId == id && entity.ReplyAuthorId == _userId);

                return
                    new ReplyDetail
                    {
                        ReplyId = entity.ReplyId,
                        ReplyText = entity.ReplyText
                    };
            }
        }

        public bool UpdateReply(ReplyEdit model)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity =
                    context
                        .Replies
                        .Single(entity => entity.ReplyId == model.ReplyId && entity.ReplyAuthorId == _userId);

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
                        .Single(entity => entity.ReplyId == replyId && entity.ReplyAuthorId == _userId);

                context.Replies.Remove(entity);

                return context.SaveChanges() == 1;
            }
        }

    }
}
