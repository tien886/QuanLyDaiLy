using QuanLyDaiLy.Interfaces;
using QuanLyDaiLy.Models;
using QuanLyDaiLy.Services;

namespace QuanLyDaiLy.ServiceImpls;

public class PhieuXuatServiceImpl : IPhieuXuatService
{
    private readonly IPhieuXuatRepository phieuXuatRepository;
    public PhieuXuatServiceImpl(IPhieuXuatRepository phieuXuatRepository)
    {
        this.phieuXuatRepository = phieuXuatRepository;
    }
    public Task<int> AddPhieuXuatAsync(PhieuXuat newPhieuXuat)
    {
        return phieuXuatRepository.AddPhieuXuatAsync(newPhieuXuat);
    }
    public Task<int> GetNextAvailableIdAsync()
    {
        return phieuXuatRepository.GetNextAvailableIDAsync();
    }
    public Task<IEnumerable<PhieuXuat>> GetAllPhieuXuatAsync()
    {
        return phieuXuatRepository.GetAllPhieuXuatAsync();
    }   
    public Task<IEnumerable<PhieuXuat>> GetPhieuXuatsPaging(int pageNumber, int pageSize)
    {
        return phieuXuatRepository.GetPhieuXuatsPaging(pageNumber, pageSize);
    }
}
