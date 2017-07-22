using System.Windows;

namespace MaskedPhoneNumber
{
    /// <summary>
    /// Displays the example form to show the phone number mask.
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly PhoneNumber phoneNumber;

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            phoneNumber = new PhoneNumber() { CountryCode = "1" };
            DataContext = phoneNumber;
        }
    }
}
