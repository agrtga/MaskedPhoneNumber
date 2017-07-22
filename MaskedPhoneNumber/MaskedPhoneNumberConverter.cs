using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace MaskedPhoneNumber
{
    /// <summary>
    /// Converts a formatted phone number to the raw digits so that it can be used effectively with a mask.
    /// </summary>
    [ValueConversion(typeof(string), typeof(string))]
    public sealed class MaskedPhoneNumberConverter : MarkupExtension, IValueConverter
    {
        const string USCountryCode = "1";
        const string GermanyCountryCode = "49";

        /// <summary>
        /// Converts a fully formatted phone number to the raw value that is suitable for use with masked edit control.
        /// </summary>
        /// <param name="value">A value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The country code that specifies what formatting is used for the phone number. The country
        /// code is not used by this method.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A string with the formatted name, or a null value if the <i>value</i> type is unsuppported.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string formattedNumber) {
                return StripFormatting(formattedNumber);
            }

            return value;
        }

        /// <summary>
        /// Converts a phone number consisting of the raw digits back into the formatted phone number.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The country code that specifies what formatting is used for the phone 
        /// number. The default value is 1 for the United States.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A value that includes the formatting relevant for the phone number.</remarks>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string countryCode = parameter as string ?? USCountryCode;

            if (value is string rawNumber) {
                return AddFormatting(rawNumber, countryCode);
            }

            return value;
        }

        /// <summary>
        /// Returns an object that is provided as the value of the target property for the
        /// markup extension.
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        /// <returns>The object value to set on the property where the extension is applied.</returns>
        /// <remarks>This method of the MarkupExtension base class allows markup to indicate the converter directly
        /// instead of requiring explicit definition in resources. Users of this converter can specify it using the
        /// markup like the following: Text={Binding NationalNumber, Converter={local:MaskedPhoneNumberConverter}, ConverterParameter=1}</remarks>
        public override object ProvideValue(IServiceProvider serviceProvider)
            => this;

        /// <summary>
        /// Strips all the non-numeric formatting characters from the phone number.
        /// </summary>
        /// <param name="formatted">The formatted phone number.</param>
        /// <returns>A raw phone number format consisting only of digits.</returns>
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

        /// <summary>
        /// Adds formatting to a raw phone number based on the country code.
        /// </summary>
        /// <param name="raw">The raw phone number.</param>
        /// <param name="countryCode">The country code.</param>
        /// <returns>A formatted phone number in the format expected for the country code.</returns>
        static string AddFormatting(string raw, string countryCode)
        {
            var builder = new StringBuilder();

            switch (countryCode) {
                case USCountryCode:
                    FormatNumberForUS(builder, raw);
                    break;

                case GermanyCountryCode:
                    FormatNumberForGermany(builder, raw);
                    break;

                default:
                    builder.Append(raw);
                    break;
            }

            return builder.ToString();
        }

        /// <summary>
        /// Formats a raw phone number for use with the U.S. country code.
        /// </summary>
        /// <param name="builder">The object used to construct the formatted phone number.</param>
        /// <param name="raw">The raw phone number.</param>
        static void FormatNumberForUS(StringBuilder builder, string raw)
        {
            const string Format = "({0}) {1}-{2}";      // (###) ###-####
            const int ExpectedLength = 10;

            if (raw.Length == ExpectedLength) {
                builder.AppendFormat(Format, raw.Substring(0, 3), raw.Substring(3, 3), raw.Substring(6));
            }
            else {
                builder.Append(raw);
            }
        }

        /// <summary>
        /// Formats a raw phone number for use with the German country code.
        /// </summary>
        /// <param name="builder">The object used to construct the formatted phone number.</param>
        /// <param name="raw">The raw phone number.</param>
        static void FormatNumberForGermany(StringBuilder builder, string raw)
        {
            const string Format = "({0}) {1}-{2}";      // (##) ###-#####
            const int ExpectedLength = 10;

            if (raw.Length == ExpectedLength) {
                builder.AppendFormat(Format, raw.Substring(0, 2), raw.Substring(2, 3), raw.Substring(5));
            }
            else {
                builder.Append(raw);
            }
        }
    }
}