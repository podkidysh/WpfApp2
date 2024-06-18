using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] captchFile = new string[]
            {
                "image/cap1.png" , "image/cap2.png" , "image/cap3.png"
            };
        string[] captchIsTrue = new string[]
            {
                "ACD" , "NEL" , "CSM"
            };

        int captchInt = 0;
        bool captchBool = false;
        int i = 0; // pods4et popitok

    public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            captchInt = random.Next(0, captchFile.Length);
            Uri uri = new Uri(captchFile[captchInt], UriKind.RelativeOrAbsolute);
            BitmapImage bitmapImage = new BitmapImage(uri);
            imageCap.Source = bitmapImage;
        }

        private void btGo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbLogin.Text) && string.IsNullOrEmpty(pbPass.Password))
            {
                MessageBox.Show("vvedite vse dannie", "error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (captchBool == false)
            {
                if (tbLogin.Text == "admin" && pbPass.Password == "admin")
                {
                    MessageBox.Show("yra vi voshli");
                }
                else
                {
                    MessageBox.Show("vvedite vse dannie", "error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    captchBool = true;
                    spCap.Visibility = Visibility.Visible;
                    return;
                }
            }

            if (captchBool == true)
            {
                if (string.IsNullOrEmpty(tbCap.Text))
                {
                    MessageBox.Show("vvedite cap", "error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (tbLogin.Text == "admin" && pbPass.Password == "admin" && tbCap.Text == captchIsTrue[captchInt])
                {
                    MessageBox.Show("yra vi voshli");
                    captchBool = false;
                    spCap.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("vvedite cap", "error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    Title = "block";
                    Thread.Sleep(10000);
                    Title = "authorizations";
                    MainWindow_Loaded(sender, e);
                    return;
                }
            }
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            MainWindow_Loaded(sender, e);
        }

        private void btShowPass_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(pbPass.Password);
        }
    }
}