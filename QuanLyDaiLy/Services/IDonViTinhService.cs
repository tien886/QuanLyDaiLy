namespace QuanLyDaiLy.Services;

public interface IDonViTinhService
{
    public Task<IEnumerable<string>> GetAllLoaiDonViAsync();
}
