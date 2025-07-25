﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using QuemVaiVai.Infrastructure.Contexts;

#nullable disable

namespace QuemVaiVai.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("CreatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("created_user");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<Guid>("DeletedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_user");

                    b.Property<int>("EventId")
                        .HasColumnType("integer")
                        .HasColumnName("event_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UpdatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("updated_user");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("comments", (string)null);
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("CreatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("created_user");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<Guid>("DeletedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_user");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("event_date");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer")
                        .HasColumnName("group_id");

                    b.Property<string>("Location")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("location");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UpdatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("updated_user");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("events", (string)null);
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("CreatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("created_user");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<Guid>("DeletedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_user");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UpdatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("updated_user");

                    b.HasKey("Id");

                    b.ToTable("groups", (string)null);
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.GroupUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("CreatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("created_user");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<Guid>("DeletedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_user");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer")
                        .HasColumnName("group_id");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UpdatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("updated_user");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("group_users", (string)null);
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.TaskItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AssignedTo")
                        .HasColumnType("integer");

                    b.Property<int?>("AssignedUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("CreatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("created_user");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<Guid>("DeletedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_user");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<bool>("IsDone")
                        .HasColumnType("boolean")
                        .HasColumnName("is_done");

                    b.Property<int>("TaskListId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UpdatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("updated_user");

                    b.HasKey("Id");

                    b.HasIndex("AssignedUserId");

                    b.HasIndex("TaskListId");

                    b.ToTable("task_items", (string)null);
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.TaskList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("CreatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("created_user");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<Guid>("DeletedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_user");

                    b.Property<int>("EventId")
                        .HasColumnType("integer")
                        .HasColumnName("event_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UpdatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("updated_user");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("task_lists", (string)null);
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("CreatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("created_user");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<Guid>("DeletedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_user");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_salt");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UpdatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("updated_user");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.UserEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("CreatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("created_user");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<Guid>("DeletedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_user");

                    b.Property<int>("EventId")
                        .HasColumnType("integer")
                        .HasColumnName("event_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UpdatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("updated_user");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("user_events", (string)null);
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.Vote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("CreatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("created_user");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<Guid>("DeletedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_user");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UpdatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("updated_user");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int>("VoteOptionId")
                        .HasColumnType("integer")
                        .HasColumnName("vote_option_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("VoteOptionId");

                    b.ToTable("votes", (string)null);
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.VoteOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("CreatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("created_user");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<Guid>("DeletedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_user");

                    b.Property<int>("EventId")
                        .HasColumnType("integer")
                        .HasColumnName("event_id");

                    b.Property<DateTime?>("SuggestedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("suggested_date");

                    b.Property<string>("SuggestedLocation")
                        .HasColumnType("text")
                        .HasColumnName("suggested_location");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UpdatedUser")
                        .HasColumnType("uuid")
                        .HasColumnName("updated_user");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("vote_options", (string)null);
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.Comment", b =>
                {
                    b.HasOne("QuemVaiVai.Domain.Entities.Event", "Event")
                        .WithMany("Comments")
                        .HasForeignKey("EventId")
                        .IsRequired();

                    b.HasOne("QuemVaiVai.Domain.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.Event", b =>
                {
                    b.HasOne("QuemVaiVai.Domain.Entities.Group", "Group")
                        .WithMany("Events")
                        .HasForeignKey("GroupId")
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.GroupUser", b =>
                {
                    b.HasOne("QuemVaiVai.Domain.Entities.Group", "Group")
                        .WithMany("GroupUsers")
                        .HasForeignKey("GroupId")
                        .IsRequired();

                    b.HasOne("QuemVaiVai.Domain.Entities.User", "User")
                        .WithMany("GroupUsers")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.TaskItem", b =>
                {
                    b.HasOne("QuemVaiVai.Domain.Entities.User", "AssignedUser")
                        .WithMany("TaskItems")
                        .HasForeignKey("AssignedUserId");

                    b.HasOne("QuemVaiVai.Domain.Entities.TaskList", "TaskList")
                        .WithMany("TaskItems")
                        .HasForeignKey("TaskListId")
                        .IsRequired();

                    b.Navigation("AssignedUser");

                    b.Navigation("TaskList");
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.TaskList", b =>
                {
                    b.HasOne("QuemVaiVai.Domain.Entities.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.UserEvent", b =>
                {
                    b.HasOne("QuemVaiVai.Domain.Entities.Event", "Event")
                        .WithMany("UserEvents")
                        .HasForeignKey("EventId")
                        .IsRequired();

                    b.HasOne("QuemVaiVai.Domain.Entities.User", "User")
                        .WithMany("UserEvents")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.Vote", b =>
                {
                    b.HasOne("QuemVaiVai.Domain.Entities.User", "User")
                        .WithMany("Votes")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.HasOne("QuemVaiVai.Domain.Entities.VoteOption", "VoteOption")
                        .WithMany("Votes")
                        .HasForeignKey("VoteOptionId")
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("VoteOption");
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.VoteOption", b =>
                {
                    b.HasOne("QuemVaiVai.Domain.Entities.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.Event", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("UserEvents");
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.Group", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("GroupUsers");
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.TaskList", b =>
                {
                    b.Navigation("TaskItems");
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("GroupUsers");

                    b.Navigation("TaskItems");

                    b.Navigation("UserEvents");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("QuemVaiVai.Domain.Entities.VoteOption", b =>
                {
                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}
