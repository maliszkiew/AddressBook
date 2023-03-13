using System.Windows;
using AddressBook.Viewmodels;
using MahApps.Metro.Controls;

namespace AddressBook
{
    public partial class ProfileWindow : MetroWindow
    {
        public ProfileWindow(int pId)  
        {
            this.DataContext = new ProfileWindowViewModel(pId, ((App)Application.Current).EventAggregator);
            InitializeComponent();
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
