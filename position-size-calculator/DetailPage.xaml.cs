using PositionSizeCalculator.ViewModel;

namespace PositionSizeCalculator
{
    public partial class DetailPage : ContentPage
    {
        public DetailPage(DetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
