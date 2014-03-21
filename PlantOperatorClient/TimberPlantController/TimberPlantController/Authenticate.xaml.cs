using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TimberPlantController
{
    /// <summary>
    /// Interaction logic for Authenticate.xaml
    /// </summary>
    public partial class Authenticate : Window
    {
        private readonly AuthenticateViewModel authenticateViewModel = new AuthenticateViewModel();

        public Authenticate()
        {
            InitializeComponent();
            this.DataContext = authenticateViewModel;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var selectedUser = this.authenticateViewModel.SelectedUserType;
            var password = this.authenticateViewModel.Password;
            
            this.DialogResult = Validate(selectedUser, password);

            this.Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private bool Validate(string userType, string pasword)
        {
            if (userType == "User" && pasword == "thirdeye")
                return true;

            if (userType == "Super User" && pasword == "thirdeye")
                return true;

            return false;
        } 
    }

    public class AuthenticateViewModel: INotifyPropertyChanged
    {
        private string _password = string.Empty;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                this.OnNotifyPropertyChanged("Password");
            }
        }

        private List<string> _userTypes = new List<string>(){"User","Super User"};
        public List<string> UserTypes
        {
            get { return _userTypes; }
            set
            {
                _userTypes = value;
                this.OnNotifyPropertyChanged("UserTypes");
            }
        }

        private string _selectedUserType = "User";
        public string SelectedUserType
        {
            get { return _selectedUserType; }
            set
            {
                _selectedUserType = value;
                this.OnNotifyPropertyChanged("SelectedUserType");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnNotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
