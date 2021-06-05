﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiveLifeAPI.Models
{
    public partial class Post
    {
        [Column("PostID")]
        public int PostId { get; set; }
        [Column("CoordID")]
        public int CoordId { get; set; }
        [Column("RegionID")]
        public int RegionId { get; set; }
        [StringLength(500)]
        public string PostTxt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedTime { get; set; }
        [Required]
        [StringLength(14)]
        public string CaseId { get; set; }
        [Column(TypeName = "money")]
        public decimal? RequiredAmount { get; set; }
        [Column(TypeName = "money")]
        public decimal? RestAmount { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [StringLength(50)]
        public string NeedCatogry { get; set; }
        public bool? Deleted { get; set; }
        [Column("OrgID")]
        public int OrgId { get; set; }

        [ForeignKey("CaseId")]
        [InverseProperty("Post")]
        public virtual Cases Case { get; set; }
        [ForeignKey("CoordId")]
        [InverseProperty("Post")]
        public virtual RegionCoordinator Coord { get; set; }
        [ForeignKey("OrgId")]
        [InverseProperty("Post")]
        public virtual Organization Org { get; set; }
        [ForeignKey("RegionId")]
        [InverseProperty("Post")]
        public virtual Region Region { get; set; }
    }
}