using Microsoft.AspNet.Identity;
using SocialMedia.Models.CommentModels;
using SocialMedia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SocialMedia.WebAPI.Controllers
{
    public class CommentController : ApiController
    {
        // reference comment service layer
        public CommentService CreateCommentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var commentService = new CommentService(userId);
            return commentService;
        }

        // POST
        [HttpPost]
        [Route("api/Comment")]
        public IHttpActionResult Post(CommentCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            CommentService commentService = CreateCommentService();

            if (!commentService.CreateComment(model))
                return InternalServerError();

            return Ok();

        }

        // GET ALL
        [HttpGet]
        [Route("api/Comment")]
        public IHttpActionResult Get()
        {
            CommentService commentService = CreateCommentService();
            var comments = commentService.GetComments();
            return Ok(comments);
        }

        // GET BY ID
        [HttpGet]
        [Route("api/Comment/{id}")]
        public IHttpActionResult Get(int id)
        {
            CommentService commentService = CreateCommentService();
            var comments = commentService.GetCommentById(id);
            return Ok(comments);
        }

        // PUT BY ID
        [HttpPut]
        [Route("api/Comment/{id}")]
        public IHttpActionResult Put(int id, CommentEdit updatedComment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != updatedComment.CommentId)
                return BadRequest("Comment Id's do not match");

            CommentService commentService = CreateCommentService();

            if (!commentService.UpdateComment(updatedComment))
                return InternalServerError();

            return Ok();
        }

        // DELETE
        [HttpDelete]
        [Route("api/Comment/{id}")]
        public IHttpActionResult Delete(int id)
        {
            CommentService commentService = CreateCommentService();
            if (!commentService.DeleteCommentById(id))
                return InternalServerError();

            return Ok();
        }

    }
}
