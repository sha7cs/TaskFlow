using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.Entities;

namespace TaskFlow.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TaskItem> Tasks => Set<TaskItem>(); // repesenting the Tasks table in the database
}