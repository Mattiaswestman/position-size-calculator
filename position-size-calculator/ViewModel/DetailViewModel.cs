using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PositionSizeCalculator.ViewModel
{
    [QueryProperty("Text", "Text")]
    public partial class DetailViewModel : ObservableObject
    {
        [ObservableProperty]
        private string text;

        [RelayCommand]
        async Task Return()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
