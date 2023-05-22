using products.Domain.Entities;

namespace products.Application.Auth.Models;

    public class TokenEntity
    {
        public string? Token { get; set; }
        public DateTime? Expiration { get; set; }
        public List<string> Roles { get; set; }

    }
