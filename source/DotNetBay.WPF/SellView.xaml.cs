using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using DotNetBay.Core;
using DotNetBay.Model;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class SellView : Window
    {
        public SellView()
        {
            this.InitializeComponent();
        }

        public void AddAuction(object sender, RoutedEventArgs e)
        {
            var repo = ((App) Application.Current).MainRepository;
            var memberService = new SimpleMemberService(repo);
            var service = new AuctionService(repo, memberService);

            var title = this.Title.Text;
            var startdate = DateTime.Parse(this.Start.Text);
            var enddate = DateTime.Parse(this.End.Text);
            var price = double.Parse(this.StartPrice.Text);
            var member = memberService.GetCurrentMember();

            var auction = new Auction
            {
                Title = title,
                StartDateTimeUtc = startdate,
                EndDateTimeUtc = enddate,
                StartPrice = price,
                Seller = member
            };

            service.Save(auction);
            this.Cancel(sender, e);
        }

        public void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
