using System;
using System.Collections.Generic;
using System.Text;

namespace Jdsl.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsPublished { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
}
