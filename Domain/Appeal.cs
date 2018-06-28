namespace Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Appeal
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
    }
}