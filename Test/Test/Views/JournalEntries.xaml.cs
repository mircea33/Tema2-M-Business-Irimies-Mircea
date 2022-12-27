using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

namespace Test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JournalEntries : ContentPage
    {

        public JournalEntries()
        {
            InitializeComponent();

        }

        private IEnumerable<JournalEntry> GetJournalEntries(string searchText = null)
        {
            List<JournalEntry> journalentry;
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<JournalEntry>();
                journalentry = conn.Table<JournalEntry>().ToList();
                JournalEntriesList.ItemsSource = journalentry;
            }
            if (String.IsNullOrWhiteSpace(searchText))
                return journalentry;
            return journalentry.Where(c => c.Title.StartsWith(searchText));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            JournalEntriesList.ItemsSource = (System.Collections.IEnumerable)GetJournalEntries();
        }

        protected async void ItemTapped(object sender, ItemTappedEventArgs args)
        {
            var item = args.Item as JournalEntry;
            if (item == null) return;
            await Navigation.PushAsync(new DetailPageJournalEntries(item));
            JournalEntriesList.SelectedItem = null;
        }

        public async void EditClicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var item = (JournalEntry)(mi.CommandParameter);
            await Navigation.PushModalAsync(new EditJournalEntry(item));
        }

        public async void DeleteClicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var item = (JournalEntry)mi.CommandParameter;
            string action;
            action = await DisplayActionSheet("Are you sure you want to delete the selected item ?", "No", "Yes");
            if (action == "Yes")
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.Delete(item);
                };
        }

        private void End_refreshing(object sender, EventArgs e)
        {
            GetJournalEntries();
            JournalEntriesList.ItemsSource = (System.Collections.IEnumerable)GetJournalEntries();
            JournalEntriesList.EndRefresh();
        }

        private async void gotojournaling(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Journaling());
        }

        private async void sendNotification(object sender, EventArgs e)
        {
            DependencyService.Get<INotification>().CreateNotification("Irimies Mircea Proiect", "You received a notification");
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            JournalEntriesList.ItemsSource = (System.Collections.IEnumerable)GetJournalEntries(e.NewTextValue);
        } 
    }
}