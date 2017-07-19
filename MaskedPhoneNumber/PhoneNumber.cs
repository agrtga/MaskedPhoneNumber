using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MaskedPhoneNumber
{
    public sealed class PhoneNumber : INotifyPropertyChanged
    {
        string countryCode, nationalNumber;

        public string CountryCode
        {
            get => countryCode;
            set => SetAndNotify(ref countryCode, value);
        }

        public string NationalNumber
        {
            get => nationalNumber;
            set => SetAndNotify(ref nationalNumber, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void SetAndNotify<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value)) {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }
    }
}
