namespace BeautySaloon.Controllers
{
    using Business;
    using Models;
    using System;
    using System.Web.Http;
    using Utils;

    public class TelegaController : ApiController
    {
        public IHttpActionResult Post([FromBody]TelegaDto dto)
        {
            try
            {
                using (var core = new Core())
                {
                    EmailSend.SendMail(dto.Name + "/" + dto.Phone + "/" + DateTime.Now);
                    core.AddAppeal(new Domain.Appeal() { Name = dto.Name, Phone = dto.Phone, Date = DateTime.Now });
                    return Ok("Запись успешно добавлена в обработку!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}