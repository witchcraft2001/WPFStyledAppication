using StyledApp.Infrastructure;
using StyledApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StyledApp.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private IClosableView view;

        private UserModel model;

        public UserModel Model
        {
            get { return model; }
            set
            {
                model = value;
                OnPropertyChanged("Model");
            }
        }

        public ICommand ApplyCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public UserViewModel(IClosableView view)
        {
            this.view = view;

            ApplyCommand = new RelayCommand(DoApply, param => !Model.HasError);
            CancelCommand = new RelayCommand(DoCancel);

            Model = new UserModel()
            {
                FirstName = "Юрий",
                LastName = "Матросов"
            };
        }
        
        private void DoApply(Object obj)
        {
            MessageBox.Show("Учетная запись сохранена");
        }

        private void DoCancel(Object obj)
        {
            if (MessageBox.Show("Данные не сохранены, завершить работу?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                view.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
