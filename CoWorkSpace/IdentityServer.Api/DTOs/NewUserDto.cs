﻿using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Api.DTOs
{
    public class NewUserDto
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "UserNeme is required")]
        public string UserNeme { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email" +
            " is required")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}