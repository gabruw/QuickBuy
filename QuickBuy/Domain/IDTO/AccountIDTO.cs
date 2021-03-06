﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.IDTO
{
    public class AccountIDTO : IdentityUser
    {
        [Required]
        [MaxLength(13)]
        public string Permission { get; set; }
    }
}