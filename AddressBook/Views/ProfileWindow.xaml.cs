using System.IO;
using System.Threading;
using System.Windows;
using AddressBook.Models;
using AddressBook.Viewmodels;
using MahApps.Metro.Controls;

namespace AddressBook
{
    public partial class ProfileWindow : MetroWindow
    {
        int _id { get; set; }
        public ProfileWindow(int pId)
        {
            this.DataContext = new ProfileWindowViewModel(pId, ((App)Application.Current).EventAggregator);
            _id = pId;
            InitializeComponent();
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
