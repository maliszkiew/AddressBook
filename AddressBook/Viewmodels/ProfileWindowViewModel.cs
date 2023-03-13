using AddressBook.Commands;
using AddressBook.Models;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace AddressBook.Viewmodels
{
    public class ProfileWindowViewModel : INotifyPropertyChanged
    {
        //INotifyPropertyChanged interface
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //
        private readonly EventAggregator _eventAggregator; //EventAggreagotor interface 
        public Data? ProfileVMImportedData { get; set; }
        public MyCommands Commands { get; set; }
        private bool _isNewProfile { get; set; }
        public ProfileWindowViewModel(int pId, EventAggregator eventAggregator)
        {
            ChangeImageCommand = new RelayCommand(ExecuteChangeImageCommand);
            Commands = new MyCommands(eventAggregator);
            ProfileVMImportedData = new Data();
            if (pId != -1) //pId = -1 when the profile doesn't exist yet
            {
                _isNewProfile = false;
                _profile = ProfileVMImportedData.importedProfiles.FirstOrDefault(p => p.Id == pId);
                ID = pId;
            }
            else
            {
                _isNewProfile = true;
                _profile = new ContactProfile();
                ID = ProfileVMImportedData.importedProfiles.Last().Id + 1;
            }
            _imagePath = _profile.imagePath;
            if (string.IsNullOrEmpty(_imagePath) || !File.Exists(_imagePath)) _imagePath = Constants.IMAGE_PLACEHOLDER;
            _firstName = _profile.firstName;
            _lastName = _profile.lastName;
            _phoneNumber = _profile.phoneNumber;
            _address = _profile.address;
            _eMail = _profile.email;
            _group = _profile.group;
            if (_isNewProfile) ProfileVMImportedData.importedProfiles.Add(_profile);
        }

        private string? _imagePath;
        public string? ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                Profile.imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        private ContactProfile? _profile;
        public ContactProfile? Profile
        {
            get => _profile;
            set
            {
                _profile = value;
                OnPropertyChanged(nameof(Profile));
            }
        }

        private int _id;
        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                _profile.Id = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                _profile.firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                _profile.lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                _profile.phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        private string _eMail;
        public string EMail
        {
            get => _eMail;
            set
            {
                _eMail = value;
                _profile.email = value;
                OnPropertyChanged(nameof(EMail));
            }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                _profile.address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private string _group;
        public string Group
        {
            get => _group;
            set
            {
                _group = value;
                Profile.group = value;
                OnPropertyChanged(nameof(Group));
            }
        }
        public RelayCommand ChangeImageCommand { get; set; }
        private void ExecuteChangeImageCommand(object parameter)
        {
            var fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                var Image = new BitmapImage(new Uri(fileDialog.FileName));
                using (var fileStream = new FileStream(Constants.IMAGE_PATH + _id + ".jpeg", FileMode.Create))
                {
                    var encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(Image));
                    encoder.Save(fileStream);
                }
                ImagePath = Constants.IMAGE_PATH + _id + ".jpeg";

            }
        }
    }
}
