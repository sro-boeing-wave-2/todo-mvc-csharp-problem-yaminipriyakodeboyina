﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using finaltodo.Models;

namespace finaltodo.Migrations
{
    [DbContext(typeof(finaltodoContext))]
    [Migration("20180803074655_model")]
    partial class model
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Todolist.Models.checklist", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Todoid");

                    b.Property<string>("checkname");

                    b.HasKey("id");

                    b.HasIndex("Todoid");

                    b.ToTable("checklist");
                });

            modelBuilder.Entity("Todolist.Models.labels", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Todoid");

                    b.Property<string>("labelname");

                    b.HasKey("id");

                    b.HasIndex("Todoid");

                    b.ToTable("labels");
                });

            modelBuilder.Entity("Todolist.Models.Todo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("heading");

                    b.Property<bool>("pinned");

                    b.Property<string>("text");

                    b.HasKey("id");

                    b.ToTable("Todo");
                });

            modelBuilder.Entity("Todolist.Models.checklist", b =>
                {
                    b.HasOne("Todolist.Models.Todo")
                        .WithMany("checklist")
                        .HasForeignKey("Todoid");
                });

            modelBuilder.Entity("Todolist.Models.labels", b =>
                {
                    b.HasOne("Todolist.Models.Todo")
                        .WithMany("label")
                        .HasForeignKey("Todoid");
                });
#pragma warning restore 612, 618
        }
    }
}
