using Microsoft.Win32;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace FakeIDGenerator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        IdentityCard idcard = new IdentityCard();
        public MainWindow()
        {
            InitializeComponent();
            idcard.generateRandomIdentity();
            updateGUI();
        }

        private void updateGUI()
        {
            surname.Text = idcard.getSurname();
            given.Text = idcard.getGivenName().ToUpper();
            nationality.Text = idcard.getNationality();
            birthdate.Text = idcard.getDateOfBirth().ToString("dd.MM.yyyy");
            cardnumber.Text = idcard.getCardNumber();
            cardnumber2.Text = idcard.getCardNumber().Insert(3," ");
            sex.Text = idcard.getSex();
            duedate.Text = idcard.getDueDate().ToString("dd.MM.yyyy");
            can.Text = idcard.getCanNumber();
            imgBig.Source = idcard.getPhoto();
            imgSmall.Source = idcard.getPhoto();
            pesel.Text = idcard.getPESEL();
            birthplace.Text = idcard.getBirthPlace().ToUpper();
            family.Text = idcard.getFamilyName().ToUpper();
            parents.Text = idcard.getParentsNames().ToUpper();
            authority.Text = idcard.getAuthority().ToUpper();
            issued.Text = idcard.getIssued().ToString("dd.MM.yyyy");
            MRZ.Content = idcard.getMRZ().ToUpper();
        }
        private void generate_Click(object sender, RoutedEventArgs e)
        {
            idcard.generateRandomIdentity();
            updateGUI();
        }
        private void changeImages()
        {
            OpenFileDialog ofp = new OpenFileDialog();
            ofp.Title = "Wybierz zdjęcie";
            ofp.Filter = "Obrazy|*.bmp;*.gif;*.jpg;*.png;|Wszystkie pliki|*.*";
            if (ofp.ShowDialog() == true)
            {
                string selectedFileName = ofp.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                idcard.setPhoto(bitmap);
                updateGUI();
            }
        }
        private void imgBig_MouseDown(object sender, MouseButtonEventArgs e)
        {
            changeImages();
        }

        private void imgSmall_MouseDown(object sender, MouseButtonEventArgs e)
        {
            changeImages();
        }

        private async void save_Click(object sender, RoutedEventArgs e)
        {
            double screenLeft = partA.PointToScreen(new System.Windows.Point(0, 0)).X;
            double screenTop = partA.PointToScreen(new System.Windows.Point(0, 0)).Y;
            double screenWidth = partA.PointToScreen(new System.Windows.Point(partA.Width, partA.Height)).X - screenLeft;
            double screenHeight = partA.PointToScreen(new System.Windows.Point(partA.Width, partA.Height)).Y - screenTop;

            using (Bitmap bmp = new Bitmap((int)screenWidth,
                (int)screenHeight))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    SaveFileDialog ofp = new SaveFileDialog();
                    ofp.Title = "Zapisz zdjęcie";
                    ofp.Filter = "Obraz PNG|*.png|Wszystkie pliki|*.*";
                    if (ofp.ShowDialog() == true)
                    {
                        string filename = ofp.FileName;
                        await Task.Delay(500);
                        g.CopyFromScreen((int)screenLeft, (int)screenTop, 0, 0, bmp.Size);
                        bmp.Save(filename);
                    }
                }

            }
        }

        private async void save_back_Click(object sender, RoutedEventArgs e)
        {
            double screenLeft = partB.PointToScreen(new System.Windows.Point(0, 0)).X;
            double screenTop = partB.PointToScreen(new System.Windows.Point(0, 0)).Y;
            double screenWidth = partB.PointToScreen(new System.Windows.Point(partB.Width, partB.Height)).X - screenLeft;
            double screenHeight = partB.PointToScreen(new System.Windows.Point(partB.Width, partB.Height)).Y - screenTop;

            using (Bitmap bmp = new Bitmap((int)screenWidth,
                (int)screenHeight))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    SaveFileDialog ofp = new SaveFileDialog();
                    ofp.Title = "Zapisz zdjęcie";
                    ofp.Filter = "Obraz PNG|*.png|Wszystkie pliki|*.*";
                    if (ofp.ShowDialog() == true)
                    {
                        string filename = ofp.FileName;
                        await Task.Delay(500);
                        g.CopyFromScreen((int)screenLeft, (int)screenTop, 0, 0, bmp.Size);
                        bmp.Save(filename);
                    }
                }

            }
        }

        private void surname_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            idcard.setSurname(surname.Text);
            idcard.setMRZ();
            updateGUI();
        }

        private void given_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            idcard.setGivenName('a',given.Text);
            idcard.setPESEL();
            idcard.setMRZ();
            updateGUI();
        }

        private void nationality_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (nationality.Text.Length >= 3)
            {
                idcard.setNationality(nationality.Text);
                idcard.setMRZ();
                updateGUI();
            }
        }

        private void birthdate_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                var bd = birthdate.Text.Split('.');
                idcard.setDateOfBirth(new DateTime(int.Parse(bd[2]), int.Parse(bd[1]), int.Parse(bd[0])));
                idcard.setPESEL();
                idcard.setMRZ();
                updateGUI();
            }
            catch (Exception)
            {

            }
        }

        private void duedate_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                var bd = duedate.Text.Split('.');
                idcard.setDueDate(new DateTime(int.Parse(bd[2]), int.Parse(bd[1]), int.Parse(bd[0])));
                idcard.setMRZ();
                updateGUI();
            }
            catch (Exception)
            {

            }
        }

        private void issued_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                var bd = issued.Text.Split('.');
                idcard.setIssued(new DateTime(int.Parse(bd[2]), int.Parse(bd[1]), int.Parse(bd[0])));
                updateGUI();
            }
            catch (Exception)
            {

            }
        }

        private void can_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            
        }
    }
}
