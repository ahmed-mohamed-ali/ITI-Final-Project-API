﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiveLifeAPI.Models
{
    public partial class User
    {
        public User()
        {
            UserGroup = new HashSet<UserGroup>();
        }

        [Column("UserID")]
        public int UserId { get; set; }
        [Column("ConnectionID")]
        public int ConnectionId { get; set; }
        public bool Deleted { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserGroup> UserGroup { get; set; }
    }
}