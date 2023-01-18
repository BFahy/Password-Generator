using System;
using System.Text;
using System.Windows;
using System.Windows.Media;
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
        bool passMode = false;
        internal static readonly string[] words =
{
    "trains","utopian","extra-large","sable","frail","lush","direction","remove","comfortable","observe","mailbox","ceaseless","enchanting","wonderful","arch","annoy","callous","haunt","meddle","fire","sassy","slimy","capricious","unwritten","doctor","rhyme","prepare","blot","peep","gainful","wind","delight","settle","torpid","soak","whisper","stick","long-term","paint","holistic","launch","pop","chicken","stream","nine","reward","illegal","late","friends","sheet","ragged","yell","woebegone","respect","jazzy","toes","program","peaceful","bite-sized","quince","legs","transport","depend","bit","hill","attraction","bizarre","dad","turkey","succeed","simple","mute","cruel","quilt","aquatic","guitar","numberless","knotty","rabid","mountain","robin","defective","industrious","governor","tin","befitting","wrist","water","pipe","selection","supply","snow","eatable","stone","wooden","productive","monkey","zesty","servant","verdant","lumber","daughter","feeling","smiling","reject","delirious","sloppy","experience","society","omniscient","quack","sink","rough","delightful","salt","porter","stroke","wheel","supreme","development","acoustics","bear","guess","excellent","key","minister","rice","value","celery","unfasten","jaded","even","comparison","identify","dogs","wakeful","violent","powder","owe","secretive","tray","baby","bed","plate","insect","strip","stimulating","nervous","absurd","voiceless","interest","pin","giants","sense","minor","soup","plug","rescue","ill-informed","tooth","empty","vegetable","creator","question","wanting","growth","muddle","explain","mend","cloudy","shut","ground","flat","boundary","influence","chalk","discussion","jar","gabby","wonder","mitten","car","cagey","rely","plantation","sneaky","measly","mighty","shallow","interfere","highfalutin","possess","wander","elbow","lamp","crowded","murky","breezy","complain","guiltless","pause","judge","abortive","abiding","polish","colorful","fall","redundant","thing","religion","steam","gamy","puncture","resolute","relieved","ugliest","quarrelsome","knot","adjoining","mere","repair","clumsy","giraffe","ban","collar","trip","rigid","vanish","grip","school","boil","red","diligent","wood","grain","wave","duck","rainstorm","equable","pies","carve","suspend","mist","drip","drink","ask","nimble","living","short","watch","unnatural","underwear","tomatoes","example","mushy","insurance","amuck","behavior","jelly","alike","fade","clam","flimsy","cup","synonymous","wide-eyed","quirky","exchange","tasteless","juvenile","repeat","market","pencil","tough","glove","plant","lumpy","cats","stingy","complex","discreet","abusive","testy","questionable","straw","young","produce","deer","look","ugly","impolite","film","morning","title","use","rebel","rot","lopsided","throne","separate","whistle","suppose","three","frequent","didactic","toothbrush","combative","yarn","cuddly","puzzled","muscle","front","tested","billowy","obsolete","grass","prose","amuse","march","tearful","detail","like","happy","travel","beg","royal","cover","scratch","skinny","willing","dirt","thankful","happen","well-off","cheerful","retire","yummy","enchanted","card","majestic","fuel","earthquake","tree","two","heavy","educate","purple","garrulous","helpless","earth","place","sharp","accidental","sea","mug","functional","return","broken","lethal","imperfect","sofa","bridge","beds","birth","deserve","way","kind","drop","overt","money","wacky","frighten","perfect","stomach","superb","selective","wrestle","x-ray","exultant","squalid","airport","decorate","available","macho","fail","judicious","provide","drown","women","pretend","harass","beautiful","hook","education","silly","envious","mix","hang","stranger","wrap","credit","skin","apologise","equal","cactus","sock","vest","sign","narrow","addition","call","worthless","cent","well-groomed","hollow","annoying","amusement","threatening","crazy","sound","likeable","hungry","flowers","vase","overconfident","fairies","childlike","report","notice","superficial","bone","hot","selfish","tow","aunt","surprise","placid","price","waiting","minute","trees","spot","whip","internal","double","passenger","unlock","lake","boast","limping","pat","ocean","sort","division","button","cast","paddle","sneeze","branch","dinosaurs","addicted","manage","woman","hand","furtive","aberrant","chin","heap","hulking","knowing","chivalrous","harm","mind","drawer","important","shiny","angry","endurable","unused","copper","snake","guard","seed","describe","seat","ill-fated","brush","thaw","jumpy","bolt","arm","tenuous","month","wry","lazy","historical","recognise","uncle","spoil","humorous","army","square","donkey","force"
};

        public MainWindow()
        {
            InitializeComponent();
        }

        private void pwLength_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int val = Convert.ToInt32(e.NewValue);
            string output = ($"Password Length: {val}");
            this.pwLengthTxt.Text = output;
            this.pwOutput.Text = GenerateRandomCharacters(val).ToString();
        }

        private void ppLength_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int val = Convert.ToInt32(e.NewValue);
            string output = ($"Passphrase Length: {val}");
            this.pwLengthTxt.Text = output;
            this.pwOutput.Text = GenerateRandomWords(val).ToString();
        }

        private string GenerateRandomCharacters(int pwLength)
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

        private string GenerateRandomWords(int pwLength)
        {       
            byte[] data = new byte[4 * pwLength]; // Change 5 to word count            
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);                
            }
            string output = "";
            for (int i = 0; i < pwLength; i++) // Change 5 to word count
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % words.Length;
                if (ppCapital.IsChecked == true && ppNumber.IsChecked == true)
                {
                    var endNum = (rnd % 10) + 1;
                    output += CapitaliseLetter(words[idx]) + endNum.ToString() + "-";
                    continue;
                }
                if (ppCapital.IsChecked == true)
                {
                    output += CapitaliseLetter(words[idx]) + "-";
                    continue;
                }
                if (ppNumber.IsChecked == true)
                {
                    var endNum = (rnd % 10) + 1;
                    output += words[idx] + endNum.ToString() + "-";
                    continue;
                }
                output += words[idx] + "-";
            }
            output = output.Remove(output.Length - 1, 1);
            return output;
        }

        private string CapitaliseLetter(string word)
        {            
            return string.Concat(word[0].ToString().ToUpper(), word.AsSpan(1));
        }

        private void pwGenerateCharacter_Click(object sender, RoutedEventArgs e)
        {
            this.pwOutput.Text = GenerateRandomCharacters(Convert.ToInt32(pwLength.Value)).ToString();
        }
        private void pwGenerateWord_Click(object sender, RoutedEventArgs e)
        {
            this.pwOutput.Text = GenerateRandomWords(Convert.ToInt32(pwLength.Value)).ToString();
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
        private void pwCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(pwOutput.Text);
        }

        private void pwMode_Click(object sender, RoutedEventArgs e)
        {            
            if (!passMode)
            {
                pwUpper.Visibility = Visibility.Hidden;
                pwLower.Visibility = Visibility.Hidden;
                pwNumber.Visibility = Visibility.Hidden;
                pwSymbol.Visibility = Visibility.Hidden;
                pwLabel.Content = "Passphrase Generator";
                pwGenerate.Click -= pwGenerateCharacter_Click;
                pwGenerate.Click += pwGenerateWord_Click;
                pwLength.ValueChanged -= pwLength_ValueChanged;
                pwLength.ValueChanged += ppLength_ValueChanged;
                pwLength.Ticks = new DoubleCollection() { 3, 4, 5, 6, 7, 8 };
                pwLength.Minimum = 3;
                pwLength.Maximum = 8;

                ppCapital.Visibility = Visibility.Visible;
                ppNumber.Visibility = Visibility.Visible;

                passMode = true;
            }
            else
            {
                pwUpper.Visibility = Visibility.Visible;
                pwLower.Visibility = Visibility.Visible;
                pwNumber.Visibility = Visibility.Visible;
                pwSymbol.Visibility = Visibility.Visible;
                pwLabel.Content = "Password Generator";
                pwGenerate.Click += pwGenerateCharacter_Click;
                pwGenerate.Click -= pwGenerateWord_Click;
                pwLength.ValueChanged -= ppLength_ValueChanged;
                pwLength.ValueChanged += pwLength_ValueChanged;                
                pwLength.Ticks = new DoubleCollection() {
                        8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
                pwLength.Minimum = 8;
                pwLength.Maximum = 30; 
                
                ppCapital.Visibility = Visibility.Hidden;
                ppNumber.Visibility = Visibility.Hidden;

                passMode = false;
            }
        }


        #region Checkbox Validation
        private void pwSymbol_Change(object sender, RoutedEventArgs e)
        {
            pwCheckboxValidate();
            this.pwOutput.Text = GenerateRandomCharacters(Convert.ToInt32(pwLength.Value)).ToString();
        }
        private void pwLower_Change(object sender, RoutedEventArgs e)
        {
            pwCheckboxValidate();
            this.pwOutput.Text = GenerateRandomCharacters(Convert.ToInt32(pwLength.Value)).ToString();
        }

        private void pwNumber_Change(object sender, RoutedEventArgs e)
        {
            pwCheckboxValidate();
            this.pwOutput.Text = GenerateRandomCharacters(Convert.ToInt32(pwLength.Value)).ToString();
        }

        private void pwUpper_Change(object sender, RoutedEventArgs e)
        {
            pwCheckboxValidate();
            this.pwOutput.Text = GenerateRandomCharacters(Convert.ToInt32(pwLength.Value)).ToString();
        }

        private void ppCapital_Change(object sender, RoutedEventArgs e)
        {
            this.pwOutput.Text = GenerateRandomWords(Convert.ToInt32(pwLength.Value)).ToString();
        }

        private void ppNumber_Change(object sender, RoutedEventArgs e)
        {
            this.pwOutput.Text = GenerateRandomWords(Convert.ToInt32(pwLength.Value)).ToString();
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


