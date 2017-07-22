using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MaskedPhoneNumber
{
    /// <summary>
    /// Represents a phone number.
    /// </summary>
    public sealed class PhoneNumber : INotifyPropertyChanged
    {
        string countryCode, nationalNumber;

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>A string value that is expected but not enforced to be numeric.</value>
        public string CountryCode
        {
            get => countryCode;
            set => SetAndNotify(ref countryCode, value);
        }

        /// <summary>
        /// Gets or sets the national number portion of the phone number.
        /// </summary>
        /// <value>A string value that includes the number and country code specific formatting.</value>
        public string NationalNumber
        {
            get => nationalNumber;
            set => SetAndNotify(ref nationalNumber, value);
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that was changed.</param>
        void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets the field and notifies listeners if the property value is changed.
        /// </summary>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <param name="field">The field to be changed.</param>
        /// <param name="value">The proposed value for the property.</param>
        /// <param name="propertyName">The name of the property being changed.</param>
        void SetAndNotify<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value)) {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }
    }
}
