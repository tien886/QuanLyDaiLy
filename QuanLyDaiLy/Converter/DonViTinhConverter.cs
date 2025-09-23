
using QuanLyDaiLy.Data;

namespace QuanLyDaiLy.Converter;

class DonViTinhConverter
{
    private readonly DataContext dataContext;
    
    public List<string> tenDonViTinhs => dataContext.DonViTinhs.Select(dvt => dvt.TenDonViTinh).ToList();
}
