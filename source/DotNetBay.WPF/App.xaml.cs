using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Data.FileStorage;
using DotNetBay.Interfaces;
using DotNetBay.Core.Execution;
using DotNetBay.Model;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public readonly IMainRepository MainRepository;
        public readonly IAuctionRunner AuctionRunner;

        public App()
        {
            this.MainRepository = new FileSystemMainRepository("repo8");
            this.AuctionRunner = new AuctionRunner(this.MainRepository);
            this.FillRepo();
            this.AuctionRunner.Start();

        }

        private void FillRepo()
        {
            var memberService = new SimpleMemberService(this.MainRepository);
            var service = new AuctionService(this.MainRepository, memberService);

            if (service.GetAll().Any())
                return;

            var me = memberService.GetCurrentMember();

            service.Save(new Auction
            {
                Title = "My asdf Auction",
                StartDateTimeUtc = DateTime.UtcNow.AddSeconds(10),
                EndDateTimeUtc = DateTime.UtcNow.AddDays(14),
                StartPrice = 123,
                Seller = me,
                IsRunning = true,
                IsClosed = false
            });

            service.Save(new Auction
            {
                Title = "My Second Auction",
                StartDateTimeUtc = DateTime.UtcNow.AddSeconds(30),
                EndDateTimeUtc = DateTime.UtcNow.AddDays(24),
                StartPrice = 22,
                Seller = me,
                IsRunning = false,
                IsClosed = true
            });
        }

    }
}
