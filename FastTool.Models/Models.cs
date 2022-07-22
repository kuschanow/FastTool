using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;

namespace FastTool.Models;

public class DBContext : DbContext
{
    public DbSet<CalculatorResultsHistory> calcHistory { get; set; }

    public string DbPath { get; }

    public DBContext()
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        Directory.CreateDirectory($"{path}\\FastTool");
        DbPath = Path.Combine(path, @"FastTool\fasttool.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }
}

public class CalculatorResultsHistory
{
    public int id { get; set; }
    public string expression { get; set; }
    public float result { get; set; }
}

