
using CommunityToolkit.Mvvm.ComponentModel;

namespace QuanLyDaiLy.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        protected string title = string.Empty;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotLoading))]
        private bool isLoading = false;

        public bool IsNotLoading => !IsLoading;

    }
}
