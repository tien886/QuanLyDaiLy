
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
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotDeleting))]
        private bool isDeleting = false;
        public bool IsNotDeleting => !isDeleting;

    }
}
