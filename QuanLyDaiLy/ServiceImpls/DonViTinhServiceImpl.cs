using QuanLyDaiLy.Interfaces;
using QuanLyDaiLy.Repositories;
using QuanLyDaiLy.Services;

namespace QuanLyDaiLy.ServiceImpls;

public class DonViTinhServiceImpl : IDonViTinhService
{
    private readonly  IDonViTinhRepository donViTinhRepository;
    public DonViTinhServiceImpl(IDonViTinhRepository donViTinhRepository)
    {
        this.donViTinhRepository = donViTinhRepository;
    }
    public async Task<IEnumerable<string>> GetAllLoaiDonViAsync()
    {
        return await donViTinhRepository.GetAllDonViTinhsAsync();
    }
}
