using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Entities;

namespace WebApp.Data;

public class AppDbContext : IdentityDbContext<IdentityAppUser>
{
    public DbSet<Alias> Aliases { get; set; }
    public DbSet<TaskToDo> TasksToDo { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}