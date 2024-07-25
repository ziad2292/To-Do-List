using DBapplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Data;

namespace ToDoList
{
    internal class LateTasksTemplate
    {
        HomeScreen homeScreenObj;
        Controller cntrl;
        public Border border;
        Grid grid;
        TextBlock taskName;
        TextBlock remaining;
        TextBlock endDate;
        Button recover;
        Button delete;
        public static int doneIndex = 0;
        public static int lateIndex = 0;
        int taskID;
        string[] modifiedDate;
        static public int r = 1;
        ColumnDefinition col;


        public LateTasksTemplate(string id, string name, string date, string daysLate)
        {
            cntrl = new Controller();
            homeScreenObj = (HomeScreen)Window.GetWindow(Application.Current.MainWindow);

            taskID = Int32.Parse(id);

            border = new Border();
            border.Margin = new Thickness(8, 4, 8, 4);
            border.CornerRadius = new CornerRadius(15, 15, 15, 15);
            border.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#253A4C");
            border.Background.Opacity = 0.42;
            homeScreenObj.LateTasksGrid.Children.Add(border);
            Grid.SetRow(border, r++);
            Grid.SetColumnSpan(border, 2);

            grid = new Grid();
            border.Child = grid;
            col = new ColumnDefinition();
            col.Width = new GridLength(1, GridUnitType.Star);
            grid.ColumnDefinitions.Add(col);
            col = new ColumnDefinition();
            col.Width = new GridLength(1, GridUnitType.Star);
            grid.ColumnDefinitions.Add(col);
            col = new ColumnDefinition();
            col.Width = new GridLength(1, GridUnitType.Star);
            grid.ColumnDefinitions.Add(col);
            col = new ColumnDefinition();
            col.Width = new GridLength(1, GridUnitType.Star);
            grid.ColumnDefinitions.Add(col);
            


            taskName = new TextBlock();
            grid.Children.Add(taskName);
            taskName.Margin = new Thickness(10, 0, 0, 0);
            taskName.Text = name;
            taskName.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
            taskName.HorizontalAlignment = HorizontalAlignment.Left;
            taskName.VerticalAlignment = VerticalAlignment.Center;
            taskName.FontFamily = new FontFamily("PMingLiU-ExtB");
            taskName.FontSize = 17;
            Grid.SetColumn(taskName, 0);

            remaining = new TextBlock();
            grid.Children.Add(remaining);
            remaining.Margin = new Thickness(10, 0, 0, 0);
            remaining.Text = (Int32.Parse(daysLate)*(-1)).ToString() + " Days Late";
            remaining.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
            remaining.VerticalAlignment = VerticalAlignment.Center;
            remaining.FontFamily = new FontFamily("PMingLiU-ExtB");
            remaining.FontSize = 17;
            Grid.SetColumn(remaining, 1);


            endDate = new TextBlock();
            grid.Children.Add(endDate);
            endDate.Margin = new Thickness(10, 0, 0, 0);
            modifiedDate = date.Split(' ');
            endDate.Text = modifiedDate[0];
            endDate.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
            endDate.VerticalAlignment = VerticalAlignment.Center;
            endDate.FontFamily = new FontFamily("PMingLiU-ExtB");
            endDate.FontSize = 17;
            Grid.SetColumn(endDate, 2);

            recover = new Button();
            grid.Children.Add(recover);
            recover.Height = 30;
            recover.Margin = new Thickness(0, 0, 40, 0);
            recover.Width = 40;
            recover.Background = null;
            recover.BorderBrush = null;
            recover.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
            recover.Cursor = Cursors.Hand;
            recover.FontFamily = new FontFamily("PMingLiU-ExtB");
            recover.HorizontalAlignment = HorizontalAlignment.Right;
            recover.Template = homeScreenObj.ChangeNameButton.Template;
            recover.Content = new Image()
            {
                Source = new BitmapImage(new Uri("/recoverC3.png", UriKind.Relative)),
                VerticalAlignment = VerticalAlignment.Center,
                Stretch = Stretch.Uniform
            };
            recover.Click += recover_Click;
            Grid.SetColumn(recover, 3);

            delete = new Button();
            grid.Children.Add(delete);
            delete.Height = 30;
            delete.Margin = new Thickness(0, 0, 0, 0);
            delete.Width = 40;
            delete.Background = null;
            delete.BorderBrush = null;
            delete.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
            delete.Cursor = Cursors.Hand;
            delete.FontFamily = new FontFamily("PMingLiU-ExtB");
            delete.HorizontalAlignment = HorizontalAlignment.Right;
            delete.Template = homeScreenObj.ChangeNameButton.Template;
            delete.Content = new Image()
            {
                Source = new BitmapImage(new Uri("/deleteC3.png", UriKind.Relative)),
                VerticalAlignment = VerticalAlignment.Center,
                Stretch = Stretch.Uniform
            };
            delete.Click += delete_Click;
            Grid.SetColumn(delete, 3);



        }

        public void recover_Click(object sedner, RoutedEventArgs e)
        {
            cntrl.UpdateEndDate(taskID);
            homeScreenObj.Title.Content = "Home";
            homeScreenObj.constructor();
            homeScreenObj.hideSubMenus();
            homeScreenObj.closePage();
        }

        public void delete_Click(object sedner, RoutedEventArgs e)
        {
            cntrl.DeleteTask(taskID);
            homeScreenObj.Title.Content = "Home";
            homeScreenObj.constructor();
            homeScreenObj.hideSubMenus();
            homeScreenObj.closePage();
        }

    }
}
