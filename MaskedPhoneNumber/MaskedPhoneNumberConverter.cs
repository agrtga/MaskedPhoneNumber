using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace MaskedPhoneNumber
{
    public sealed class MaskedPhoneNumberConverter : MarkupExtension, IValueConverter
    {
        const string USCountryCode = "1";
        const string GermanyCountryCode = "49";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string formattedNumber) {
                return StripFormatting(formattedNumber);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string countryCode = parameter as string ?? USCountryCode;

            if (value is string rawNumber) {
                return AddFormatting(rawNumber, countryCode);
            }

            return value;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
            => this;

        static string StripFormatting(string formatted)
        {
            var builder = new StringBuilder();

            for (int i = 0; i < formatted.Length; i++) {
                if (char.IsNumber(formatted[i])) {
                    builder.Append(formatted[i]);
                }
            }

            return builder.ToString();
        }

        static string AddFormatting(string raw, string countryCode)
        {
            var builder = new StringBuilder();

            switch (countryCode) {
                case USCountryCode:
                    FormatUSPhoneNumber(builder, raw);
                    break;

                case GermanyCountryCode:
                    FormatGermanyPhoneNumber(builder, raw);
                    break;

                default:
                    builder.Append(raw);
                    break;
            }

            return builder.ToString();
        }

        static void FormatUSPhoneNumber(StringBuilder builder, string rawNumber)
        {
            if (rawNumber.Length == 10) {
                builder.AppendFormat("({0}) {1}-{2}", rawNumber.Substring(0, 3), rawNumber.Substring(3, 3), rawNumber.Substring(6));
            }
            else {
                builder.Append(rawNumber);
            }
        }

        static void FormatGermanyPhoneNumber(StringBuilder builder, string rawNumber)
        {
            if (rawNumber.Length == 10) {
                builder.AppendFormat("({0}) {1}-{2}", rawNumber.Substring(0, 2), rawNumber.Substring(2, 3), rawNumber.Substring(5));
            }
            else {
                builder.Append(rawNumber);
            }
        }
    }
}
