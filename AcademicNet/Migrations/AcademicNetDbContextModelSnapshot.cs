﻿// <auto-generated />
using System;
using AcademicNet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AcademicNet.Migrations
{
    [DbContext(typeof(AcademicNetDbContext))]
    partial class AcademicNetDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AcademicNet.Models.AddressModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("char(9)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("char(2)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Referencia")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Selecionado")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("AcademicNet.Models.ClassModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float>("AVGFrequency")
                        .HasColumnType("real");

                    b.Property<float>("AVGGrade")
                        .HasColumnType("real");

                    b.Property<float>("AVGGradeFrequency")
                        .HasColumnType("real");

                    b.Property<int>("CoordinatorId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UnitId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CoordinatorId");

                    b.HasIndex("UnitId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("AcademicNet.Models.ClassSubjectModel", b =>
                {
                    b.Property<int>("ClassId")
                        .HasColumnType("integer");

                    b.Property<int>("SubjectId")
                        .HasColumnType("integer");

                    b.Property<float>("AVGFrequency")
                        .HasColumnType("real");

                    b.Property<float>("AVGGrade")
                        .HasColumnType("real");

                    b.Property<float>("AVGGradeFrequency")
                        .HasColumnType("real");

                    b.Property<int>("TeacherId")
                        .HasColumnType("integer");

                    b.Property<int>("UnitId")
                        .HasColumnType("integer");

                    b.HasKey("ClassId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.HasIndex("UnitId");

                    b.ToTable("ClassSubject", (string)null);
                });

            modelBuilder.Entity("AcademicNet.Models.CoordinatorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Coordinators");
                });

            modelBuilder.Entity("AcademicNet.Models.PostageModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int?>("ParentPostageId")
                        .HasColumnType("integer");

                    b.Property<string>("PathToPhoto")
                        .HasColumnType("text");

                    b.Property<int>("StudentStudiesGroupStudentId")
                        .HasColumnType("integer");

                    b.Property<int>("StudentStudiesGroupStudiesGroupId")
                        .HasColumnType("integer");

                    b.Property<string>("TextBody")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParentPostageId");

                    b.HasIndex("StudentStudiesGroupStudiesGroupId", "StudentStudiesGroupStudentId");

                    b.ToTable("Postages");
                });

            modelBuilder.Entity("AcademicNet.Models.StudentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float?>("AVGFrequency")
                        .HasColumnType("real");

                    b.Property<float?>("AVGGrade")
                        .HasColumnType("real");

                    b.Property<float?>("AVGGradeFrequency")
                        .HasColumnType("real");

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ClassId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PathToPhotoProfile")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("ClassId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("AcademicNet.Models.StudentStudiesGroupModel", b =>
                {
                    b.Property<int>("StudiesGroupId")
                        .HasColumnType("integer");

                    b.Property<int>("StudentId")
                        .HasColumnType("integer");

                    b.HasKey("StudiesGroupId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentStudiesGroups");
                });

            modelBuilder.Entity("AcademicNet.Models.StudentSubjectModel", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("integer");

                    b.Property<int>("SubjectId")
                        .HasColumnType("integer");

                    b.Property<int>("ClassSubjectClassId")
                        .HasColumnType("integer");

                    b.Property<int>("ClassSubjectSubjectId")
                        .HasColumnType("integer");

                    b.Property<float>("Frequency")
                        .HasColumnType("real");

                    b.Property<float>("Grade")
                        .HasColumnType("real");

                    b.Property<float>("GradeFrequency")
                        .HasColumnType("real");

                    b.HasKey("StudentId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("ClassSubjectClassId", "ClassSubjectSubjectId");

                    b.ToTable("Matriculation");
                });

            modelBuilder.Entity("AcademicNet.Models.StudiesGroupModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("StudiesGroups");
                });

            modelBuilder.Entity("AcademicNet.Models.SubjectModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("AcademicNet.Models.TeacherModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Degree")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PathToPhotoProfile")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("AcademicNet.Models.UnitModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float>("AVGFrequencyPerClass")
                        .HasColumnType("real");

                    b.Property<float>("AVGFrequencyPerClassSubject")
                        .HasColumnType("real");

                    b.Property<float>("AVGGradeFrequencyPerClass")
                        .HasColumnType("real");

                    b.Property<float>("AVGGradeFrequencyPerClassSubject")
                        .HasColumnType("real");

                    b.Property<float>("AVGGradePerClass")
                        .HasColumnType("real");

                    b.Property<float>("AVGGradePerClassSubject")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("AcademicNet.Models.ClassModel", b =>
                {
                    b.HasOne("AcademicNet.Models.CoordinatorModel", "Coordinator")
                        .WithMany("Classes")
                        .HasForeignKey("CoordinatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicNet.Models.UnitModel", "Unit")
                        .WithMany("Classes")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coordinator");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("AcademicNet.Models.ClassSubjectModel", b =>
                {
                    b.HasOne("AcademicNet.Models.ClassModel", "Class")
                        .WithMany("ClassSubjects")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicNet.Models.SubjectModel", "Subject")
                        .WithMany("ClassSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicNet.Models.TeacherModel", "Teacher")
                        .WithMany("ClassSubjects")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicNet.Models.UnitModel", "Unit")
                        .WithMany("ClassSubjects")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Subject");

                    b.Navigation("Teacher");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("AcademicNet.Models.CoordinatorModel", b =>
                {
                    b.HasOne("AcademicNet.Models.AddressModel", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("AcademicNet.Models.PostageModel", b =>
                {
                    b.HasOne("AcademicNet.Models.PostageModel", "ParentPostage")
                        .WithMany("Replies")
                        .HasForeignKey("ParentPostageId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AcademicNet.Models.StudentStudiesGroupModel", "StudentStudiesGroup")
                        .WithMany("Postages")
                        .HasForeignKey("StudentStudiesGroupStudiesGroupId", "StudentStudiesGroupStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentPostage");

                    b.Navigation("StudentStudiesGroup");
                });

            modelBuilder.Entity("AcademicNet.Models.StudentModel", b =>
                {
                    b.HasOne("AcademicNet.Models.AddressModel", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicNet.Models.ClassModel", "Class")
                        .WithMany("Students")
                        .HasForeignKey("ClassId");

                    b.Navigation("Address");

                    b.Navigation("Class");
                });

            modelBuilder.Entity("AcademicNet.Models.StudentStudiesGroupModel", b =>
                {
                    b.HasOne("AcademicNet.Models.StudentModel", "Student")
                        .WithMany("StudentStudiesGroups")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicNet.Models.StudiesGroupModel", "StudiesGroup")
                        .WithMany("StudentStudiesGroups")
                        .HasForeignKey("StudiesGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("StudiesGroup");
                });

            modelBuilder.Entity("AcademicNet.Models.StudentSubjectModel", b =>
                {
                    b.HasOne("AcademicNet.Models.StudentModel", "Student")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicNet.Models.SubjectModel", "Subject")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicNet.Models.ClassSubjectModel", "ClassSubject")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("ClassSubjectClassId", "ClassSubjectSubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassSubject");

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("AcademicNet.Models.TeacherModel", b =>
                {
                    b.HasOne("AcademicNet.Models.AddressModel", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("AcademicNet.Models.ClassModel", b =>
                {
                    b.Navigation("ClassSubjects");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("AcademicNet.Models.ClassSubjectModel", b =>
                {
                    b.Navigation("StudentSubjects");
                });

            modelBuilder.Entity("AcademicNet.Models.CoordinatorModel", b =>
                {
                    b.Navigation("Classes");
                });

            modelBuilder.Entity("AcademicNet.Models.PostageModel", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("AcademicNet.Models.StudentModel", b =>
                {
                    b.Navigation("StudentStudiesGroups");

                    b.Navigation("StudentSubjects");
                });

            modelBuilder.Entity("AcademicNet.Models.StudentStudiesGroupModel", b =>
                {
                    b.Navigation("Postages");
                });

            modelBuilder.Entity("AcademicNet.Models.StudiesGroupModel", b =>
                {
                    b.Navigation("StudentStudiesGroups");
                });

            modelBuilder.Entity("AcademicNet.Models.SubjectModel", b =>
                {
                    b.Navigation("ClassSubjects");

                    b.Navigation("StudentSubjects");
                });

            modelBuilder.Entity("AcademicNet.Models.TeacherModel", b =>
                {
                    b.Navigation("ClassSubjects");
                });

            modelBuilder.Entity("AcademicNet.Models.UnitModel", b =>
                {
                    b.Navigation("ClassSubjects");

                    b.Navigation("Classes");
                });
#pragma warning restore 612, 618
        }
    }
}
