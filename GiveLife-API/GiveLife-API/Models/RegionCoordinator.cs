﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiveLifeAPI.Models
{
    public partial class RegionCoordinator
    {
        public RegionCoordinator()
        {
            Cupon = new HashSet<Cupon>();
            Post = new HashSet<Post>();
        }

        [Key]
        [Column("CoordID")]
        public int CoordId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("NationalID")]
        [StringLength(14)]
        public string NationalId { get; set; }
         [Column("RegionID")]
        public int RegionId { get; set; }
        
        public string VisaNum { get; set; }
        [Column(TypeName = "money")]
        public decimal? WalletBalance { get; set; }
        [Column("RegionAdminID")]
        public int? RegionAdminId { get; set; }
        [Required]
       
        public string Password { get; set; }
        public bool Deleted { get; set; }

        [ForeignKey("RegionId")]
        [InverseProperty("RegionCoordinator")]
        public virtual Region Region { get; set; }
        [ForeignKey("RegionAdminId")]
        [InverseProperty("RegionCoordinator")]
        public virtual RegionAdmin RegionAdmin { get; set; }
        [InverseProperty("Coord")]
        public virtual ICollection<Cupon> Cupon { get; set; }
        [InverseProperty("Coord")]
        public virtual ICollection<Post> Post { get; set; }
    }
}