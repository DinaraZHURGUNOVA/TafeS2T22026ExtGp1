using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class Currency : Page
	{
		public Currency()
		{
			this.InitializeComponent();
		}
		// USD exchange rates
		private const decimal USD_TO_EUR = 0.85189982m;
		private const decimal USD_TO_GBP = 0.72872436m;
		private const decimal USD_TO_INR = 74.257327m;

		// EUR exchange rates
		private const decimal EUR_TO_USD = 1.1739732m;
		private const decimal EUR_TO_GBP = 0.8556672m;
		private const decimal EUR_TO_INR = 87.00755m;

		// GBP exchange rates
		private const decimal GBP_TO_USD = 1.371907m;
		private const decimal GBP_TO_EUR = 1.1686692m;
		private const decimal GBP_TO_INR = 101.68635m;

		// INR exchange rates
		private const decimal INR_TO_USD = 0.011492628m;
		private const decimal INR_TO_EUR = 0.013492774m;
		private const decimal INR_TO_GBP = 0.0098339397m;

		private decimal GetExchangeRate(int fromIndex, int toIndex)
		{
			// Same currency: no conversion required
			if (fromIndex == toIndex)
				return 1m;

			// From USD
			if (fromIndex == 0)
			{
				if (toIndex == 1) return USD_TO_EUR;
				if (toIndex == 2) return USD_TO_GBP;
				if (toIndex == 3) return USD_TO_INR;
			}

			// From EUR
			if (fromIndex == 1)
			{
				if (toIndex == 0) return EUR_TO_USD;
				if (toIndex == 2) return EUR_TO_GBP;
				if (toIndex == 3) return EUR_TO_INR;
			}

			// From GBP
			if (fromIndex == 2)
			{
				if (toIndex == 0) return GBP_TO_USD;
				if (toIndex == 1) return GBP_TO_EUR;
				if (toIndex == 3) return GBP_TO_INR;
			}

			// From INR
			if (fromIndex == 3)
			{
				if (toIndex == 0) return INR_TO_USD;
				if (toIndex == 1) return INR_TO_EUR;
				if (toIndex == 2) return INR_TO_GBP;
			}

			return 0m;
		}
		private void currencyCalcButtonClick(object sender, RoutedEventArgs e)
		{
			// Check whether an amount has been entered
			if (string.IsNullOrWhiteSpace(exchangeAmountTextBox.Text))
			{
				return;
			}

			// Validate the amount
			if (!decimal.TryParse(exchangeAmountTextBox.Text, out decimal amount)
				|| amount <= 0)
			{
				return;
			}

			// Get selected currency indexes
			int fromIndex = currencyFromComboBox.SelectedIndex;
			int toIndex = currencyToComboBox.SelectedIndex;

			// Check currency selections
			if (fromIndex < 0 || toIndex < 0)
			{
				return;
			}

			// Get exchange rate
			decimal exchangeRate = GetExchangeRate(fromIndex, toIndex);

			// Calculate converted amount
			decimal convertedAmount = amount * exchangeRate;

			// Display original amount
			fromAmountAndCurrencyTextBlock.Text =
				$"{amount:N2} {GetCurrencyCode(fromIndex)}";

			// Display converted amount
			toAmountAndCurrencyTextBlock.Text =
				$"{convertedAmount:N2} {GetCurrencyCode(toIndex)}";

			// Display forward exchange rate
			exchangeRateTextBlock.Text =
				$"1 {GetCurrencyCode(fromIndex)} = {exchangeRate} {GetCurrencyCode(toIndex)}";

			// Display reverse exchange rate
			decimal reverseRate = GetExchangeRate(toIndex, fromIndex);

			flipExchangeRateTextBlock.Text =
				$"1 {GetCurrencyCode(toIndex)} = {reverseRate} {GetCurrencyCode(fromIndex)}";
		}
		private string GetCurrencyCode(int index)
		{
			switch (index)
			{
				case 0:
					return "USD";
				case 1:
					return "EUR";
				case 2:
					return "GBP";
				case 3:
					return "INR";
				default:
					return "";
			}
		}

	}
}
