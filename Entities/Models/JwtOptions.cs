﻿namespace MovieRestApiWithEF.Core.Models
{
    public sealed class JwtOptions
    {
        public const string SectionName = "jwt";
        public string Secret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int AccessTokenExpiration { get; set; }
    }
}
