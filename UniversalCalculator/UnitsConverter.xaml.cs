using System;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Calculator
{
	public sealed partial class UnitsConverter : Page
	{
		public UnitsConverter()
		{
			InitializeComponent();

			ConvertButton.Click += ConvertButton_Click;
			ClearButton.Click += ClearButton_Click;

			// Clear old results when the input or conversion changes.
			ValueBox.TextChanged += (sender, args) => ResetResult();
			ConversionBox.SelectionChanged += (sender, args) => ResetResult();
		}

		private void ResetResult()
		{
			ResultText.Text = "Your result will appear here.";
			ErrorText.Text = "";
		}

		private void ConvertButton_Click(object sender, RoutedEventArgs e)
		{
			ResetResult();

			double value;

			if (!double.TryParse(
					ValueBox.Text,
					NumberStyles.Float,
					CultureInfo.CurrentCulture,
					out value)
				|| double.IsNaN(value)
				|| double.IsInfinity(value))
			{
				ErrorText.Text = "Please enter a valid number.";
				return;
			}

			double result;
			string unit;

			switch (ConversionBox.SelectedIndex)
			{
				case 0:
					result = value * 1.8 + 32;
					unit = "°F";
					break;

				case 1:
					result = (value - 32) / 1.8;
					unit = "°C";
					break;

				case 2:
					result = value / 0.3048;
					unit = "ft";
					break;

				case 3:
					result = value * 0.3048;
					unit = "m";
					break;

				case 4:
					result = value / 0.45359237;
					unit = "lb";
					break;

				case 5:
					result = value * 0.45359237;
					unit = "kg";
					break;

				case 6:
					result = value / 6.89475729;
					unit = "PSI";
					break;

				case 7:
					result = value * 6.89475729;
					unit = "kPa";
					break;

				default:
					ErrorText.Text = "Please select a conversion.";
					return;
			}

			if (double.IsNaN(result) || double.IsInfinity(result))
			{
				ErrorText.Text = "This value is too large to convert.";
				return;
			}

			ResultText.Text =
		   result.ToString("G10", CultureInfo.CurrentCulture)
		   + " " + unit;
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Exit();
		}

		private void ClearButton_Click(object sender, RoutedEventArgs e)
		{
			ValueBox.Text = "";
			ResetResult();
			ValueBox.Focus(FocusState.Programmatic);
		}
	}
}