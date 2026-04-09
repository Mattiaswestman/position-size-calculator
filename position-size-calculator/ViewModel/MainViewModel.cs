using System.Diagnostics;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using PositionSizeCalculator.Utilities;

namespace PositionSizeCalculator.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public decimal AccountSizeValue
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

        public decimal MaxPositionSizeValue
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

        public decimal EntryPrice
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

        public decimal StopLossPrice
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

        [ObservableProperty]
        private string sharesAmountText;
        [ObservableProperty]
        private string sharesValueText;
        [ObservableProperty]
        private string riskValueText;

        private ExchangeRateProvider exchangeRateProvider;

        private decimal accountSizeValue;
        private decimal maxPositionSizeValue;
        private decimal entryPrice;
        private decimal stopLossPrice;
        private double riskPercentage;

        private decimal riskValue;
        private decimal sharesValue;
        private int sharesAmount;

        public MainViewModel()
        {
            exchangeRateProvider = new ExchangeRateProvider();

            SharesAmountText = "-- shares";
            SharesValueText = "-- SEK";
            RiskValueText = "-- SEK";
        }

        private async Task TryCalculatePositionSize()
        {
            await TryCalculatePositionSizeAsync();
        }

        private async Task TryCalculatePositionSizeAsync()
        {
            if (!AreAllValuesSet())
            {
                return;
            }

            try
            {
                riskValue = accountSizeValue * ((decimal)riskPercentage / 100m);

                decimal entryPriceInSek = await exchangeRateProvider.ConvertAsync(entryPrice, "USD", "SEK");
                decimal stopLossPriceInSek = await exchangeRateProvider.ConvertAsync(stopLossPrice, "USD", "SEK");
                decimal riskPerShare = Math.Abs(entryPriceInSek - stopLossPriceInSek);

                sharesAmount = (int)Math.Floor(riskValue / riskPerShare);

                int maxSharesAmount = (int)Math.Floor(MaxPositionSizeValue / entryPriceInSek);
                if (sharesAmount > maxSharesAmount)
                {
                    sharesAmount = maxSharesAmount;
                }

                sharesValue = Math.Round(sharesAmount * entryPriceInSek, 2);

                SharesAmountText = $"{sharesAmount} shares";
                SharesValueText = $"{sharesValue} SEK";
                RiskValueText = $"{riskValue} SEK";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in TryCalculatePositionSizeAsync: {ex}");
            }
        }

        private bool AreAllValuesSet()
        {
            return (accountSizeValue != 0m && maxPositionSizeValue != 0m && riskPercentage != 0 && entryPrice != 0m && stopLossPrice != 0m);
        }
    }
}
