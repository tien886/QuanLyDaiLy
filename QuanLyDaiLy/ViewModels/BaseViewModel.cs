
using CommunityToolkit.Mvvm.ComponentModel;

namespace QuanLyDaiLy.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        protected string title = string.Empty;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(isNotLoading))]
        private bool isLoading = false;

        public bool isNotLoading => !isLoading;

    }
}
