using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditJournalEntry : ContentPage
    {
        JournalEntry Item;
        public EditJournalEntry(JournalEntry item)
        {
            InitializeComponent();
            Item = item;
            this.BindingContext = item;
        }
        private async void Save_Clicked1(object sender, EventArgs e)
        {
            string Query = "update JournalEntry set Title = '" + TitleEntry1.Text + "',Writings='" + WritingsEditor1.Text + "' where Id='" + Item.Id + "';";
            SQLiteConnection conn = new SQLiteConnection(App.FilePath);
            SQLiteCommand command = new SQLiteCommand(conn);
            command.CommandText = Query;
            command.ExecuteNonQuery();
            await Navigation.PopModalAsync();
        }
    }
}