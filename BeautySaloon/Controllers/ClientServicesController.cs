namespace BeautySaloon.Controllers
{
    using Business;
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    public class ClientServicesController : ApiController
    {
        public IHttpActionResult Get()
        {
            try
            {
                using (var core = new Core())
                {
                    return Ok(core.GetMainServices().ToList().Select(u => new
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Description = u.Description,
                        Photo = pictureLink(u.Photos)
                    }).ToList());
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IHttpActionResult GetSubServices(int id)
        {
            try
            {
                using (var core = new Core())
                {
                    return Ok(core.GetSubServices(id).ToList().Select(u => new
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Coast = u.Coast,
                        Photo = pictureLink(u.Photos)
                    }).ToList());
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private static string pictureLink(ICollection<Photo> photos)
        {
            var list = photos.Where(p => !p.IsDeleted).ToList();
            return "/Images/" + (list.Count() >= 1 ? list.First().Id + list.First().FileName : "default.png");
        }
    }
}