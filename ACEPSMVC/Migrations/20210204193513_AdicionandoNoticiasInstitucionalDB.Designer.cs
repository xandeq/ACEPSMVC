﻿// <auto-generated />
using System;
using ACEPSMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ACEPSMVC.Migrations
{
    [DbContext(typeof(ContextoDBAplicacao))]
    [Migration("20210204193513_AdicionandoNoticiasInstitucionalDB")]
    partial class AdicionandoNoticiasInstitucionalDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("ACEPSMVC.Models.Institucional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Texto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Institucional");
                });

            modelBuilder.Entity("ACEPSMVC.Models.Noticias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagemDestaque")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagemInterna")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinhaFina")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subtitulo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Noticias");
                });
#pragma warning restore 612, 618
        }
    }
}
