﻿// <auto-generated />
using System;
using GiveLifeAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GiveLife_API.Migrations
{
    [DbContext(typeof(GiveLifeContext))]
    [Migration("20210616104302_update cupon2")]
    partial class updatecupon2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GiveLifeAPI.Models.Cases", b =>
                {
                    b.Property<string>("NationalId")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)")
                        .HasColumnName("NationalID");

                    b.Property<int?>("ChildNum")
                        .HasColumnType("int")
                        .HasColumnName("Child_Num");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int?>("FamilyMemberNum")
                        .HasColumnType("int")
                        .HasColumnName("Family_Member_Num");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RegionId")
                        .HasColumnType("int")
                        .HasColumnName("RegionID");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasDefaultValueSql("(N'pending')");

                    b.HasKey("NationalId");

                    b.HasIndex("RegionId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.Cupon", b =>
                {
                    b.Property<int>("CuponId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CuponID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AmountOfMoney")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CaseNationalId")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)")
                        .HasColumnName("CaseNationalID");

                    b.Property<int>("CoordId")
                        .HasColumnType("int")
                        .HasColumnName("CoordID");

                    b.Property<string>("CuponIdentity")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductCategory")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int>("RegionId")
                        .HasColumnType("int")
                        .HasColumnName("RegionID");

                    b.HasKey("CuponId");

                    b.HasIndex("CaseNationalId");

                    b.HasIndex("CoordId");

                    b.HasIndex("RegionId");

                    b.ToTable("Cupon");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("GroupID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("GroupId");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.OnlineDonner", b =>
                {
                    b.Property<int>("DonnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DonnerID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("VisaNum")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<decimal?>("WalletBalance")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("DonnerId");

                    b.ToTable("OnlineDonner");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.Organization", b =>
                {
                    b.Property<int>("OrganizationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrganizationID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActiveType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("OrgName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RegionId")
                        .HasColumnType("int")
                        .HasColumnName("RegionID");

                    b.Property<string>("VisaNum")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<decimal?>("WalletBalance")
                        .HasColumnType("money");

                    b.HasKey("OrganizationId");

                    b.HasIndex("RegionId");

                    b.ToTable("Organization");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PostID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CaseId")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<int?>("CoordId")
                        .HasColumnType("int")
                        .HasColumnName("CoordID");

                    b.Property<DateTime?>("CreatedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("NeedCatogry")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int?>("OrgId")
                        .HasColumnType("int")
                        .HasColumnName("OrgID");

                    b.Property<string>("PostTxt")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("RegionId")
                        .HasColumnType("int")
                        .HasColumnName("RegionID");

                    b.Property<decimal?>("RequiredAmount")
                        .HasColumnType("money");

                    b.Property<decimal?>("RestAmount")
                        .HasColumnType("money");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("(N'pending')");

                    b.HasKey("PostId");

                    b.HasIndex("CaseId");

                    b.HasIndex("CoordId");

                    b.HasIndex("OrgId");

                    b.HasIndex("RegionId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.Region", b =>
                {
                    b.Property<int>("RegionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RegionId");

                    b.ToTable("Region");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.RegionAdmin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AdminID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("BankAccountBalance")
                        .HasColumnType("money");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<int?>("RegionId")
                        .HasColumnType("int")
                        .HasColumnName("RegionID");

                    b.HasKey("AdminId");

                    b.HasIndex("RegionId");

                    b.ToTable("RegionAdmin");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.RegionCoordinator", b =>
                {
                    b.Property<int>("CoordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CoordID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<int>("RegionAdminId")
                        .HasColumnType("int")
                        .HasColumnName("RegionAdminID");

                    b.Property<int>("RegionId")
                        .HasColumnType("int")
                        .HasColumnName("RegionID");

                    b.Property<string>("VisaNum")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<decimal?>("WalletBalance")
                        .HasColumnType("money");

                    b.HasKey("CoordId");

                    b.HasIndex("RegionAdminId");

                    b.HasIndex("RegionId");

                    b.ToTable("RegionCoordinator");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConnectionId")
                        .HasColumnType("int")
                        .HasColumnName("ConnectionID");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.UserGroup", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.Property<int>("GroupId")
                        .HasColumnType("int")
                        .HasColumnName("GroupID");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("User_Group");
                });

            modelBuilder.Entity("GiveLife_API.Models.Donnation", b =>
                {
                    b.Property<int>("DonnationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("DonnationAmout")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("OnlineDonnerId")
                        .HasColumnType("int");

                    b.Property<int?>("RegionCoordinatorId")
                        .HasColumnType("int");

                    b.Property<int>("RegionId")
                        .HasColumnType("int");

                    b.HasKey("DonnationID");

                    b.HasIndex("OnlineDonnerId");

                    b.HasIndex("RegionCoordinatorId");

                    b.HasIndex("RegionId");

                    b.ToTable("Donnations");
                });

            modelBuilder.Entity("GiveLife_API.Models.MoneyTransformation", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("MoneyAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("OrganizationId")
                        .HasColumnType("int");

                    b.Property<int>("RegionAdminId")
                        .HasColumnType("int");

                    b.Property<int?>("RegionCoordinatorId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("RegionAdminId");

                    b.HasIndex("RegionCoordinatorId");

                    b.ToTable("MoneyTransformation");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.Cases", b =>
                {
                    b.HasOne("GiveLifeAPI.Models.Region", "Region")
                        .WithMany("Cases")
                        .HasForeignKey("RegionId")
                        .HasConstraintName("FK_Cases_Region")
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.Cupon", b =>
                {
                    b.HasOne("GiveLifeAPI.Models.Cases", "CaseNational")
                        .WithMany("Cupon")
                        .HasForeignKey("CaseNationalId")
                        .HasConstraintName("FK_Cupon_Cases")
                        .IsRequired();

                    b.HasOne("GiveLifeAPI.Models.RegionCoordinator", "Coord")
                        .WithMany("Cupon")
                        .HasForeignKey("CoordId")
                        .HasConstraintName("FK_Cupon_RegionCoordinator1")
                        .IsRequired();

                    b.HasOne("GiveLifeAPI.Models.Region", "Region")
                        .WithMany("Cupon")
                        .HasForeignKey("RegionId")
                        .HasConstraintName("FK_Cupon_Region")
                        .IsRequired();

                    b.Navigation("CaseNational");

                    b.Navigation("Coord");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.Organization", b =>
                {
                    b.HasOne("GiveLifeAPI.Models.Region", "Region")
                        .WithMany("Organization")
                        .HasForeignKey("RegionId")
                        .HasConstraintName("FK_Organization_Region")
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.Post", b =>
                {
                    b.HasOne("GiveLifeAPI.Models.Cases", "Case")
                        .WithMany("Post")
                        .HasForeignKey("CaseId")
                        .HasConstraintName("FK_Post_Cases")
                        .IsRequired();

                    b.HasOne("GiveLifeAPI.Models.RegionCoordinator", "Coord")
                        .WithMany("Post")
                        .HasForeignKey("CoordId")
                        .HasConstraintName("FK_Post_RegionCoordinator");

                    b.HasOne("GiveLifeAPI.Models.Organization", "Org")
                        .WithMany("Post")
                        .HasForeignKey("OrgId")
                        .HasConstraintName("FK_Post_Organization");

                    b.HasOne("GiveLifeAPI.Models.Region", "Region")
                        .WithMany("Post")
                        .HasForeignKey("RegionId")
                        .HasConstraintName("FK_Post_Region")
                        .IsRequired();

                    b.Navigation("Case");

                    b.Navigation("Coord");

                    b.Navigation("Org");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.RegionAdmin", b =>
                {
                    b.HasOne("GiveLifeAPI.Models.Region", "Region")
                        .WithMany("RegionAdmin")
                        .HasForeignKey("RegionId")
                        .HasConstraintName("FK_RegionAdmin_Region");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.RegionCoordinator", b =>
                {
                    b.HasOne("GiveLifeAPI.Models.RegionAdmin", "RegionAdmin")
                        .WithMany("RegionCoordinator")
                        .HasForeignKey("RegionAdminId")
                        .HasConstraintName("FK_RegionCoordinator_RegionAdmin1")
                        .IsRequired();

                    b.HasOne("GiveLifeAPI.Models.Region", "Region")
                        .WithMany("RegionCoordinator")
                        .HasForeignKey("RegionId")
                        .HasConstraintName("FK_RegionCoordinator_Region")
                        .IsRequired();

                    b.Navigation("Region");

                    b.Navigation("RegionAdmin");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.UserGroup", b =>
                {
                    b.HasOne("GiveLifeAPI.Models.Group", "Group")
                        .WithMany("UserGroup")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK_User_Group_Group")
                        .IsRequired();

                    b.HasOne("GiveLifeAPI.Models.User", "User")
                        .WithMany("UserGroup")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_User_Group_User")
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GiveLife_API.Models.Donnation", b =>
                {
                    b.HasOne("GiveLifeAPI.Models.OnlineDonner", "OnlineDonner")
                        .WithMany()
                        .HasForeignKey("OnlineDonnerId");

                    b.HasOne("GiveLifeAPI.Models.RegionCoordinator", "RegionCoordinator")
                        .WithMany()
                        .HasForeignKey("RegionCoordinatorId");

                    b.HasOne("GiveLifeAPI.Models.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OnlineDonner");

                    b.Navigation("Region");

                    b.Navigation("RegionCoordinator");
                });

            modelBuilder.Entity("GiveLife_API.Models.MoneyTransformation", b =>
                {
                    b.HasOne("GiveLifeAPI.Models.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");

                    b.HasOne("GiveLifeAPI.Models.RegionAdmin", "regionAdmin")
                        .WithMany()
                        .HasForeignKey("RegionAdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GiveLifeAPI.Models.RegionCoordinator", "RegionCoordinator")
                        .WithMany()
                        .HasForeignKey("RegionCoordinatorId");

                    b.Navigation("Organization");

                    b.Navigation("regionAdmin");

                    b.Navigation("RegionCoordinator");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.Cases", b =>
                {
                    b.Navigation("Cupon");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.Group", b =>
                {
                    b.Navigation("UserGroup");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.Organization", b =>
                {
                    b.Navigation("Post");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.Region", b =>
                {
                    b.Navigation("Cases");

                    b.Navigation("Cupon");

                    b.Navigation("Organization");

                    b.Navigation("Post");

                    b.Navigation("RegionAdmin");

                    b.Navigation("RegionCoordinator");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.RegionAdmin", b =>
                {
                    b.Navigation("RegionCoordinator");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.RegionCoordinator", b =>
                {
                    b.Navigation("Cupon");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("GiveLifeAPI.Models.User", b =>
                {
                    b.Navigation("UserGroup");
                });
#pragma warning restore 612, 618
        }
    }
}
