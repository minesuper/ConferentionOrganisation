using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegistrationJuryModeratorPage.xaml
    /// </summary>
    public partial class RegistrationJuryModeratorPage : Page
    {
        public Model.Users NewUser { get; set; }
        public RegistrationJuryModeratorPage()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            var Roles = Model.ConferentionOrganisationDBEntities.GetContext().Roles.ToList();
            var Sexes = Model.ConferentionOrganisationDBEntities.GetContext().Sexes.ToList();
            Roles.Insert(0, new Model.Roles() { Roles_Name = "Выберите роль" });
            Sexes.Insert(0, new Model.Sexes() { Sexes_Name = "Выберите пол" });
            RoleCB.ItemsSource = Roles;
            SexCB.ItemsSource = Sexes;
            RoleCB.SelectedIndex = 0;
            SexCB.SelectedIndex = 0;

            var User = Model.ConferentionOrganisationDBEntities.GetContext().Users.ToList();
            IdNumberTB.Text = (User.Last().User_Id+1).ToString();

            EventCB.IsEnabled = false;
            EventCB.ItemsSource = Model.ConferentionOrganisationDBEntities.GetContext().Event.ToList();

            PhoneTB.Text = "+7(___)-___-__-__";
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (Classes.Navigation.ActiveFrame.CanGoBack)
            {
                Classes.Navigation.ActiveFrame.GoBack();
            }
        }

        private void RegisterImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetImage();
        }

        private void SetImage()
        {
            OpenFileDialog Dialog = new OpenFileDialog();
            Dialog.Filter = "Изображения (*.png, *.jpg, *.jpeg)|*.png;*.jpg;*.jpeg";
            string Path = "";
            if (Dialog.ShowDialog() == true)
            {
                Path = Dialog.FileName;
            }
            BitmapImage Image = new BitmapImage(new Uri(Path));
            RegisterImage.Source = Image;
            //Path.Split('\\').LastOrDefault().ToString();
        }

        private void AttachToEvent_Unchecked(object sender, RoutedEventArgs e)
        {
            EventCB.IsEnabled = false;
        }

        private void AttachToEvent_Checked(object sender, RoutedEventArgs e)
        {
            EventCB.IsEnabled = true;
        }

        private void PhoneTB_LostFocus(object sender, RoutedEventArgs e)
        {
            string Input = (sender as TextBox).Text;

            if (!Regex.IsMatch(Input, @"^\+7\(\d{3}\)-\d{3}-\d{2}-\d{2}$"))
            {
                MessageBox.Show("Телефон не в формате +7(___)-___-__-__");
            }
        }
    }
}
