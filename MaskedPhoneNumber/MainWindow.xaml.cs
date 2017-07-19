using System.Windows;

namespace MaskedPhoneNumber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly PhoneNumber phoneNumber;

        public MainWindow()
        {
            InitializeComponent();

            phoneNumber = new PhoneNumber() { CountryCode = "1" };
            DataContext = phoneNumber;
        }
    }
}
