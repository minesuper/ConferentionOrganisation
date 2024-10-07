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

namespace ConferentionOrganisationProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        private int CountAuthTries { get; set; }
        private string CaptchaText { get; set; }
        public Authorization()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            CaptchaLabel.Visibility = Visibility.Collapsed;
            CaptchaAnswerTB.Visibility = Visibility.Collapsed;
            CaptchaImage.Visibility = Visibility.Collapsed;
        }
        private BitmapSource GenerateCaptcha()
        {
            CaptchaText = "";
            List<char> Words = new List<char>();
            for (char i = 'A'; i<= 'Z'; i++)
            {
                Words.Add(i);
            }
            Random rnd = new Random();
            for (int i = 0; i < 4; i++)
            {
                CaptchaText += Words[rnd.Next(Words.Count)];
            }
            var Visual = new DrawingVisual();
            using (var DrawContext = Visual.RenderOpen())
            {
                int X = 10;
                var Font = new Typeface(new FontFamily("Arial"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
                foreach (var i  in CaptchaText)
                {
                    DrawContext.PushTransform(new TranslateTransform(X, rnd.Next(5, 15)));
                    var FormatedText = new FormattedText(i.ToString(),
                        System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                        Font, 36, new SolidColorBrush(Colors.Black));
                    DrawContext.DrawText(FormatedText, new Point(X, 10));
                    DrawContext.Pop();
                    X += 10;
                }
            }
            var BitmapImage = new RenderTargetBitmap(100, 40, 48, 48, PixelFormats.Pbgra32);
            BitmapImage.Render(Visual);
            return BitmapImage;
        }
        private async void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder errors = new StringBuilder();
                string Login = LoginTB.Text;
                string Password = PasswordPB.Password;
                string CaptchaAnswer = CaptchaAnswerTB.Text;
                if (string.IsNullOrEmpty(Login))
                {
                    errors.AppendLine("Заполните поле логина");
                }
                if (!int.TryParse(Login, out var res))
                {
                    errors.AppendLine("Логин - id");
                }
                if (string.IsNullOrEmpty(Password))
                {
                    errors.AppendLine("Заполните поле пароля");
                }
                if (CountAuthTries >= 3 && string.IsNullOrEmpty(CaptchaAnswer))
                {
                    errors.AppendLine("Каптча не решена");
                }
                else if (CountAuthTries >= 3 && CaptchaAnswer != CaptchaText)
                {
                    errors.AppendLine("Каптча решена неправильно");
                }
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString(), "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                int LoginToInt = Convert.ToInt32(Login);
                var user = Model.ConferentionOrganisationDBEntities.GetContext().Users
                    .Where(d => d.User_Id == LoginToInt && d.User_Password == Password).FirstOrDefault();
                if (user == null)
                {
                    MessageBox.Show("Неправильный логин/пароль", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    CountAuthTries++;
                    if (CountAuthTries >= 3)
                    {
                        CaptchaLabel.Visibility = Visibility.Visible;
                        CaptchaAnswerTB.Visibility = Visibility.Visible;
                        CaptchaImage.Visibility = Visibility.Visible;
                        CaptchaImage.Source = GenerateCaptcha();
                        AuthButton.IsEnabled = false;
                        await Task.Delay(10000);
                        AuthButton.IsEnabled = true;
                    }
                    return;
                }
                Init();
                CountAuthTries = 0;
                switch (user.Roles.Roles_Name)
                {
                    case "Жюри":
                        Classes.Navigation.ActiveFrame.Navigate(new Pages.NotOrgPage(user.Roles.Roles_Name));
                        break;
                    case "Модератор":
                        Classes.Navigation.ActiveFrame.Navigate(new Pages.NotOrgPage(user.Roles.Roles_Name));
                        break;
                    case "Участник":
                        Classes.Navigation.ActiveFrame.Navigate(new Pages.NotOrgPage(user.Roles.Roles_Name));
                        break;
                    case "Организатор":
                        Classes.Navigation.ActiveFrame.Navigate(new Pages.OrganisatorPage());
                        break;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Classes.Navigation.ActiveFrame.CanGoBack)
            {
                Classes.Navigation.ActiveFrame.GoBack();
            }
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Navigation.ActiveFrame.Navigate(new Pages.RegisterFrame());
        }
    }
}
