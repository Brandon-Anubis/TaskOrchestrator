using Microsoft.EntityFrameworkCore;
using TaskOrchestrator.Domain.Entities;

namespace TaskOrchestrator.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<WorkTask> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<TaskComment> TaskComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WorkTask>(entity =>
        {
            entity.ToTable("Tasks");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(2000);
            
            entity.HasOne(e => e.AssignedTo)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(e => e.AssignedToId)
                .OnDelete(DeleteBehavior.SetNull);
                
            entity.HasOne(e => e.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.UserName).IsUnique();
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("Projects");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(2000);
            
            entity.HasOne(e => e.Owner)
                .WithMany(u => u.OwnedProjects)
                .HasForeignKey(e => e.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.ToTable("Teams");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            
            entity.HasOne(e => e.Project)
                .WithMany(p => p.Teams)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<TeamMember>(entity =>
        {
            entity.ToTable("TeamMembers");
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.Team)
                .WithMany(t => t.Members)
                .HasForeignKey(e => e.TeamId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasOne(e => e.User)
                .WithMany(u => u.TeamMemberships)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasIndex(e => new { e.TeamId, e.UserId }).IsUnique();
        });

        modelBuilder.Entity<TaskComment>(entity =>
        {
            entity.ToTable("TaskComments");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Content).IsRequired().HasMaxLength(1000);
            
            entity.HasOne(e => e.Task)
                .WithMany(t => t.Comments)
                .HasForeignKey(e => e.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
