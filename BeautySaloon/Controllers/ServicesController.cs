namespace BeautySaloon.Controllers
{
    using Business;
    using Models;
    using System;
    using System.Linq;
    using System.Web.Http;

    [Authorize]
    public class ServicesController : ApiController
    {
        public IHttpActionResult Get()
        {
            try
            {
                using (var coreadmin = new CoreAdmin())
                {
                    var services = coreadmin.GetServices().ToList();
                    var mainServices = services.Where(s => s.ParentId == null).Select(s => s.Id).Distinct().OrderBy(u => u).ToList();
                    return Ok(mainServices.GroupJoin(services, y => y, sp => sp.ParentId,
                        (y, sp) => new TreeServices()
                        {
                            Id = y,
                            text = services.FirstOrDefault(u => u.Id == y).Name,
                            nodes = sp.Select(osp => new NodeService() { Id = osp.Id, text = osp.Name, Coast = osp.Coast })
                            .OrderBy(osp => osp.Id)
                            .ToList()
                        }).ToList());
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
                    var service = coreadmin.GetService(id);
                    return Ok(new ServiceDto()
                    {
                        Id = service.Id,
                        Name = service.Name,
                        Coast = service.Coast,
                        Description = service.Description
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IHttpActionResult Post([FromBody]ServiceDto value)
        {
            try
            {
                using (var coreadmin = new CoreAdmin())
                {
                    coreadmin.AddOrUpdateService(value.Id == null ?
                    new Domain.Service()
                    {
                        Name = value.Name,
                        Coast = value.Coast,
                        Description = value.Description,
                        ParentId = value.ParentId
                    } :
                    new Domain.Service()
                    {
                        Id = value.Id.Value,
                        Name = value.Name,
                        Coast = value.Coast,
                        Description = value.Description,
                        ParentId = value.ParentId
                    });
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
                    coreadmin.DeleteService(id);
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