using AddressBook.Commands;
using AddressBook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AddressBook.Viewmodels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // INotifyPropertyChanged interface
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //
        private readonly EventAggregator _eventAggregator; //EventAggreagotor interface
        private void OnJsonChanged(JsonChangedMessage message)
        {
            VMImportedData = new Data();
            FilteredData = _VMImportedData.importedProfiles;
        }

        public MyCommands Commands { get; set; }
        public MainViewModel(EventAggregator eventAggregator)
        {
            _VMImportedData = new Data();
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe<JsonChangedMessage>(OnJsonChanged);
            FilteredData = _VMImportedData.importedProfiles;
            Commands = new MyCommands(eventAggregator);
            Data.GarbageCleaner();

        }

        private Data? _VMImportedData;
        public Data? VMImportedData
        {
            get => _VMImportedData;
            set
            {
                _VMImportedData = value;
                OnPropertyChanged(nameof(VMImportedData));
            }
        }

        private List<ContactProfile>? _filteredData;
        public List<ContactProfile>? FilteredData
        {
            get => _filteredData;
            set
            {
                _filteredData = value;
                OnPropertyChanged(nameof(FilteredData));
            }
        }

        private string? _searchText;
        public string? SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                Search();
                OnPropertyChanged(nameof(SearchText));

            }
        }

        private void Search()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                FilteredData = VMImportedData.importedProfiles.Where(p => p.lastName.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0 || p.firstName.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
                Data.sortData(FilteredData);
            }
            else
            {
                FilteredData = VMImportedData.importedProfiles;
            }
            OnPropertyChanged(nameof(VMImportedData));
        }
    }
}
