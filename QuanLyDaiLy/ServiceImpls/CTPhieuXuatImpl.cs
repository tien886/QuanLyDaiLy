using QuanLyDaiLy.Interfaces;
using QuanLyDaiLy.Models;
using QuanLyDaiLy.Services;

namespace QuanLyDaiLy.ServiceImpls;

public class CTPhieuXuatImpl : ICTPhieuXuatService
{
    private readonly ICTPhieuXuatRepository cTPhieuXuatRepository;
    public CTPhieuXuatImpl(ICTPhieuXuatRepository cTPhieuXuatRepository)
    {
        this.cTPhieuXuatRepository = cTPhieuXuatRepository;
    }
    public async Task AddCTPhieuXuatAsync(CTPhieuXuat ctPhieuXuat)
    {
        await cTPhieuXuatRepository.AddCTPhieuXuatAsync(ctPhieuXuat);
    }
    public async Task<IEnumerable<CTPhieuXuat>> GetAllChiTietPhieuXuat()
    {
        return await cTPhieuXuatRepository.GetAllChiTietPhieuXuat();
    }
    public async Task<int> GetNextCTPhieuXuatAvailableIdAsync()
    {
        return await cTPhieuXuatRepository.GetNextAvailableIdAsync();
    }
}
