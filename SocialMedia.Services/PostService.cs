using SocialMedia.Data;
using SocialMedia.Models.PostModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services
{
    public class PostService
    {
        private readonly Guid _userId;

        public PostService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePost(PostCreate model)
        {
            var entity =
                new Post()
                {
                    PostId = model.PostId,

                    AuthorId = _userId,
                    Title = model.Title,
                    Text = model.Text,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Posts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PostListItem> GetPosts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Posts
                        .Where(e => e.AuthorId == _userId)
                        .Select(
                            e =>
                                new PostListItem
                                {
                                    PostId = e.PostId,
                                    Title = e.Title,
                                    CreatedUtc = e.CreatedUtc
                                }

                        );
                return query.ToArray();
            }
        }

        public PostDetail GetPostById(int id)
        {
            var commentService = new CommentService(_userId);
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Posts
                        .Single(e => e.PostId == id && e.AuthorId == _userId);

                var comments = commentService.GetCommentsByPostId(entity.PostId);

                return
                    new PostDetail
                    {
                        PostId = entity.PostId,
                        AuthorId = entity.AuthorId,
                        Title = entity.Title,
                        Text = entity.Text,
                        Comments = comments,
                        Likes = entity.Likes,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc

                    };
            }
        }


        public PostListItem GetPostByCommentId(int commentId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var com =
                    ctx
                        .Comments
                        .Single(c => c.CommentId == commentId && c.CommentAuthor == _userId);

                var entity =
                    ctx
                        .Posts
                        .Single(e => e.AuthorId == _userId && e.PostId == com.PostId);

                return
                    new PostListItem
                    {
                        PostId = entity.PostId,
                        Title = entity.Title,
                        CreatedUtc = entity.CreatedUtc
                    };
            }
        }

        public PostListItem GetPostByLikeId(int likeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var like =
                    ctx
                        .Likes
                        .Single(l => l.LikeId == likeId && l.LikeAuthor == _userId);

                var entity =
                    ctx
                        .Posts
                        .Single(e => e.AuthorId == _userId && e.PostId == like.PostId);

                return
                    new PostListItem
                    {
                        PostId = entity.PostId,
                        Title = entity.Title,
                        CreatedUtc = entity.CreatedUtc
                    };
            }
        }

        public bool UpdatePost(PostEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Posts
                        .Single(e => e.PostId == model.PostId && e.AuthorId == _userId);

                entity.Title = model.Title;
                entity.Text = model.Text;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePost(int postId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Posts
                        .Single(e => e.PostId == postId && e.AuthorId == _userId);

                ctx.Posts.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
