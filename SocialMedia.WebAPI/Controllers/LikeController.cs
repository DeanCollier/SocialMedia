using Microsoft.AspNet.Identity;
using SocialMedia.Models.LikeModels;
using SocialMedia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SocialMedia.WebAPI.Controllers
{
    public class LikeController : ApiController
    {
        public LikeService CreateLikeService() 
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var likeService = new LikeService(userId);
            return likeService;

        }


        [HttpGet]
        [Route("api/Like/{postId}")]
        public IHttpActionResult Get(int postId)
        {
            LikeService likeService = CreateLikeService();
            var likes = likeService.GetLikesByPostId(postId);
            return Ok(likes);
        }

        [HttpGet]
        [Route("api/Like")]
        public IHttpActionResult Get()
        {
            LikeService likeService = CreateLikeService();
            var likes = likeService.GetLikeByAuthor();
            return Ok(likes);
        }

        [HttpPost]
        [Route("api/Like")]
        public IHttpActionResult Post(LikeCreate like)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateLikeService();

            if (!service.CreateLike(like))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Put(int id, LikeEdit model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != model.LikeId)
                return BadRequest("Like Id's do not match");

            var service = CreateLikeService();

            if (!service.UpdateLike(model))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            LikeService service = CreateLikeService();
            if (!service.DeleteLike(id))
                return InternalServerError();

            return Ok();
        }

    }
}
