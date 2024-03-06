using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using TaskMenager.Components.dbcontroll;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) {

        try
        {
            var dbcreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if(dbcreator != null)
            {
                if(!dbcreator.CanConnect())
                {
                    dbcreator.Create();
                }
                if(!dbcreator.HasTables())
                {
                    dbcreator.CreateTables();
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    
    }
    public DbSet<Tasks> Tasks { get; set; }
    public DbSet<Project> Projects { get; set; }
    //new
    public MyDbContext(DbContextOptions<DbContext> options) : base(options)
    {
    }
}



