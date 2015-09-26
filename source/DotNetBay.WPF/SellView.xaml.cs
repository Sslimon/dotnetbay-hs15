using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using Microsoft.Win32;
using Xceed.Wpf.DataGrid.Converters;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class SellView : Window
    {

        private ObservableCollection<Auction> Auctions;

        public SellView(ObservableCollection<Auction> auctions)
        {
            this.InitializeComponent();
            this.Auctions = auctions;
        }

        public void AddAuction(object sender, RoutedEventArgs e)
        {
            var repo = ((App) Application.Current).MainRepository;
            var memberService = new SimpleMemberService(repo);
            var service = new AuctionService(repo, memberService);

            var title = this.Title.Text;
            var description = this.Description.Text;
            var startdate = DateTime.Parse(this.Start.Text);
            var enddate = DateTime.Parse(this.End.Text);
            var price = double.Parse(this.StartPrice.Text);
            var member = memberService.GetCurrentMember();
            var image = this.Image.Text;
            var imageArr = File.ReadAllBytes(image);

            var auction = new Auction
            {
                Title = title,
                StartDateTimeUtc = startdate,
                EndDateTimeUtc = enddate,
                StartPrice = price,
                Seller = member,
                Image = imageArr,
                Description = description
            };

            service.Save(auction);
            this.Auctions.Add(auction);
            this.Cancel(sender, e);
        }

        public void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SelectImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                this.Image.Text = dialog.FileName;
            }
        }
    }
}
