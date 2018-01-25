using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StyledApp.Models
{
    public class UserModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private string firstName;
        private string lastName;
        private string middleName;

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
                OnPropertyChanged("Errors");
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
                OnPropertyChanged("Errors");
            }
        }

        public string MiddleName
        {
            get { return middleName; }
            set
            {
                middleName = value;
                OnPropertyChanged("MiddleName");
                OnPropertyChanged("Errors");
            }
        }

        #region IDataErrorInfo
        public string this[string columnName] {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "FirstName":
                        if (string.IsNullOrWhiteSpace(FirstName))
                        {
                            error = "Укажите имя";
                        }
                        break;
                    case "LastName":
                        if (string.IsNullOrWhiteSpace(LastName))
                        {
                            error = "Укажите фамилию";
                        }
                        break;
                    case "MiddleName":
                        if (string.IsNullOrWhiteSpace(MiddleName))
                        {
                            error = "Укажите отчество";
                        }
                        break;
                }
                return error;
            }
        }
        
        public string Error
        {
            get
            {
                if (!HasError) return string.Empty;
                StringBuilder errors = new StringBuilder();
                foreach (string error in Errors)
                {
                    errors.AppendLine(error);
                }
                return errors.ToString();
            }
        }

        private ObservableCollection<string> errors = null;
        public virtual ObservableCollection<string> Errors
        {
            get
            {
                errors = new ObservableCollection<string>();
                string error = this["FirstName"];
                if (!string.IsNullOrEmpty(error))
                {
                    errors.Add(error);
                }
                error = this["LastName"];
                if (!string.IsNullOrEmpty(error))
                {
                    errors.Add(error);
                }
                error = this["MiddleName"];
                if (!string.IsNullOrEmpty(error))
                {
                    errors.Add(error);
                }
                return errors;
            }
        }

        public virtual bool HasError
        {
            get { return Errors != null && Errors.Count > 0; }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        } 
        #endregion
    }
}
