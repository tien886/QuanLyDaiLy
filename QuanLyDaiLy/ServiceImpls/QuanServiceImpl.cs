
using QuanLyDaiLy.Interfaces;
using QuanLyDaiLy.Models;
using QuanLyDaiLy.Repositories;
using QuanLyDaiLy.Services;

namespace QuanLyDaiLy.ServiceImpls;

public class QuanServiceImpl : IQuanService
{
    private readonly IQuanRepository quanRepository;
    public QuanServiceImpl(IQuanRepository quanRepository)
    {
        this.quanRepository = quanRepository;
    }
    public async Task<IEnumerable<Quan>> GetAllQuanAsync()
    {
        return await quanRepository.GetAllQuanAsync();
    }
}
