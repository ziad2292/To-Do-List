using DBapplication;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for EditTask.xaml
    /// </summary>
    public partial class EditTask : Window
    {
        Controller cntrl;
        int TaskID;
        string oldName, oldDescription;
        HomeScreen homeScreenObj;
        public EditTask(int id)
        {
            InitializeComponent();
            cntrl = new Controller();
            homeScreenObj = (HomeScreen)Window.GetWindow(Application.Current.MainWindow);
            rectangle.Visibility = Visibility.Hidden;
            SubTaskAddedText.Visibility = Visibility.Hidden;
            TaskID = id;
            setTitles();
            setSubTasks();
            updateVisibility(true);
        }

        public int[] extractDate(string date)
        {
            string temp = "";
            int[] output = { 0, 0, 0 };
            int x = 0;
            foreach(char i in date)
            {
                
                if(i != '/' && i != ' ')
                {
                    temp += i;

                }
                else
                {
                    output[x] = Int32.Parse(temp);
                    temp = "";
                    x++;
                    if (i == ' ') { break; }
                }

            }

            return output; 
        }

        public void setTitles()
        {
            DataTable task = cntrl.SelectTaskByID(TaskID);
            TaskNameTitle.Text = task.Rows[0][1].ToString();
            oldName = task.Rows[0][1].ToString();
            StartDateTitle.Text = task.Rows[0][2].ToString();
            EndDateTitle.Text = task.Rows[0][3].ToString();
            TaskDescriptionTitle.Text = task.Rows[0][6].ToString();
            oldDescription = task.Rows[0][6].ToString();
            TaskTypeTitle.Text = task.Rows[0][8].ToString();
            TaskPriorityTitle.Text = task.Rows[0][5].ToString();
            RemainingDaysTitle.Text = task.Rows[0][7].ToString();
        }

        public void setFields()
        {
            DataTable task = cntrl.SelectTaskByID(TaskID);
            TaskName.Text = task.Rows[0][1].ToString();
            oldName = task.Rows[0][1].ToString();
            int[] startDate = extractDate(task.Rows[0][2].ToString());
            int[] endDate = extractDate(task.Rows[0][3].ToString());
            StartDate.SelectedDate = new DateTime(startDate[2], startDate[0], startDate[1]);
            EndDate.SelectedDate = new DateTime(endDate[2], endDate[0], endDate[1]);
            TaskDescription.Text = task.Rows[0][6].ToString();
            oldDescription = task.Rows[0][6].ToString();
            TypeBox.Text = task.Rows[0][8].ToString();
            Priority.Value = (int)task.Rows[0][5];
            RemainingDaysTitle.Text = task.Rows[0][7].ToString();
        }

        DataTable queryResult;
        int numOfSubTasks;
        public void setSubTasks()
        {
            SubTaskTemplate.order = true;
            SubTaskTemplate.rightIndex = 0;
            SubTaskTemplate.leftIndex = 0;
            queryResult = cntrl.SelectSubTasks(TaskID);
            numOfSubTasks = cntrl.SelectSubTasksCount(TaskID);
            displaySubTasks(queryResult, numOfSubTasks);
        }

        SubTaskTemplate temp;
        public void displaySubTasks(DataTable queryResult, int numOfSubTasks)
        {
            RightSubTaskStack.Children.Clear();
            LeftSubTaskStack.Children.Clear();
            for (int i = 0; i < numOfSubTasks; i++)
            {
                temp = new SubTaskTemplate(queryResult.Rows[i][0].ToString(), queryResult.Rows[i][1].ToString(), queryResult.Rows[i][3].ToString());
                if (SubTaskTemplate.order)
                {
                    RightSubTaskStack.Children.Insert(SubTaskTemplate.rightIndex++, temp.outerGrid);
                    SubTaskTemplate.order = false;
                }
                else
                {
                    LeftSubTaskStack.Children.Insert(SubTaskTemplate.leftIndex++, temp.outerGrid);
                    SubTaskTemplate.order = true;
                }
            }
        }

        bool nameFlag = true;
        private void TaskName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TaskName.Text == "")
            {
                nameFlag = false;
                TaskName.Text = oldName;
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
                TaskDescription.Text = oldDescription;
            }
        }

        private void SubTaskName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SubTaskNameBox.Text == "")
            {
                SubTaskNameBox.Text = "Sub Task Name";
            }
        }
        int SubTasksCount;
        private void AddSubTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubTaskNameBox.Text == "" || SubTaskNameBox.Text == "Sub Task Name")
            {
                MessageBox.Show("Please Enter SubTask Name");
            }
            else
            {
                SubTasksCount = cntrl.SelectSubTasksCount(TaskID);
                if (SubTasksCount >= 12)
                {
                    MessageBox.Show("Maximum Number Of SubTasks Reached");
                    AddSubTaskButton.IsEnabled = true;
                }
                else
                {
                    cntrl.InsertSubTask(SubTaskNameBox.Text, TaskID);
                    setSubTasks();
                    Storyboard sb = (this.Resources["SubTaskAdded"] as Storyboard);
                    sb.Begin();
                }
                SubTaskNameBox.Text = "Sub Task Name";

            }
        }

        private void CompletedButton_Click(object sender, RoutedEventArgs e)
        {
            CompleteStatus.Text = "Completed";
            cntrl.UpdateTaskCompletion(TaskID, "Completed");
        }

        private void NotCompletedButton_Click(object sender, RoutedEventArgs e)
        {
            CompleteStatus.Text = "Not Completed";
            cntrl.UpdateTaskCompletion(TaskID, "Not Completed");
        }

        bool editStatus = false;
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (editStatus)
            {
                if (nameFlag)
                {
                    EditButton.Content = "Edit";
                    updateVisibility(editStatus);
                    editStatus = false;
                    cntrl.UpdateTask(TaskID.ToString(), TaskName.Text, StartDate.Text, EndDate.Text, (int)Priority.Value, TaskDescription.Text, TypeBox.Text);
                    setFields();
                    setTitles();
                    homeScreenObj.closePage();
                    homeScreenObj.Title.Content = "Search Tasks";
                    homeScreenObj.openPage(new SearchTaskPage());
                }
                else
                {
                    MessageBox.Show("Please Complete All Fields");
                }
            }
            else
            {
                EditButton.Content = "Update";
                updateVisibility(editStatus);
                editStatus = true;
            }
        }

        private void SubTaskName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SubTaskNameBox.Text == "Sub Task Name")
            {
                SubTaskNameBox.Text = "";
            }
        }

        private void MinusSign_Click(object sender, RoutedEventArgs e)
        {
            SubTaskNameTitle.Visibility = Visibility.Hidden;
            border.Visibility = Visibility.Hidden;
            SubTaskName.Visibility = Visibility.Hidden;
            AddSubTaskButton.Visibility = Visibility.Hidden;
            PlusSign.Visibility = Visibility.Visible;
            MinusSign.Visibility = Visibility.Hidden;
        }

        private void PlusSign_Click(object sender, RoutedEventArgs e)
        {
            SubTaskNameTitle.Visibility = Visibility.Visible;
            border.Visibility = Visibility.Visible;
            SubTaskName.Visibility = Visibility.Visible;
            AddSubTaskButton.Visibility = Visibility.Visible;
            PlusSign.Visibility = Visibility.Hidden;
            MinusSign.Visibility = Visibility.Visible;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            cntrl.DeleteTask(TaskID);
            this.Close();
            homeScreenObj.closePage();
            homeScreenObj.openPage(new SearchTaskPage());
        }

        private void updateVisibility(bool editStatus)
        {
            if(editStatus){
                TaskNameTitle.Visibility = Visibility.Visible;
                TaskPriorityTitle.Visibility = Visibility.Visible;
                StartDateTitle.Visibility = Visibility.Visible;
                EndDateTitle.Visibility = Visibility.Visible;
                TaskTypeTitle.Visibility = Visibility.Visible;
                DescriptionTitleBorder.Visibility = Visibility.Visible;

                TaskNameBoarder.Visibility = Visibility.Hidden;
                Priority.Visibility = Visibility.Hidden;
                StartDate.Visibility = Visibility.Hidden;
                EndDate.Visibility = Visibility.Hidden;
                Type.Visibility = Visibility.Hidden;
                TaskDescriptionBorder.Visibility = Visibility.Hidden;
            }
            else
            {
                setFields();
                TaskNameTitle.Visibility = Visibility.Hidden;
                TaskPriorityTitle.Visibility = Visibility.Hidden;
                StartDateTitle.Visibility = Visibility.Hidden;
                EndDateTitle.Visibility = Visibility.Hidden;
                TaskTypeTitle.Visibility = Visibility.Hidden;
                DescriptionTitleBorder.Visibility = Visibility.Hidden;

                TaskNameBoarder.Visibility = Visibility.Visible;
                Priority.Visibility = Visibility.Visible;
                StartDate.Visibility = Visibility.Visible;
                EndDate.Visibility = Visibility.Visible;
                Type.Visibility = Visibility.Visible;
                TaskDescriptionBorder.Visibility = Visibility.Visible;
            }
        }
    }
}
