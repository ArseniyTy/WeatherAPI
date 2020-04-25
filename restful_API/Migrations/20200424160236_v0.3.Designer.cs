﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using restful_API.Models;

namespace restful_API.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20200424160236_v0.3")]
    partial class v03
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("restful_API.Models.User", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Login");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("restful_API.Models.UserWeatherForecast", b =>
                {
                    b.Property<string>("UserLogin")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("WeatherForecastId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserLogin", "WeatherForecastId");

                    b.HasIndex("WeatherForecastId");

                    b.ToTable("UserWeatherForecasts");
                });

            modelBuilder.Entity("restful_API.Models.WeatherForecast", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Temperature")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("WeatherForecasts");
                });

            modelBuilder.Entity("restful_API.Models.UserWeatherForecast", b =>
                {
                    b.HasOne("restful_API.Models.User", "User")
                        .WithMany("UserWeatherForecasts")
                        .HasForeignKey("UserLogin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("restful_API.Models.WeatherForecast", "WeatherForecast")
                        .WithMany("UserWeatherForecasts")
                        .HasForeignKey("WeatherForecastId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
