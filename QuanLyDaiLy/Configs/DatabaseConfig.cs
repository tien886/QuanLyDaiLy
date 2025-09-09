
using QuanLyDaiLy.Data;
using System.Diagnostics;
using System.Reflection;

namespace QuanLyDaiLy.Configs;

public class DatabaseConfig
{
    private readonly DataContext _dataContext;
    public DatabaseConfig(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public DataContext DataContext => _dataContext ?? throw new Exception("Database not initialized");
    public static string GetResourcePath()
    {
        string appDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? 
                            AppDomain.CurrentDomain.BaseDirectory;
        string relativePath = Path.Combine(appDirectory, @"..\..\..\..\..\Resources\Database");
        string databaseDirectory = Path.GetFullPath(relativePath);
        Directory.CreateDirectory(databaseDirectory); // Ensure the directory exists

        return Path.Combine(databaseDirectory, $"QuanLyDaiLy.db");
    }   

    public void Initialize()
    {
        Debug.WriteLine("init begin");
        try
        {
            _dataContext.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Database initialization error: {ex.Message}");
        }
        Debug.WriteLine("init end");
    }
}
