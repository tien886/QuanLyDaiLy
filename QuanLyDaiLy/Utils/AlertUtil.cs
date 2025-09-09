

namespace QuanLyDaiLy.Utils;

public class AlertUtil
{
    public static async Task ShowAlert(string Messages)
    {
        var mainpage = Application.Current.MainPage;
        if(mainpage != null)
        {
            await mainpage.DisplayAlert("CAUTION", Messages, "OK");
        }
    }

    public static async Task ShowFailed(string Messages)
    {
        var mainpage = Application.Current.MainPage;
        if (mainpage != null)
        {
            await mainpage.DisplayAlert("FAILED ACTION", Messages, "OK");
        }
    }

    public static async Task ShowSuccess(string Messages)
    {
        var mainpage = Application.Current.MainPage;
        if (mainpage != null)
        {
            await mainpage.DisplayAlert("SUCCESSFULLY", Messages, "OK");
        }
    }
}
