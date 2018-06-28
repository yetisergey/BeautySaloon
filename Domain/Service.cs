namespace Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double? Coast { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Service Parent { get; set; }
        [ForeignKey("ParentId")]
        public virtual ICollection<Service> Childs { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}