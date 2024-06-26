﻿namespace SchoolFinder.Core.Configurations
{
    public class JwtConfiguration
    {
        public string ValidAudience { get; set; } = string.Empty;
        public string ValidIssuer { get; set; } = string.Empty;
        public string Secret {  get; set; } = string.Empty;
    }
}
