using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Runtime.InteropServices;
using DBapplication;
using System.Windows.Media.Imaging;
using System.Windows.Markup;
using System.Windows.Input;

namespace ToDoList
{
    internal class TaskTemp
    {
        public static double margin = 15;
        public static int order = 0;
        public int TaskID;
        Controller cntrl;
        Grid grid;
        Rectangle outerBorder;
        Label title;
        Label start;
        Label end;
        Label prior;
        Label remainDays;
        Label taskType;
        Button InCompleted;
        Button completed;
        Border completeBorder;
        Border inCompleteBorder;
        Button expand;
        HomeScreen homeScreenObj;

        public TaskTemp(int id, string name, string startDate, string endDate, string status, string priority, string descr, string remainingDays, string type)
        {
            cntrl = new Controller();

            grid = new Grid();
            grid.Name = "TaskGrid";
            grid.HorizontalAlignment = HorizontalAlignment.Left;
            grid.Height = 291;
            grid.Width = 200;
            grid.Margin = new Thickness(margin, 20, 0, 0);
            grid.VerticalAlignment = VerticalAlignment.Top;
            homeScreenObj = (HomeScreen)Window.GetWindow(Application.Current.MainWindow);
            homeScreenObj.SearchPage.TasksGrid.Children.Insert(order, grid);
            order++;


            
            outerBorder = new Rectangle();
            grid.Children.Add(outerBorder);
            outerBorder.HorizontalAlignment = HorizontalAlignment.Center;
            outerBorder.Height = 291;
            outerBorder.Width = 200;
            outerBorder.Stroke = new SolidColorBrush(Colors.Black);
            outerBorder.VerticalAlignment = VerticalAlignment.Center;
            outerBorder.RadiusX = 11;
            outerBorder.RadiusY = 11;
            if (status == "Completed") outerBorder.Opacity = 0.4;
            else outerBorder.Opacity = 0.1;
            outerBorder.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#768B96");

            title = new Label();
            grid.Children.Add(title);
            title.Name = "TaskTitle";
            title.HorizontalAlignment = HorizontalAlignment.Left;
            title.VerticalAlignment = VerticalAlignment.Top;
            title.Width = 150;
            title.Margin = new Thickness(10, 10, 0, 0);
            title.Content = name;
            title.FontFamily = new FontFamily("Perpetua Titling MT");
            title.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");

            start = new Label();
            grid.Children.Add(start);
            start.Name = "StartDate";
            start.HorizontalAlignment = HorizontalAlignment.Left;
            start.VerticalAlignment = VerticalAlignment.Top;
            start.Width = 130;
            start.Height = 25;
            start.Margin = new Thickness(30, 64, 0, 0);
            start.Content = "Start Date: " + startDate;
            start.FontFamily = new FontFamily("PMingLiU-ExtB");
            start.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");

            end = new Label();
            grid.Children.Add(end);
            end.Name = "EndDate";
            end.HorizontalAlignment = HorizontalAlignment.Left;
            end.VerticalAlignment = VerticalAlignment.Top;
            end.Width = 125;
            end.Height = 25;
            end.Margin = new Thickness(30, 94, 0, 0);
            end.Content = "End Date: " + endDate;
            end.FontFamily = new FontFamily("PMingLiU-ExtB");
            end.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");

            prior = new Label();
            grid.Children.Add(prior);
            prior.Name = "Priority";
            prior.HorizontalAlignment = HorizontalAlignment.Left;
            prior.VerticalAlignment = VerticalAlignment.Top;
            prior.Width = 100;
            prior.Height = 25;            
            prior.Margin = new Thickness(30, 124, 0, 0);
            prior.Content = "Priority Level: " + priority;
            prior.FontFamily = new FontFamily("PMingLiU-ExtB");
            prior.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");

            remainDays = new Label();
            grid.Children.Add(remainDays);
            remainDays.Name = "RemainingDays";
            remainDays.HorizontalAlignment = HorizontalAlignment.Left;
            remainDays.VerticalAlignment = VerticalAlignment.Top;
            remainDays.Width = 120;
            remainDays.Height = 30;
            remainDays.Margin = new Thickness(30, 154, 0, 0);
            remainDays.Content = "Remaining Days: " + remainingDays;
            remainDays.FontFamily = new FontFamily("PMingLiU-ExtB");
            remainDays.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");

            taskType = new Label();
            grid.Children.Add(taskType);
            taskType.Name = "Type";
            taskType.HorizontalAlignment = HorizontalAlignment.Left;
            taskType.VerticalAlignment = VerticalAlignment.Top;
            taskType.Width = 100;
            taskType.Height = 25;
            taskType.Margin = new Thickness(30, 184, 0, 0);
            taskType.Content = "Task Type: " + type;
            taskType.FontFamily = new FontFamily("PMingLiU-ExtB");
            taskType.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");

            inCompleteBorder = new Border();
            grid.Children.Add(inCompleteBorder);
            inCompleteBorder.HorizontalAlignment = HorizontalAlignment.Left;
            inCompleteBorder.VerticalAlignment = VerticalAlignment.Top;
            inCompleteBorder.Width = 80;
            inCompleteBorder.Height = 30;
            inCompleteBorder.Margin = new Thickness(20, 250, 0, 0);
            inCompleteBorder.Child = InCompleted;
            inCompleteBorder.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
            inCompleteBorder.CornerRadius = new CornerRadius(15, 15, 15, 15);
            inCompleteBorder.Background.Opacity = 0.7;

            InCompleted = new Button();
            grid.Children.Add(InCompleted);
            InCompleted.Name = "InCompleteButton";
            InCompleted.HorizontalAlignment = HorizontalAlignment.Left;
            InCompleted.VerticalAlignment = VerticalAlignment.Top;
            InCompleted.Width = 80;
            InCompleted.Height = 30;
            InCompleted.Margin = new Thickness(20, 250, 0, 0);
            InCompleted.Content = "InCompleted";
            InCompleted.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF15253F");
            InCompleted.BorderBrush = null;
            InCompleted.Background = null;
            InCompleted.FontFamily = new FontFamily("Perpetua Titling MT");
            InCompleted.Template = homeScreenObj.SearchPage.SearchButton.Template;
            InCompleted.Cursor = Cursors.Hand;
            InCompleted.FontSize = 10;
            InCompleted.Click += InCompleted_Click;

            completeBorder = new Border();
            grid.Children.Add(completeBorder);
            completeBorder.HorizontalAlignment = HorizontalAlignment.Center;
            completeBorder.VerticalAlignment = VerticalAlignment.Top;
            completeBorder.Width = 80;
            completeBorder.Height = 30;
            completeBorder.Margin = new Thickness(110, 250, 0, 0);
            completeBorder.Child = completed;
            completeBorder.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
            completeBorder.CornerRadius = new CornerRadius(15, 15, 15, 15);
            completeBorder.Background.Opacity = 0.7;


            completed = new Button();
            grid.Children.Add(completed);
            completed.Name = "completeButton";
            completed.HorizontalAlignment = HorizontalAlignment.Left;
            completed.VerticalAlignment = VerticalAlignment.Top;
            completed.Width = 80;
            completed.Height = 30;
            completed.Margin = new Thickness(115, 250, 0, 0);
            completed.Content = "Completed";
            completed.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF15253F");
            completed.BorderBrush = null;
            completed.Background = null;
            completed.FontFamily = new FontFamily("Perpetua Titling MT");
            completed.Template = homeScreenObj.SearchPage.SearchButton.Template;
            completed.Cursor = Cursors.Hand;
            completed.FontSize = 10;
            completed.Click += completed_Click;

            expand = new Button();
            grid.Children.Add(expand);
            expand.Name = "ExpandButton";
            expand.HorizontalAlignment = HorizontalAlignment.Left;
            expand.VerticalAlignment = VerticalAlignment.Top;
            expand.Width = 40;
            expand.Height = 30;
            expand.Margin = new Thickness(150, 10, 0, 0);
            expand.Background = null;
            expand.BorderBrush = null;           
            expand.Template = homeScreenObj.SearchPage.SearchButton.Template;
            expand.Cursor = Cursors.Hand;
            expand.Content = new Image()
            {
                Source = new BitmapImage(new Uri("/expand.png", UriKind.Relative)),
                VerticalAlignment = VerticalAlignment.Center,
                Stretch = Stretch.UniformToFill
            };
            expand.Click += ExpandButton_Click;

            TaskID = id;
        }
       

        public void ExpandButton_Click(object sedner, RoutedEventArgs e)
        {
            homeScreenObj.editWindow = new EditTask(TaskID);
            homeScreenObj.editWindow.Show();
        }

        public void InCompleted_Click(object sedner, RoutedEventArgs e)
        {
            outerBorder.Opacity = 0.1;
            cntrl.UpdateTaskCompletion(TaskID, "Not Completed");
        }

        public void completed_Click(object sedner, RoutedEventArgs e)
        {
            outerBorder.Opacity = 0.4;
            cntrl.UpdateTaskCompletion(TaskID, "Completed");
        }

    }
}
