using SocialMedia.Data;
using SocialMedia.Models.LikeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services
{
    public class LikeService
    {
        private readonly Guid _userId;

        public LikeService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateLike(LikeCreate model)
        {
            var entity =
                new Like()
                {
                    LikeAuthor = _userId,
                    PostId = model.PostId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Likes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<LikeListItem> GetLikesByPostId(int postId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Likes
                        .Where(e => e.LikeAuthor == _userId && e.PostId == postId)
                        .Select(
                            e =>
                                new LikeListItem
                                {
                                    PostId = e.PostId,
                                    LikeId = e.LikeId
                                }

                        );
                return query.ToArray();
            }
        }

        public IEnumerable<LikeDetail> GetLikeByAuthor()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Likes
                        .Where(e => e.LikeAuthor == _userId)
                        .Select(
                            e =>
                                new LikeDetail
                                {
                                    Post = e.Post,
                                    LikeId = e.LikeId
                                }

                        );
                return query.ToArray();
            }
        }

        public bool UpdateLike(LikeEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Likes
                        .Single(e => e.LikeId == model.LikeId && e.LikeAuthor == _userId);

                entity.PostId = model.PostId;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteLike(int likeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Likes
                        .Single(e => e.LikeId == likeId && e.LikeAuthor == _userId);

                ctx.Likes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
