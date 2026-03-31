using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PositionSizeCalculator.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private IConnectivity connectivity;

        [ObservableProperty]
        private ObservableCollection<string> items;

        [ObservableProperty]
        private string text;

        public MainViewModel(IConnectivity connectivity)
        {
            items = new ObservableCollection<string>();
            this.connectivity = connectivity;
        }

        [RelayCommand]
        async Task Add()
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                return;
            }

            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Error", "No internet detected", "OK");
                return;
            }

            Items.Add(Text);
            Text = string.Empty;
        }

        [RelayCommand]
        void Delete(string text)
        {
            if (Items.Contains(text))
            {
                Items.Remove(text);
            }
        }

        [RelayCommand]
        async Task Tap(string text)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={text}");
        }
    }
}
