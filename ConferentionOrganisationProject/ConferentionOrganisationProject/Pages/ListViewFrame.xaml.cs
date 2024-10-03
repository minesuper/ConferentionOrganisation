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
    /// Логика взаимодействия для ListViewFrame.xaml
    /// </summary>
    public partial class ListViewFrame : Page
    {
        private List<Model.Event> _currentEvent = new List<Model.Event>();
        public ListViewFrame()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            _currentEvent = Model.ConferentionOrganisationDBEntities.GetContext().Event.ToList();
            var _ = Model.ConferentionOrganisationDBEntities.GetContext().Directions.ToList();
            _.Insert(0, new Model.Directions() { Directions_Name = "Все направления"});
            DirectionComboBox.ItemsSource = _;
            DirectionComboBox.SelectedIndex = 0;
            EventsListView.ItemsSource = _currentEvent;
        }
        private void Update()
        {

        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
