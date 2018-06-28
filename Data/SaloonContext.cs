namespace Data
{
    using Domain;
    using System.Data.Entity;

    public class SaloonContext : DbContext
    {
        public SaloonContext() : base("Saloon") { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appeal> Appeals { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Service>()
               .HasOptional(q => q.Parent)
               .WithMany(ds => ds.Childs)
               .HasForeignKey(q => q.ParentId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photo>()
               .HasOptional(q => q.Service)
               .WithMany(ds => ds.Photos)
               .HasForeignKey(q => q.ServiceId)
               .WillCascadeOnDelete(false);
        }
    }
}