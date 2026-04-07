using CommunityToolkit.Mvvm.ComponentModel;

namespace PositionSizeCalculator.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
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

        public double MaxPositionSizeValue
        {
            get => maxPositionSizeValue;
            set
            {
                if (value != maxPositionSizeValue)
                {
                    maxPositionSizeValue = value;
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
        private string sharesAmountText;
        [ObservableProperty]
        private string sharesValueText;
        [ObservableProperty]
        private string riskValueText;

        private double accountSizeValue;
        private double maxPositionSizeValue;
        private double riskPercentage;
        private double entryPrice;
        private double stopLossPrice;
        private double riskValue;
        private double sharesValue;
        private int sharesAmount;
        
        public MainViewModel()
        {
            SharesAmountText = "-- shares";
            SharesValueText = "-- SEK";
            RiskValueText = "-- SEK";
        }

        private void TryCalculatePositionSize()
        {
            if (!AreAllValuesSet())
            {
                return;
            }

            riskValue = accountSizeValue * (riskPercentage / 100);
            double riskPerShare = Math.Abs(entryPrice - stopLossPrice);
            sharesAmount = (int)Math.Floor(riskValue / riskPerShare);

            int maxSharesAmount = (int)Math.Floor(MaxPositionSizeValue / entryPrice);
            if (sharesAmount > maxSharesAmount)
            {
                sharesAmount = maxSharesAmount;
            }

            sharesValue = Math.Round(sharesAmount * entryPrice, 2);

            SharesAmountText = $"{sharesAmount} shares";
            SharesValueText = $"{sharesValue} SEK";
            RiskValueText = $"{riskValue} SEK";
        }

        private bool AreAllValuesSet()
        {
            return (accountSizeValue != 0 && maxPositionSizeValue != 0 && riskPercentage != 0 && entryPrice != 0 && stopLossPrice != 0);
        }
    }
}
