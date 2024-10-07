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
    /// Логика взаимодействия для NotOrgPage.xaml
    /// </summary>
    public partial class NotOrgPage : Page
    {
        public NotOrgPage(string role)
        {
            InitializeComponent();
            if (role == "Жюри")
            {
                this.Title = "Окно " + role;
            }
            this.Title = "Окно " + role + "а";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Classes.Navigation.ActiveFrame.CanGoBack)
            {
                Classes.Navigation.ActiveFrame.GoBack();
            }
        }
    }
}
