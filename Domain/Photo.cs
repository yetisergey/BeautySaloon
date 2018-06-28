namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("Service")]
        public int? ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}