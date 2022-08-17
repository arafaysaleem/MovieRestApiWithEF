﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieRestApiWithEF.EfCore;

#nullable disable

namespace MovieRestApiWithEF.Migrations
{
    [DbContext(typeof(MovieAppDbContext))]
    [Migration("20220817142152_MovieAppDbContext.Movie.UpdateWorkerRelationshipConstraints")]
    partial class MovieAppDbContextMovieUpdateWorkerRelationshipConstraints
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MovieCast", b =>
                {
                    b.Property<int>("CastId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("CastId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieCast");
                });

            modelBuilder.Entity("MovieRestApiWithEF.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DirectorId")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DirectorId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieRestApiWithEF.Models.MovieWorker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PictureUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("MovieWorker");
                });

            modelBuilder.Entity("MovieCast", b =>
                {
                    b.HasOne("MovieRestApiWithEF.Models.MovieWorker", null)
                        .WithMany()
                        .HasForeignKey("CastId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MovieRestApiWithEF.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieRestApiWithEF.Models.Movie", b =>
                {
                    b.HasOne("MovieRestApiWithEF.Models.MovieWorker", "Director")
                        .WithMany("DirectedMovies")
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Director");
                });

            modelBuilder.Entity("MovieRestApiWithEF.Models.MovieWorker", b =>
                {
                    b.Navigation("DirectedMovies");
                });
#pragma warning restore 612, 618
        }
    }
}
