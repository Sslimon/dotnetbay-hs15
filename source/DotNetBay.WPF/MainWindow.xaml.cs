using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DotNetBay.Core;
using DotNetBay.Model;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly ObservableCollection<Auction> auctions = new ObservableCollection<Auction>();

        public ObservableCollection<Auction> Auctions
        {
            get { return this.auctions; }
        }

        public MainWindow()
        {
            if ((Application.Current as App) != null)
            {
                var service = new AuctionService(((App)Application.Current).MainRepository, new SimpleMemberService(((App)Application.Current).MainRepository));
                foreach (var auction in service.GetAll())
                {
                    this.Auctions.Add(auction);
                }
            }
            this.DataContext = this;
            this.InitializeComponent();
        }

        public void OpenSellView(object sender, RoutedEventArgs e)
        {
            var sellView = new SellView();
            sellView.ShowDialog();
        }

        public void PlaceBid(object sender, RoutedEventArgs e)
        {
            
        }

    }

    public class BooleanToStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "Valid" : "Closed";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
