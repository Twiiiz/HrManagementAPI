using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace HrManagementAPI.Models;

public partial class HrManagementContext : DbContext
{
    private readonly IConfiguration _configuration;

    public HrManagementContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    static HrManagementContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<CandidateStatus>("candidate_status");
        NpgsqlConnection.GlobalTypeMapper.MapEnum<OpeningStatus>("opening_status");
        NpgsqlConnection.GlobalTypeMapper.MapEnum<PositionType>("position_type");
    }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<CandidateSubmission> CandidateSubmissions { get; set; }

    public virtual DbSet<EmployeeSkill> EmployeeSkills { get; set; }

    public virtual DbSet<HrManager> HrManagers { get; set; }

    public virtual DbSet<JobOpening> JobOpenings { get; set; }

    public virtual DbSet<JobPosition> JobPositions { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Office> Offices { get; set; }

    public virtual DbSet<SubmissionSkill> SubmissionSkills { get; set; }

    public virtual DbSet<SubmissionStatus> SubmissionStatuses { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagSubmission> TagSubmissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("candidate_status", new[] { "hired", "not_hired", "offer_made", "offer_denied", "spam" })
            .HasPostgresEnum("opening_status", new[] { "available", "closed", "offer_under_consideration" })
            .HasPostgresEnum("position_type", new[] { "developer", "designer", "qa", "data_related", "project_manager", "sys_admin", "support", "hr" });

        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.CandidateId).HasName("candidates_pkey");

            entity.ToTable("candidates");

            entity.Property(e => e.CandidateId).HasColumnName("candidate_id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(35)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phone_number");
            entity.Property(e => e.Status)
                .HasColumnName("status")
                .HasColumnType("candidate_status");
        });

        modelBuilder.Entity<CandidateSubmission>(entity =>
        {
            entity.HasKey(e => e.SubId).HasName("candidate_submissions_pkey");

            entity.ToTable("candidate_submissions");

            entity.Property(e => e.SubId).HasColumnName("sub_id");
            entity.Property(e => e.CandidateId).HasColumnName("candidate_id");
            entity.Property(e => e.CvFilepath).HasColumnName("cv_filepath");
            entity.Property(e => e.HrId).HasColumnName("hr_id");
            entity.Property(e => e.JobPosition)
                .HasMaxLength(25)
                .HasColumnName("job_position");
            entity.Property(e => e.PrefferredLocation)
                .HasColumnType("character varying(20)[]")
                .HasColumnName("prefferred_location");
            entity.Property(e => e.SubDate).HasColumnName("sub_date");

            entity.HasOne(d => d.Candidate).WithMany(p => p.CandidateSubmissions)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("candidate_submissions_candidate_id_fkey");

            entity.HasOne(d => d.Hr).WithMany(p => p.CandidateSubmissions)
                .HasForeignKey(d => d.HrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("candidate_submissions_hr_id_fkey");
        });

        modelBuilder.Entity<EmployeeSkill>(entity =>
        {
            entity.HasKey(e => e.EmpSkillId).HasName("employee_skills_pkey");

            entity.ToTable("employee_skills");

            entity.Property(e => e.EmpSkillId).HasColumnName("emp_skill_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EmpSkillName)
                .HasMaxLength(20)
                .HasColumnName("emp_skill_name");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeSkills)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("employee_skills_employee_id_fkey");
        });

        modelBuilder.Entity<HrManager>(entity =>
        {
            entity.HasKey(e => e.HrId).HasName("employees_pkey");

            entity.ToTable("hr_managers");

            entity.Property(e => e.HrId)
                .HasDefaultValueSql("nextval('employees_employee_id_seq'::regclass)")
                .HasColumnName("hr_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate).HasColumnName("hire_date");
            entity.Property(e => e.LastName)
                .HasMaxLength(35)
                .HasColumnName("last_name");
            entity.Property(e => e.MonthlySalary).HasColumnName("monthly_salary");
            entity.Property(e => e.OfficeId).HasColumnName("office_id");

            entity.HasOne(d => d.Office).WithMany(p => p.HrManagers)
                .HasForeignKey(d => d.OfficeId)
                .HasConstraintName("employees_office_id_fkey");
        });

        modelBuilder.Entity<JobOpening>(entity =>
        {
            entity.HasKey(e => e.OpeningId).HasName("job_openings_pkey");

            entity.ToTable("job_openings");

            entity.Property(e => e.OpeningId).HasColumnName("opening_id");
            entity.Property(e => e.HiredCandidate).HasColumnName("hired_candidate");
            entity.Property(e => e.LastUpdateDate).HasColumnName("last_update_date");
            entity.Property(e => e.OfficeId).HasColumnName("office_id");
            entity.Property(e => e.OpeningDate).HasColumnName("opening_date");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.Status)
                .HasColumnName("status")
                .HasColumnType("opening_status");

            entity.HasOne(d => d.HiredCandidateNavigation).WithMany(p => p.JobOpenings)
                .HasForeignKey(d => d.HiredCandidate)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("job_openings_hired_candidate_fkey");

            entity.HasOne(d => d.Office).WithMany(p => p.JobOpenings)
                .HasForeignKey(d => d.OfficeId)
                .HasConstraintName("job_openings_office_id_fkey");

            entity.HasOne(d => d.Position).WithMany(p => p.JobOpenings)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("job_openings_position_id_fkey");
        });

        modelBuilder.Entity<JobPosition>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("job_positions_pkey");

            entity.ToTable("job_positions");

            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.PositionType)
                .HasColumnType("position_type")
                .HasColumnName("position_type");
            entity.Property(e => e.PositionName)
                .HasColumnType("character varying")
                .HasColumnName("position_name");
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.NoteId).HasName("notes_pkey");

            entity.ToTable("notes");

            entity.Property(e => e.NoteId).HasColumnName("note_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.NoteDate).HasColumnName("note_date");
            entity.Property(e => e.SubId).HasColumnName("sub_id");

            entity.HasOne(d => d.Sub).WithMany(p => p.Notes)
                .HasForeignKey(d => d.SubId)
                .HasConstraintName("notes_sub_id_fkey");
        });

        modelBuilder.Entity<Office>(entity =>
        {
            entity.HasKey(e => e.OfficeId).HasName("offices_pkey");

            entity.ToTable("offices");

            entity.Property(e => e.OfficeId).HasColumnName("office_id");
            entity.Property(e => e.Address)
                .HasMaxLength(60)
                .HasColumnName("address");
        });

        modelBuilder.Entity<SubmissionSkill>(entity =>
        {
            entity.HasKey(e => e.SubSkillId).HasName("submission_skills_pkey");

            entity.ToTable("submission_skills");

            entity.Property(e => e.SubSkillId).HasColumnName("sub_skill_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.SubId).HasColumnName("sub_id");
            entity.Property(e => e.SubSkillName)
                .HasMaxLength(20)
                .HasColumnName("sub_skill_name");

            entity.HasOne(d => d.Sub).WithMany(p => p.SubmissionSkills)
                .HasForeignKey(d => d.SubId)
                .HasConstraintName("submission_skills_sub_id_fkey");
        });

        modelBuilder.Entity<SubmissionStatus>(entity =>
        {
            entity.HasKey(e => e.SubStatId).HasName("submission_statuses_pkey");

            entity.ToTable("submission_statuses");

            entity.Property(e => e.SubStatId).HasColumnName("sub_stat_id");
            entity.Property(e => e.StatusDate).HasColumnName("status_date");
            entity.Property(e => e.StatusName)
                .HasMaxLength(40)
                .HasColumnName("status_name");
            entity.Property(e => e.SubId).HasColumnName("sub_id");

            entity.HasOne(d => d.Sub).WithMany(p => p.SubmissionStatuses)
                .HasForeignKey(d => d.SubId)
                .HasConstraintName("submission_statuses_sub_id_fkey");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("tags_pkey");

            entity.ToTable("tags");

            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.CreationDate).HasColumnName("creation_date");
            entity.Property(e => e.HrId).HasColumnName("hr_id");
            entity.Property(e => e.LastUpdateDate).HasColumnName("last_update_date");
            entity.Property(e => e.TagName)
                .HasMaxLength(50)
                .HasColumnName("tag_name");

            entity.HasOne(d => d.Hr).WithMany(p => p.Tags)
                .HasForeignKey(d => d.HrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tags_hr_id_fkey");
        });

        modelBuilder.Entity<TagSubmission>(entity =>
        {
            entity.HasKey(e => e.SubTagId).HasName("tag_submissions_pkey");

            entity.ToTable("tag_submissions");

            entity.Property(e => e.SubTagId).HasColumnName("sub_tag_id");
            entity.Property(e => e.SubId).HasColumnName("sub_id");
            entity.Property(e => e.TagId).HasColumnName("tag_id");

            entity.HasOne(d => d.Sub).WithMany(p => p.TagSubmissions)
                .HasForeignKey(d => d.SubId)
                .HasConstraintName("tag_submissions_sub_id_fkey");

            entity.HasOne(d => d.Tag).WithMany(p => p.TagSubmissions)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("tag_submissions_tag_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
