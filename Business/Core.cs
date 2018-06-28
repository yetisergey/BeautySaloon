namespace Business
{
    using Data;
    using Domain;
    using System;
    using System.Linq;

    public class Core : IDisposable
    {
        private readonly SaloonContext ctx;

        public Core()
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

        /// <summary>
        /// Получить Услуги (2)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<Service> GetSubServices(int id)
        {
            return ctx.Services.Where(s => s.ParentId == id && !s.IsDeleted);
        }

        /// <summary>
        /// Получить Услуги (1)
        /// </summary>
        /// <returns></returns>
        public IQueryable<Service> GetMainServices()
        {
            return ctx.Services.Where(s => s.ParentId == null && !s.IsDeleted);
        }

        /// <summary>
        /// Добавить Обращение
        /// </summary>
        /// <param name="a"></param>
        public void AddAppeal(Appeal a)
        {
            ctx.Appeals.Add(a);
            ctx.SaveChanges();
        }
    }
}