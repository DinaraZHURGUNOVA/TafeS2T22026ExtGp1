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
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	public sealed partial class MortgagePage : Page
	{
		public MortgagePage()
		{
			InitializeComponent();
		}
		private void calculateButtonClick(object sender, RoutedEventArgs e)
		{
			// check input
			if (!double.TryParse(loanAmountBox.Text, out double principal) || principal <= 0 ||
				!double.TryParse(interestRateBox.Text, out double annualRate) || annualRate < 0 ||
				!int.TryParse(loanYearsBox.Text, out int years) || years <= 0)
			{
				monthlyResult.Text = "Please enter valid numbers.";
				totalResult.Text = "";
				interestResult.Text = "";
				return;
			}
			int months = years * 12;
			double monthlyRate = annualRate / 100 / 12;
			double payment;
			// M = P * r * (1 + r)^n / ((1 + r)^n - 1)
			if (monthlyRate == 0)
				payment = principal / months;
			else
				payment = principal * monthlyRate * Math.Pow(1 + monthlyRate, months)
						  / (Math.Pow(1 + monthlyRate, months) - 1);
			double totalPaid = payment * months;
			double totalInterest = totalPaid - principal;
			monthlyResult.Text = "Monthly repayment: $" + payment.ToString("N2");
			totalResult.Text = "Total paid: $" + totalPaid.ToString("N2");
			interestResult.Text = "Total interest: $" + totalInterest.ToString("N2");
		}
		private void backButtonClick(object sender, RoutedEventArgs e)
		{
			if (Frame.CanGoBack)
				Frame.GoBack();
		}
	}
}