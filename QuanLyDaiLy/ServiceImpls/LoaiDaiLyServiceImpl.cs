    
using QuanLyDaiLy.Interfaces;
using QuanLyDaiLy.Models;
using QuanLyDaiLy.Services;

namespace QuanLyDaiLy.ServiceImpls;

public class LoaiDaiLyServiceImpl : ILoaiDaiLyService
{
    private readonly ILoaiDaiLyRepository loaiDaiLyRepository;
    public LoaiDaiLyServiceImpl(ILoaiDaiLyRepository loaiDaiLyRepository)
    {
        this.loaiDaiLyRepository = loaiDaiLyRepository;
    }
    public async Task<IEnumerable<LoaiDaiLy>> GetAllLoaiDaiLyAsync()
    {
        return await loaiDaiLyRepository.GetAllLoaiDaiLyAsync();
    }

}
