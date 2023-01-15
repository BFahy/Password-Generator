using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;

namespace Password_Generator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static readonly string charLower = "abcdefghijklmnopqrstuvwxyz", 
            charUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 
            charSymbol = "!@#$%^&*()_+-=./?", 
            charNumber = "1234567890";

        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void pwLength_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int val = Convert.ToInt32(e.NewValue);
            string output = ($"Password Length: {val}");
            this.pwLengthTxt.Text = output;
            this.pwOutput.Text = CreateKey(val).ToString();
        }

        private string CreateKey(int pwLength)
        {
            char[] charOptions = pwSelections().ToCharArray();

            byte[] data = new byte[4 * pwLength];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder output = new StringBuilder(pwLength);
            for (int i = 0; i < pwLength; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % charOptions.Length;
                output.Append(charOptions[idx]);
            }
            return output.ToString();
        }

        private void pwGenerate_Click(object sender, RoutedEventArgs e)
        {
            this.pwOutput.Text = CreateKey(Convert.ToInt32(pwLength.Value)).ToString();
        }

        private string pwSelections()
        {
            string genOptions = "";

            if (pwUpper.IsChecked == true) genOptions += charUpper;
            if (pwLower.IsChecked == true) genOptions += charLower;
            if (pwSymbol.IsChecked == true) genOptions += charSymbol;
            if (pwNumber.IsChecked == true) genOptions += charNumber;

            return genOptions;
        }

        #region Checkbox Validation
        private void pwSymbol_Change(object sender, RoutedEventArgs e)
        {
            pwCheckboxValidate();
            this.pwOutput.Text = CreateKey(Convert.ToInt32(pwLength.Value)).ToString();
        }
        private void pwLower_Change(object sender, RoutedEventArgs e)
        {
            pwCheckboxValidate();
            this.pwOutput.Text = CreateKey(Convert.ToInt32(pwLength.Value)).ToString();
        }

        private void pwCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(pwOutput.Text);
        }

        private void pwNumber_Change(object sender, RoutedEventArgs e)
        {
            pwCheckboxValidate();
            this.pwOutput.Text = CreateKey(Convert.ToInt32(pwLength.Value)).ToString();
        }

        private void pwUpper_Change(object sender, RoutedEventArgs e)
        {
            pwCheckboxValidate();
            this.pwOutput.Text = CreateKey(Convert.ToInt32(pwLength.Value)).ToString();
        }

        /**
         * Checking if at least 1 checkbox is enabled, else disabling last option selected
         */
        private void pwCheckboxValidate()
        {
            if (pwUpper.IsChecked == false && pwSymbol.IsChecked == false && pwNumber.IsChecked == false)
            {
                pwLower.IsHitTestVisible = false;
            }
            else if (pwLower.IsChecked == false && pwSymbol.IsChecked == false && pwNumber.IsChecked == false)
            {
                pwUpper.IsHitTestVisible = false;
            }
            else if (pwLower.IsChecked == false && pwUpper.IsChecked == false && pwNumber.IsChecked == false)
            {
                pwSymbol.IsHitTestVisible = false;
            }
            else if (pwLower.IsChecked == false && pwUpper.IsChecked == false && pwSymbol.IsChecked == false)
            {
                pwNumber.IsHitTestVisible = false;
            }
            else
            {
                pwNumber.IsHitTestVisible = true;
                pwUpper.IsHitTestVisible = true;
                pwLower.IsHitTestVisible = true;
                pwSymbol.IsHitTestVisible = true;
            }
        }
        #endregion
    }
}
