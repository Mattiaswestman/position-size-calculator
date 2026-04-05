using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Graphics;

namespace PositionSizeCalculator.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public Color LongButtonColor => IsLong ? GetStaticResourceColor("Green") : GetStaticResourceColor("ForegroundColor");
        public Color ShortButtonColor => !IsLong ? GetStaticResourceColor("Red") : GetStaticResourceColor("ForegroundColor");

        public double AccountSizeValue
        {
            get => accountSizeValue;
            set
            {
                if(value != accountSizeValue)
                {
                    accountSizeValue = value;
                    TryCalculatePositionSize();
                }
            }
        }

        public double RiskPercentage
        {
            get => riskPercentage;
            set
            {
                if (value != riskPercentage)
                {
                    riskPercentage = value;
                    TryCalculatePositionSize();
                }
            }
        }

        public double EntryPrice
        {
            get => entryPrice;
            set
            {
                if (value != entryPrice)
                {
                    entryPrice = value;
                    TryCalculatePositionSize();
                }
            }
        }

        public double StopLossPrice
        {
            get => stopLossPrice;
            set
            {
                if (value != stopLossPrice)
                {
                    stopLossPrice = value;
                    TryCalculatePositionSize();
                }
            }
        }

        [ObservableProperty]
        private int sharesAmount;
        [ObservableProperty]
        private double sharesValue;
        [ObservableProperty]
        private double riskValue;
        [ObservableProperty]
        private bool isLong = true;

        private double accountSizeValue;
        private double riskPercentage;
        private double entryPrice;
        private double stopLossPrice;
        
        public MainViewModel()
        {
        }

        [RelayCommand]
        private void ToggleLong()
        {
            if (!IsLong)
            {
                IsLong = true;
            }
        }

        [RelayCommand]
        private void ToggleShort()
        {
            if (IsLong)
            {
                IsLong = false;
            }
        }

        private void TryCalculatePositionSize()
        {
            if (!AreAllValuesSet())
            {
                return;
            }

            RiskValue = accountSizeValue * (riskPercentage / 100);
            double riskPerShare = entryPrice - stopLossPrice;
            SharesAmount = (int)(RiskValue / riskPerShare);
            SharesValue = SharesAmount * entryPrice;
            Math.Round(SharesValue, 2);
        }

        private static Color GetStaticResourceColor(string key)
        {
            if (Application.Current.Resources.TryGetValue(key, out var value) && value is Color color)
            {
                return color;
            }
            return Colors.Transparent;
        }

        private bool AreAllValuesSet()
        {
            return (accountSizeValue != 0 && riskPercentage != 0 && entryPrice != 0 && stopLossPrice != 0);
        }

        partial void OnIsLongChanged(bool value)
        {
            OnPropertyChanged(nameof(LongButtonColor));
            OnPropertyChanged(nameof(ShortButtonColor));
        }
    }
}
