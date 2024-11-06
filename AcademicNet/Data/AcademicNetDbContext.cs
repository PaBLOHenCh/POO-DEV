using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AcademicNet.Models;



namespace AcademicNet.Data
{
    public class AcademicNetDbContext : DbContext
    {
        //construtor
         public AcademicNetDbContext(DbContextOptions<AcademicNetDbContext> options) : base(options)
        {
        }

        //dbsets criam instancias para tratarmos as tabelas como objetos na manipulação do CRUD
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<CoordinatorModel> Coordinators { get; set; }
        public DbSet<TeacherModel> Teachers { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<SubjectModel> Subjects { get; set; }
        public DbSet<StudentSubjectModel> Matriculations { get; set; }
        public DbSet<ClassSubjectModel> ClassSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ClassSubjectModel>().ToTable("ClassSubject");
            
            //configura os relacionamentos student e subject para many to many
            //chave primaria composta de student e subject
            builder.Entity<StudentSubjectModel>()
                .HasKey(ss => new { ss.StudentId, ss.SubjectId});

            builder.Entity<StudentSubjectModel>()
                .HasOne<StudentModel>()
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.StudentId);

            builder.Entity<StudentSubjectModel>()
                .HasOne<SubjectModel>()
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.SubjectId);

            //chave estrangeira composta de class_subject
            builder.Entity<StudentSubjectModel>()
                .HasOne<ClassSubjectModel>()
                .WithMany(cs => cs.StudentSubjects)
                .HasForeignKey(ss => new{ss.ClassSubjectClassId, ss.ClassSubjectSubjectId});
            //as relacoes one to many de class_subject com Subject e Class são feitas por data Annotations com o foreign key

            builder.Entity<StudentSubjectModel>()
                .HasOne(ss => ss.ClassSubject)
                .WithMany(cs => cs.StudentSubjects)
                .HasForeignKey(ss => new { ss.ClassSubjectClassId, ss.ClassSubjectSubjectId });

            builder.Entity<ClassSubjectModel>()
                .HasKey(cs => new { cs.ClassId, cs.SubjectId });

            builder.Entity<ClassSubjectModel>()
                .HasOne(cs => cs.Subject)
                .WithMany(s => s.ClassSubjects)
                .HasForeignKey(cs => cs.SubjectId);

            builder.Entity<ClassSubjectModel>()
                .HasOne(cs => cs.Class)
                .WithMany(c => c.ClassSubjects)
                .HasForeignKey(cs => cs.ClassId);

            builder.Entity<ClassSubjectModel>()
                .HasOne(cs => cs.Unit)
                .WithMany(u => u.ClassSubjects)
                .HasForeignKey(cs => cs.UnitId);

            builder.Entity<ClassSubjectModel>()
                .HasOne(cs => cs.Teacher)
                .WithMany(t => t.ClassSubjects)
                .HasForeignKey(cs => cs.TeacherId);
            
        }

    }
}