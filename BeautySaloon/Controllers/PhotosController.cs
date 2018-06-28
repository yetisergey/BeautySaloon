namespace BeautySaloon.Controllers
{
    using Business;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;

    public class PhotosController : ApiController
    {
        public IHttpActionResult Get()
        {
            try
            {
                using (var coreadmin = new CoreAdmin())
                {
                    return Ok(coreadmin.GetPhotos().ToList().Select(e => new { id = e.Id, FileName = e.Id + e.FileName, Size = getSize(e.Id + e.FileName) }).ToList());
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private static string getSize(string name)
        {
            string root = HttpContext.Current.Server.MapPath("~/Images/");
            var img = Image.FromFile(root + @"\" + name);
            return img.Width + "x" + img.Height;
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                using (var coreadmin = new CoreAdmin())
                {
                    return Ok(coreadmin.GetPhotos(id).Select(e => new { id = e.Id, FileName = e.Id + e.FileName }).ToList());
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        public async Task<IHttpActionResult> Post(int? id)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }
            var provider = new MultipartMemoryStreamProvider();
            string root = HttpContext.Current.Server.MapPath("~/Images/");
            await Request.Content.ReadAsMultipartAsync(provider);

            foreach (var file in provider.Contents)
            {
                var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                byte[] fileArray = await file.ReadAsByteArrayAsync();
                using (var coreadmin = new CoreAdmin())
                {
                    using (FileStream fs = new FileStream(root + coreadmin.AddPhoto(new Domain.Photo() { FileName = filename, IsDeleted = false, ServiceId = id }) + filename, FileMode.Create))
                    {
                        await fs.WriteAsync(fileArray, 0, fileArray.Length);
                    }
                }
            }
            return Ok("файлы загружены");
        }

        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                using (var coreadmin = new CoreAdmin())
                {
                    coreadmin.DeletePhoto(id);
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