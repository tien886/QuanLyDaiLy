using QuanLyDaiLy.Configs;
using QuanLyDaiLy.Models;
using QuanLyDaiLy.Services;
using System.Diagnostics;

namespace QuanLyDaiLy.ServiceImpls;

public class DatabaseServiceImpl : DatabaseService
{
    private readonly DatabaseConfig databaseConfig;
    public DatabaseServiceImpl(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }
    public async Task InitializeAsync()
    {
        databaseConfig.Initialize();
    }
}
