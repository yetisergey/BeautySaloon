﻿namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    using System;
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsDeleted { get; set; }
    }
}