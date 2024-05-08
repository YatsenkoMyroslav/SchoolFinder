﻿using SchoolFinder.Common.Abstraction;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Common.School.Model.Feedback;

namespace SchoolFinder.Common.School.Model
{
    public class School
    {
        Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
        public string SchoolWebsiteUrl { get; set; } = string.Empty;
        public string SchoolPhoneNumber { get; set; } = string.Empty;
        public User Owner { get; set; } = new User();
        public Geo Location { get; set; } = new Geo();
        public List<Comment>? Comments { get; set; }
        public List<FileBytes>? Photos { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public bool Deleted { get; set; }
        public DateTime DeletedOn { get; set; }
    }
}
