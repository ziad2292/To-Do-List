using DBapplication;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for ChangeName.xaml
    /// </summary>
    public partial class ChangeName : Window
    {
        public string name;
        Controller cntrl;
        public ChangeName()
        {
            InitializeComponent();
            cntrl = new Controller();
        }

        private void GotFocus(object sender, RoutedEventArgs e)
        {
            if (NewName.Text == "Name")
            {
                NewName.Text = "";
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            name = NewName.Text;
            cntrl.UpdateUserName(name);
            HomeScreen homeScreenObj = (HomeScreen)GetWindow(Application.Current.MainWindow);
            homeScreenObj.GreetingText.Text = "Welcome, " + name;
            this.Close();
        }

        
    }
}
