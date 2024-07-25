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
using System.Data;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for SearchTaskPage.xaml
    /// </summary>
    public partial class SearchTaskPage : Page
    {

        Controller cntrl;

        public SearchTaskPage()
        {
            InitializeComponent();
            cntrl = new Controller();
            order = 0;
            margin = 40;
        }

        private void SearchBar_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBar.Text == "Search Task....")
            {
                SearchBar.Text = "";
            }
        }

        private void SearchBar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBar.Text == "")
            {
                SearchBar.Text = "Search Task....";
            }
        }
        
        public static double margin = 40;
        public static int order = 0;

        string[] start, end;
        private void DisplayTasks(DataTable queryResult, int numOfTasks)
        {
            
            for (int i = 0; i < numOfTasks; i++)
            {
                start = queryResult.Rows[i][2].ToString().Split(' ');
                end = queryResult.Rows[i][3].ToString().Split(' ');
                new TaskTemp((int)queryResult.Rows[i][0], queryResult.Rows[i][1].ToString(), start[0], end[0], queryResult.Rows[i][4].ToString(), queryResult.Rows[i][5].ToString(), queryResult.Rows[i][6].ToString(), queryResult.Rows[i][7].ToString(), queryResult.Rows[i][8].ToString());
            }
        }

        DataTable queryResult;
        int numOfTasks;
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            TaskTemp.margin = 15;
            TaskTemp.order = 0;
            TasksGrid.Children.Clear();
            if(SearchBar.Text == "Search Task...." && Type_Filter.Text == "All Tasks" && !(bool)Priority_Filter.IsChecked && !(bool)EndDateFilter.IsChecked)
            {
                queryResult = cntrl.SelectAllTasks();
                numOfTasks = cntrl.SelectAllTasksCount();
                DisplayTasks(queryResult, numOfTasks);
            }
            else if(SearchBar.Text != "Search Task...." && Type_Filter.Text == "All Tasks" && !(bool)Priority_Filter.IsChecked && !(bool)EndDateFilter.IsChecked)
            {
                queryResult = cntrl.SelectTask(SearchBar.Text);
                numOfTasks = cntrl.SelectTaskCount(SearchBar.Text);
                DisplayTasks(queryResult, numOfTasks);
            }
            else if (SearchBar.Text == "Search Task...." && Type_Filter.Text != "All Tasks" && !(bool)Priority_Filter.IsChecked && !(bool)EndDateFilter.IsChecked)
            {
                queryResult = cntrl.SelectTaskByType(Type_Filter.Text);
                numOfTasks = cntrl.SelectTaskByTypeCount(Type_Filter.Text);
                DisplayTasks(queryResult, numOfTasks);
            }
            else if (SearchBar.Text == "Search Task...." && Type_Filter.Text == "All Tasks" && (bool)Priority_Filter.IsChecked && !(bool)EndDateFilter.IsChecked)
            {
                queryResult = cntrl.SelectTaskPrioritySorted();
                numOfTasks = cntrl.SelectAllTasksCount();
                DisplayTasks(queryResult, numOfTasks);
            }
            else if (SearchBar.Text == "Search Task...." && Type_Filter.Text == "All Tasks" && !(bool)Priority_Filter.IsChecked && (bool)EndDateFilter.IsChecked)
            {
                queryResult = cntrl.SelectTaskRemainingDaysSorted();
                numOfTasks = cntrl.SelectAllTasksCount();
                DisplayTasks(queryResult, numOfTasks);
            }
            else if (SearchBar.Text == "Search Task...." && Type_Filter.Text != "All Tasks" && (bool)Priority_Filter.IsChecked && !(bool)EndDateFilter.IsChecked)
            {
                queryResult = cntrl.SelectTaskByTypePrioritySorted(Type_Filter.Text);
                numOfTasks = cntrl.SelectTaskByTypeCount(Type_Filter.Text);
                DisplayTasks(queryResult, numOfTasks);
            }
            else if (SearchBar.Text == "Search Task...." && Type_Filter.Text != "All Tasks" && !(bool)Priority_Filter.IsChecked && (bool)EndDateFilter.IsChecked)
            {
                queryResult = cntrl.SelectTaskByTypeRemainingDaysSorted(Type_Filter.Text);
                numOfTasks = cntrl.SelectTaskByTypeCount(Type_Filter.Text);
                DisplayTasks(queryResult, numOfTasks);
            }
            SearchBar.Text = "Search Task....";
        }

        bool isCheckedPriority = false;
        private void Priority_Filter_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Priority_Filter.IsChecked && !isCheckedPriority)
                Priority_Filter.IsChecked = false;
            else
            {
                Priority_Filter.IsChecked = true;
                isCheckedPriority = false;
            }
        }

        private void Priority_Filter_Checked(object sender, RoutedEventArgs e)
        {
            isCheckedPriority = (bool)Priority_Filter.IsChecked;
        }

        bool isCheckedEndDate = false;
        private void EndDateFilter_Checked(object sender, RoutedEventArgs e)
        {
            isCheckedEndDate = (bool)EndDateFilter.IsChecked;
        }

        private void EndDateFilter_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)EndDateFilter.IsChecked && !isCheckedEndDate)
                EndDateFilter.IsChecked = false;
            else
            {
                EndDateFilter.IsChecked = true;
                isCheckedEndDate = false;
            }
        }

       
    }
}
