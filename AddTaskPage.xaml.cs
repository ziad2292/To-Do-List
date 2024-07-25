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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Media.Animation;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for AddTaskPage.xaml
    /// </summary>
    public partial class AddTaskPage : Page
    {
        Controller cntrl;
        int TaskID;
        Queue<string> subTasks;
        int SubTasksCount;

        public AddTaskPage()
        {
            InitializeComponent();
            cntrl = new Controller();
            rectangle.Visibility = Visibility.Hidden;
            SubTaskAddedText.Visibility = Visibility.Hidden;
            subTasks = new Queue<string>();
            SubTasksCount = 0;
        }

        private void TaskName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TaskName.Text == "Task Name")
            {
                TaskName.Text = "";
            }
        }

        private void TaskDescription_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TaskDescription.Text == "Description")
            {
                TaskDescription.Text = "";
            }
        }

        private void SubTaskName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SubTaskNameBox.Text == "Sub Task Name")
            {
                SubTaskNameBox.Text = "";
            }
        }

        

        bool nameFlag = false;
        private void TaskName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TaskName.Text == "")
            {
                nameFlag = false;
                TaskName.Text = "Task Name";
            }
            else
            {
                nameFlag = true;
            }
        }

        private void TaskDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TaskDescription.Text == "")
            {
                TaskDescription.Text = "Description";
            }
        }

        private void SubTaskName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SubTaskNameBox.Text == "")
            {
                SubTaskNameBox.Text = "Sub Task Name";
            }
        }


        bool typeFlag = false;
        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            typeFlag = true;
        }

        bool startFlag = false;
        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            startFlag = true;
        }

        bool endFlag = false;
        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            endFlag = true;
        }

        private void AddSubTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if(SubTaskNameBox.Text == "" || SubTaskNameBox.Text == "Sub Task Name")
            {
                MessageBox.Show("Please Enter SubTask Name");
            }
            else
            {
                if (SubTasksCount >= 12)
                {
                    MessageBox.Show("Maximum Number Of SubTasks Reached");
                    SubTaskNameBox.Text = "Sub Task Name";
                }
                else
                {
                    subTasks.Enqueue(SubTaskNameBox.Text);
                    SubTasksCount++;
                    SubTaskNameBox.Text = "Sub Task Name";
                    Storyboard sb = (this.Resources["SubTaskAdded"] as Storyboard);
                    sb.Begin();
                }
                
            }
            
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if(nameFlag && startFlag && endFlag && typeFlag)
            {
                cntrl.InsertTask(TaskName.Text, StartDate.Text, EndDate.Text, (int)Priority.Value, TaskDescription.Text, Type.Text);
                TaskID = cntrl.SelectLastTask();
                foreach(string name in subTasks)
                {
                    cntrl.InsertSubTask(name, TaskID);
                }
                HomeScreen homeScreenObj = (HomeScreen)Window.GetWindow(Application.Current.MainWindow);
                homeScreenObj.Title.Content = "Home";
                homeScreenObj.constructor();
                homeScreenObj.hideSubMenus();
                homeScreenObj.closePage();
            }
            else
            {
                MessageBox.Show("Please Complete All Fields");
            }
        }

        private void MinusSign_Click(object sender, RoutedEventArgs e)
        {
            PlusSign.Visibility = Visibility.Visible;
            MinusSign.Visibility = Visibility.Hidden;
            SubTaskNameTitle.Visibility = Visibility.Hidden;
            border.Visibility = Visibility.Hidden;
            SubTaskName.Visibility = Visibility.Hidden;
            AddSubTaskButton.Visibility = Visibility.Hidden;           
        }

        private void PlusSign_Click(object sender, RoutedEventArgs e)
        {
            PlusSign.Visibility = Visibility.Hidden;
            MinusSign.Visibility = Visibility.Visible;
            SubTaskNameTitle.Visibility = Visibility.Visible;
            border.Visibility = Visibility.Visible;
            SubTaskName.Visibility = Visibility.Visible;
            AddSubTaskButton.Visibility = Visibility.Visible;           
        }

       
    }
}
