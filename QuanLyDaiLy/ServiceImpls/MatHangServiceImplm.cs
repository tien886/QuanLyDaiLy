using QuanLyDaiLy.Repositories;
using QuanLyDaiLy.Models;
using QuanLyDaiLy.Services;
using QuanLyDaiLy.Interfaces;
namespace QuanLyDaiLy.ServiceImpls;

public class MatHangServiceImplm : IMatHangService
{
    private readonly IMatHangRepository matHangRepository;
    public MatHangServiceImplm(IMatHangRepository matHangRepository)
    {
        this.matHangRepository = matHangRepository;
    }
    public async Task<IEnumerable<MatHang>> GetAllMatHangsAsync()
    {
        return await matHangRepository.GetAllMatHangsAsync();
    }
    public async Task<MatHang> GetMatHangByID(int MaMatHang)
    {
        return await matHangRepository.GetMatHangByID(MaMatHang);
    }
    public async Task UpdateMatHang(MatHang matHang)
    {
        await matHangRepository.UpdateMatHang(matHang);
    }
}
