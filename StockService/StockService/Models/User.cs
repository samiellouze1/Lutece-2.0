﻿using Microsoft.AspNetCore.Identity;
using StockService.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockService.Models
{
    public class User:IEntityBase
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public UserTypeEnum UserType { get; set; }

        [Required]
        public double Balance { get; set; }
        public virtual List<StockUnit> StockUnits { get; set; } = new List<StockUnit>();
        public virtual List<OriginalOrder> OriginalOrders { get; set; } = new List<OriginalOrder>();
    }
    public class Jwt
    {
        public string key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set;}
    }
}
