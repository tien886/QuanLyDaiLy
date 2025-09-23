
namespace QuanLyDaiLy.Interfaces;

public interface IDonViTinhRepository
{
    Task<IEnumerable<string>> GetAllDonViTinhsAsync();  
}
