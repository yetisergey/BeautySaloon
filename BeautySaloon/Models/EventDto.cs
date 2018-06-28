﻿namespace BeautySaloon.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public class EventDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}