using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataModels;

public class AppDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    
    
    public string DbPath { get; set; }

    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "GoodEveningTech.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
    
    
}