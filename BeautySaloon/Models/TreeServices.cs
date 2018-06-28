using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautySaloon.Models
{
    public class TreeServices
    {
        public int Id { get; set; }
        public string text { get; set; }
        public List<NodeService> nodes { get; set; }
    }
}