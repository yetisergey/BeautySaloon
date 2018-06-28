namespace BeautySaloon.Models
{
    public class ServiceDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Coast { get; set; }
        public int? ParentId { get; set; }
    }
}