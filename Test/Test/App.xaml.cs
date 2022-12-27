using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.IO;
using Test.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace Test
{
    public partial class App : Application
    {
        public static string FilePath;
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new JournalEntries());
        }
        public App(string filePath)
        {
            InitializeComponent();
            MainPage = new NavigationPage(new JournalEntries());
            FilePath = filePath;
        }
        protected override void OnStart()
        {

        }
        protected override void OnSleep()
        {

        }
        protected override void OnResume()
        {

        }
    }
}
