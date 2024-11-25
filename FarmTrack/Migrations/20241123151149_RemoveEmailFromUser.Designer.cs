﻿// <auto-generated />
using System;
using FarmTrack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FarmTrack.Migrations
{
    [DbContext(typeof(FarmContext))]
    [Migration("20241123151149_RemoveEmailFromUser")]
    partial class RemoveEmailFromUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FarmTrack.Models.Entities.Alert", b =>
                {
                    b.Property<int>("AlertId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlertId"));

                    b.Property<DateTime>("AlertDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AlertDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AlertName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Dismissed")
                        .HasColumnType("int");

                    b.Property<int>("PlantedCropId")
                        .HasColumnType("int");

                    b.Property<int>("Triggered")
                        .HasColumnType("int");

                    b.HasKey("AlertId");

                    b.HasIndex("PlantedCropId");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("FarmTrack.Models.Entities.Crop", b =>
                {
                    b.Property<int>("CropId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CropId"));

                    b.Property<string>("CropDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CropName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DaysToGrow")
                        .HasColumnType("int");

                    b.Property<string>("HarvestSeasonCold")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HarvestSeasonWarm")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlantingSeasonCold")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlantingSeasonWarm")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CropId");

                    b.ToTable("Crop");
                });

            modelBuilder.Entity("FarmTrack.Models.Entities.Field", b =>
                {
                    b.Property<int>("FieldId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Hectare")
                        .HasColumnType("real");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FieldId");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("FarmTrack.Models.Entities.PlantedCrop", b =>
                {
                    b.Property<int>("PlantedCropId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlantedCropId"));

                    b.Property<int>("Climate")
                        .HasColumnType("int");

                    b.Property<int>("CropId")
                        .HasColumnType("int");

                    b.Property<int>("FieldId")
                        .HasColumnType("int");

                    b.Property<int>("Harvested")
                        .HasColumnType("int");

                    b.Property<DateTime>("PlantDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PlantedCropId");

                    b.HasIndex("CropId");

                    b.HasIndex("FieldId");

                    b.ToTable("PlantedCrops");
                });

            modelBuilder.Entity("FarmTrack.Models.Entities.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FarmTrack.Models.Entities.Alert", b =>
                {
                    b.HasOne("FarmTrack.Models.Entities.PlantedCrop", "PlantedCrop")
                        .WithMany("Alerts")
                        .HasForeignKey("PlantedCropId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlantedCrop");
                });

            modelBuilder.Entity("FarmTrack.Models.Entities.Field", b =>
                {
                    b.HasOne("FarmTrack.Models.Entities.User", "User")
                        .WithMany("Fields")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FarmTrack.Models.Entities.PlantedCrop", b =>
                {
                    b.HasOne("FarmTrack.Models.Entities.Crop", "Crop")
                        .WithMany()
                        .HasForeignKey("CropId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FarmTrack.Models.Entities.Field", "Field")
                        .WithMany("PlantedCrops")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Crop");

                    b.Navigation("Field");
                });

            modelBuilder.Entity("FarmTrack.Models.Entities.Field", b =>
                {
                    b.Navigation("PlantedCrops");
                });

            modelBuilder.Entity("FarmTrack.Models.Entities.PlantedCrop", b =>
                {
                    b.Navigation("Alerts");
                });

            modelBuilder.Entity("FarmTrack.Models.Entities.User", b =>
                {
                    b.Navigation("Fields");
                });
#pragma warning restore 612, 618
        }
    }
}