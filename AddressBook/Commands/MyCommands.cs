using AddressBook.Models;
using AddressBook.Viewmodels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.ObjectiveC;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AddressBook.Commands
{
    public class MyCommands
    {
        private readonly EventAggregator _eventAggregator;
        public MyCommands(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            OpenProfileCommand = new RelayCommand(ExecuteOpenProfile);
            SaveProfileChangesCommand = new RelayCommand(ExecuteSaveProfileChanges);
            CreateNewProfileCommand = new RelayCommand(ExecuteCreateNewProfileCommand);
            DeleteProfileCommand = new RelayCommand(ExecuteDeleteProfileCommand);
            
        }
        public RelayCommand  OpenProfileCommand { get; set; }
        private void ExecuteOpenProfile(object parameter)
        {

            int p_Id = (int)parameter;
            var window = new ProfileWindow(p_Id);
            window.Show();
        }

        public RelayCommand SaveProfileChangesCommand { get; set; }
        private void ExecuteSaveProfileChanges(object parameter)
        {
            Data data = (Data)parameter;
            data.ExportData();
            _eventAggregator.Publish(new JsonChangedMessage());
        }

        public RelayCommand CreateNewProfileCommand { get; set; } 
        private void ExecuteCreateNewProfileCommand(object parameter)
        {
            int p_Id = -1;
            var window = new ProfileWindow(p_Id);
            window.Show();

        }

        public RelayCommand DeleteProfileCommand { get; set; }
        private void ExecuteDeleteProfileCommand(object parameter)
        {
            int _parameter = (int)parameter;
            Data.DeleteProfile(_parameter);
            _eventAggregator.Publish(new JsonChangedMessage());
        }
    }
}