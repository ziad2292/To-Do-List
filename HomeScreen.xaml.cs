using DBapplication;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : Window
    {
        Controller cntrl;
        public SearchTaskPage SearchPage;
        public EditTask editWindow;
        public HomeScreen()
        {
            InitializeComponent();
            constructor();
        }

        public void constructor()
        {
            customizeDesign();
            cntrl = new Controller();
            GreetingText.Text = "Welcome, " + cntrl.SelectUserName();
            if(cntrl.SelectPhotoPath() != "/guest.png")
            {
                ImageBrush ib = new ImageBrush();
                ib.Stretch = Stretch.UniformToFill;
                ib.ImageSource = new BitmapImage(new Uri(cntrl.SelectPhotoPath(), UriKind.RelativeOrAbsolute));
                ProfilePicture.Fill = ib; 
            }
            else
            {
                SubPP.Visibility = Visibility.Visible;
                ProfilePicture.Visibility = Visibility.Collapsed;
            }


            showHome();
            setPriotiyTasks();
            markCalendar();
            setDueTodayTasks();
            setStatistics();
            setLateTasks();
        }

        private void customizeDesign()
        {
            TaskPanel.Visibility = Visibility.Collapsed;
        }


        private void showSubMenu(Panel subMenu)
        {
            if(subMenu.Visibility == Visibility.Collapsed)
            {
                subMenu.Visibility = Visibility.Visible;
            }
            else
            {
                subMenu.Visibility = Visibility.Collapsed;
            }
        }
        public void hideSubMenus()
        {
            if (TaskPanel.Visibility == Visibility.Visible)
            {
                TaskPanel.Visibility = Visibility.Collapsed;
            }

        }
        private void Task_Click(object sender, RoutedEventArgs e)
        {
            showSubMenu(TaskPanel);
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            Title.Content = "Add Task";
            openPage(new AddTaskPage());
        }

        private void SearchTasks_Click(object sender, RoutedEventArgs e)
        {
            Title.Content = "Search Tasks";
            openPage(new SearchTaskPage());
        }

        public Page activePage = null;
        public void openPage(Page page)
        {
            hideHome();
            if (activePage != null)
            {
                activePage.Visibility = Visibility.Hidden;
            }
            activePage = page;
            MainFrame.Content = page;
            if (page is SearchTaskPage)
            {
                SearchPage = (SearchTaskPage)page;
            }
            hideSubMenus();
        }

        public void closePage()
        {
            if (activePage != null)
            {
                activePage.Visibility = Visibility.Hidden;
            }
        }

        private void ChangeNameButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeName changeNameWindow = new ChangeName();
            changeNameWindow.Show();
        }

        private void ChangePhotoButton_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Title = "Please Choose A Photo";
            bool? success = fileDialog.ShowDialog();
            if (success == true)
            {
                string path = fileDialog.FileName;
                try
                {
                    ImageBrush ib = new ImageBrush();
                    ib.ImageSource = new BitmapImage(new Uri(path));
                    ib.Stretch = Stretch.UniformToFill;
                    ProfilePicture.Fill = ib;
                    cntrl.UpdatePath(path);
                    SubPP.Visibility = Visibility.Collapsed;
                    ProfilePicture.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please Choose A Photo", "Photo Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Title.Content = "Home";
            constructor();
            hideSubMenus();
            closePage();
        }

        bool opened = true;
        private void SideMenuButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb;
            if (opened)
            {
                sb = (Window.Resources["CloseMenu"] as Storyboard);
                opened = false;
            }
            else
            { 
                sb = (Window.Resources["OpenMenu"] as Storyboard);
                opened = true;
            }
            sb.Begin();

        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            var Result = MessageBox.Show("Are You Sure You Want To Reset Your Account?","Reset Account", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes)
            {
                cntrl.DeleteAll();
                cntrl.UpdateUserName("Guest");
                cntrl.UpdatePath("/guest.png");
                closePage();
                constructor();
            }

        }

        private void Archive_Click(object sender, RoutedEventArgs e)
        {
            Title.Content = "Archive";
            openPage(new ArchivePage());
        }

        
        private void setPriotiyTasks()
        {
            int count;
            DataTable queryResult;
            PriorityTemplate temp;
            TextBlock title;
            TextBlock noTasks;
            count = cntrl.SelectCompletedTaskPrioritySortedCount();
            queryResult = cntrl.SelectCompletedTaskPrioritySorted();
            PriorityGrid.Children.Clear();
            title = new TextBlock();
            PriorityGrid.Children.Add(title);
            title.TextWrapping = TextWrapping.Wrap;
            title.Text = "Priority";
            title.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
            title.FontFamily = new FontFamily("Perpetua Titling MT");
            title.FontWeight = FontWeights.Bold;
            title.FontSize = 20;
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(title, 0);
            Grid.SetColumnSpan(title, 2);

            PriorityTemplate.r = 1;
            if(count == 0)
            {
                noTasks = new TextBlock();
                PriorityGrid.Children.Add(noTasks);
                noTasks.TextWrapping = TextWrapping.Wrap;
                noTasks.Text = "No Tasks Yet";
                noTasks.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
                noTasks.FontFamily = new FontFamily("PMingLiU-ExtB");
                noTasks.FontWeight = FontWeights.Bold;
                noTasks.FontSize = 18;
                noTasks.HorizontalAlignment = HorizontalAlignment.Center;
                noTasks.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetRow(noTasks, 2);
                Grid.SetColumn(noTasks, 0);
                Grid.SetColumnSpan(noTasks, 2);
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    if (i == 4) break;
                    temp = new PriorityTemplate(queryResult.Rows[i][0].ToString(), queryResult.Rows[i][1].ToString(), queryResult.Rows[i][4].ToString(), cntrl.SelectCompletedSubTasksCount(Int32.Parse(queryResult.Rows[i][0].ToString())).ToString(), cntrl.SelectSubTasksCount(Int32.Parse(queryResult.Rows[i][0].ToString())).ToString());
                }
            }
            
        }
        private void hideHome()
        {
            Priority.Visibility = Visibility.Hidden;
            LateTasks.Visibility = Visibility.Hidden;
            CalenderGrid.Visibility = Visibility.Hidden;
            AddButton.Visibility = Visibility.Hidden;
            DueTodayGrid.Visibility = Visibility.Hidden;
            Statistics.Visibility = Visibility.Hidden;
        }
        private void showHome()
        {
            Priority.Visibility = Visibility.Visible;
            LateTasks.Visibility = Visibility.Visible;
            CalenderGrid.Visibility = Visibility.Visible;
            AddButton.Visibility = Visibility.Visible;
            DueTodayGrid.Visibility = Visibility.Visible;
            Statistics.Visibility = Visibility.Visible;
        }

        private void addTaskWidget_Click(object sender, RoutedEventArgs e)
        {
            Title.Content = "Add Task";
            openPage(new AddTaskPage());
        }

        int[] date;
        DataTable query;
        int year, month, day;


        private void markCalendar()
        {
            Calendar.BlackoutDates.Clear();
            Calendar.SelectionMode = CalendarSelectionMode.MultipleRange;
            Calendar.SelectedDates.Clear();
            query = cntrl.SelectDistinctDates();
            for(int i = 0; i < cntrl.SelectDistinctDatesCount(); i++)
            {
                date = extractDate(query.Rows[i][0].ToString());
                year = date[2];
                month = date[0];
                day = date[1];
                Calendar.BlackoutDates.Add(new CalendarDateRange(new DateTime(year, month, day), new DateTime(year, month, day)));
            }
    }

        public int[] extractDate(string date)
        {
            string temp = "";
            int[] output = { 0, 0, 0 };
            int x = 0;
            foreach (char i in date)
            {

                if (i != '/' && i != ' ')
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

        
        private void setDueTodayTasks()
        {
            int count;
            DataTable queryResult;
            DueTodayTemplate temp;
            TextBlock title;
            TextBlock noTasks;
            count = cntrl.SelectDueTodayTasksCount();
            queryResult = cntrl.SelectDueTodayTasks();
            DueToday.Children.Clear();
            title = new TextBlock();
            DueToday.Children.Add(title);
            title.Margin = new Thickness(5, 0, 5, 0);
            title.TextWrapping = TextWrapping.Wrap;
            title.Text = "Due Today";
            title.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
            title.FontFamily = new FontFamily("Perpetua Titling MT");
            title.FontWeight = FontWeights.Bold;
            title.FontSize = 20;
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(title, 0);
            Grid.SetColumnSpan(title, 2);
            DueTodayTemplate.r = 1;
            if(count == 0)
            {
                noTasks = new TextBlock();
                DueToday.Children.Add(noTasks);
                noTasks.TextWrapping = TextWrapping.Wrap;
                noTasks.Text = "No Tasks Due Today";
                noTasks.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
                noTasks.FontFamily = new FontFamily("PMingLiU-ExtB");
                noTasks.FontWeight = FontWeights.Bold;
                noTasks.FontSize = 18;
                noTasks.HorizontalAlignment = HorizontalAlignment.Center;
                noTasks.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetRow(noTasks, 2);
                Grid.SetColumn(noTasks, 0);
                Grid.SetColumnSpan(noTasks, 2);
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    if (i == 4) break;
                    temp = new DueTodayTemplate(queryResult.Rows[i][0].ToString(), queryResult.Rows[i][1].ToString(), queryResult.Rows[i][4].ToString(), cntrl.SelectCompletedSubTasksCount(Int32.Parse(queryResult.Rows[i][0].ToString())).ToString(), cntrl.SelectSubTasksCount(Int32.Parse(queryResult.Rows[i][0].ToString())).ToString());
                }
            }
            
        }
        private void setStatistics()
        {
            TotalTasks.Text = cntrl.SelectAllTasksCount().ToString() + " Tasks";
            Done.Text = cntrl.SelectAllCompletedTasksCount().ToString() + "\nDone";
            Unfinished.Text = cntrl.SelectAllUnfinishedTasksCount().ToString() + "\nUnfinished";
            Unfinished.Text = cntrl.SelectAllUnfinishedTasksCount().ToString() + "\nUnfinished";
            Late.Text = cntrl.SelectAllArchiveTasksCount().ToString() + "\nLate";
        }

        private void setLateTasks()
        {
            int count;
            DataTable queryResult;
            LateTasksTemplate temp;
            TextBlock title;
            TextBlock noTasks;
            count = cntrl.SelectAllLateArchiveTasksCount();
            queryResult = cntrl.SelectAllLateArchiveTasks();
            LateTasksGrid.Children.Clear();
            title = new TextBlock();
            LateTasksGrid.Children.Add(title);
            title.TextWrapping = TextWrapping.Wrap;
            title.Margin = new Thickness(15, 0, 0, 0);
            title.Text = "Late Tasks";
            title.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
            title.FontFamily = new FontFamily("Perpetua Titling MT");
            title.FontWeight = FontWeights.Bold;
            title.FontSize = 20;
            title.HorizontalAlignment = HorizontalAlignment.Left;
            title.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(title, 0);
            Grid.SetColumnSpan(title, 2);
            LateTasksTemplate.r = 1;
            if (count == 0)
            {
                noTasks = new TextBlock();
                LateTasksGrid.Children.Add(noTasks);
                noTasks.TextWrapping = TextWrapping.Wrap;
                noTasks.Text = "No Late Tasks";
                noTasks.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
                noTasks.FontFamily = new FontFamily("PMingLiU-ExtB");
                noTasks.FontWeight = FontWeights.Bold;
                noTasks.FontSize = 18;
                noTasks.HorizontalAlignment = HorizontalAlignment.Center;
                noTasks.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetRow(noTasks, 2);
                Grid.SetColumn(noTasks, 0);
                Grid.SetColumnSpan(noTasks, 3);
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    if (i == 4) break;
                    temp = new LateTasksTemplate(queryResult.Rows[i][0].ToString(), queryResult.Rows[i][1].ToString(), queryResult.Rows[i][3].ToString(), queryResult.Rows[i][7].ToString());
                }
            }
        }
    }    
}
