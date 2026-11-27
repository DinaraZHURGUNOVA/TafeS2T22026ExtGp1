using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Calculator
{
	public sealed partial class MainMenu : Page
	{
		public MainMenu()
		{
			InitializeComponent();
		}
	

	private void mathButtonClick(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainPage));
		}
		private void mortgageButtonClick(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MortgagePage));
		}
		private void currencyButtonClick(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(Currency));
		}
		private void unitsButtonClick(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(UnitsConverter));
		}
		private void exitButtonClick(object sender, RoutedEventArgs e)
		{
			Application.Current.Exit();
		}
	}
}
