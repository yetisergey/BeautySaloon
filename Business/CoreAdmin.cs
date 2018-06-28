namespace Business
{
    using Data;
    using System;
    using System.Linq;
    using Domain;
    using System.Data.Entity.Migrations;

    public class CoreAdmin : IDisposable
    {
        private readonly SaloonContext ctx;

        public CoreAdmin()
        {
            try
            {
                ctx = new SaloonContext();
            }
            catch
            {
                throw new Exception("Внутренняя ошибка сервера");
            }
        }

        public void Dispose()
        {
            try
            {
                ctx?.Dispose();
            }
            catch
            {
                throw new Exception("Внутренняя ошибка сервера");
            }
        }

        #region Service
        /// <summary>
        /// Получить услуги
        /// </summary>
        /// <returns></returns>
        public IQueryable<Service> GetServices()
        {
            return ctx.Services.Where(doc => !doc.IsDeleted);
        }

        /// <summary>
        /// Получить услугу по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Service GetService(int id)
        {
            return ctx.Services.FirstOrDefault(u => u.Id == id);
        }

        /// <summary>
        /// Добавить или Обновить Услуги
        /// </summary>
        /// <param name="s"></param>
        public void AddOrUpdateService(Service s)
        {
            ctx.Services.AddOrUpdate(s);
            ctx.SaveChanges();
        }

        /// <summary>
        /// Удалить услуги
        /// </summary>
        /// <param name="id"></param>
        public void DeleteService(int id)
        {
            var p = ctx.Services.FirstOrDefault(e => e.Id == id);
            if (p != null)
            {
                p.IsDeleted = true;
                ctx.SaveChanges();
            }
        }
        #endregion

        #region Event
        /// <summary>
        /// Получить События
        /// </summary>
        /// <param name="content"></param>
        public IQueryable<Event> GetEvents()
        {
            return ctx.Events.Where(e => !e.IsDeleted);
        }

        /// <summary>
        /// Удалить событие
        /// </summary>
        /// <param name="id"></param>
        public void DeleteEvent(int id)
        {
            var ev = ctx.Events.FirstOrDefault(e => e.Id == id);
            if (ev != null)
            {
                ev.IsDeleted = true;
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Получить событие по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Event GetEvent(int id)
        {
            return ctx.Events.FirstOrDefault(e => e.Id == id && !e.IsDeleted);
        }

        /// <summary>
        /// Добавить/Отредактировать Событие
        /// </summary>
        /// <param name="content"></param>
        public void AddOrUpdateEvent(Event e)
        {
            ctx.Events.AddOrUpdate(e);
            ctx.SaveChanges();
        }
        #endregion

        #region Photo
        /// <summary>
        /// Получить фотографию по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<Photo> GetPhotos(int id)
        {
            return ctx.Photos.Where(e => !e.IsDeleted && e.ServiceId == id);
        }

        /// <summary>
        /// Получить фотографии
        /// </summary>
        /// <param name="content"></param>
        public IQueryable<Photo> GetPhotos()
        {
            return ctx.Photos.Where(e => !e.IsDeleted);
        }

        /// <summary>
        /// Добавить фото
        /// </summary>
        /// <param name="content"></param>
        public int AddPhoto(Photo p)
        {
            var photo = ctx.Photos.Add(p);
            ctx.SaveChanges();
            return photo.Id;
        }

        /// <summary>
        /// удалить фотографию
        /// </summary>
        /// <param name="id"></param>
        public void DeletePhoto(int id)
        {
            var p = ctx.Photos.FirstOrDefault(e => e.Id == id);
            if (p != null)
            {
                p.IsDeleted = true;
                ctx.SaveChanges();
            }
        }
        #endregion
    }
}