﻿namespace InnoGotchi.DataAccess.Models.IdentityModels
{
    public class IdentityUserModel
    {
        public int Id { get; set; }
        public string? Username { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public string? Name { get; set; } = string.Empty;
        public string? Surname { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;
        public string? Role { get; set; } = "0";
    }
}
