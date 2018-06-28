namespace BeautySaloon.Controllers
{
    using Business;
    using Models;
    using System;
    using System.Linq;
    using System.Web.Http;

    [Authorize]
    public class SheduleController : ApiController
    {
        public IHttpActionResult Get()
        {
            try
            {
                using (var coreadmin = new CoreAdmin())
                {
                    return Ok(coreadmin.GetEvents().Select(e => new { id = e.Id, title = e.Name, start = e.Start, end = e.End }).ToList());
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                using (var coreadmin = new CoreAdmin())
                {
                    return Ok(coreadmin.GetEvent(id));
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IHttpActionResult Post([FromBody]EventDto value)
        {
            try
            {
                using (var coreadmin = new CoreAdmin())
                {
                    coreadmin.AddOrUpdateEvent(value.Id == null ?
                        new Domain.Event() { Name = value.Name, Start = value.Start, End = value.End } :
                        new Domain.Event() { Id = value.Id.Value, Name = value.Name, Start = value.Start, End = value.End });
                    return Ok("Успешное добавление или редактирование!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                using (var coreadmin = new CoreAdmin())
                {
                    coreadmin.DeleteEvent(id);
                    return Ok("Успешное удаление!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}