using DBapplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Expando;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Markup;

namespace ToDoList
{
    internal class ArchiveTemplate
    {
        HomeScreen homeScreenObj;
        Controller cntrl;
        public Border border;
        Grid grid;
        TextBlock taskName;
        TextBlock endDate;
        Button recover;
        Button delete;
        public static int doneIndex = 0;
        public static int lateIndex = 0;
        int taskID;
        string[] modifiedDate;

        public ArchiveTemplate(string id, string name, string date)
        {
            cntrl = new Controller();
            homeScreenObj = (HomeScreen)Window.GetWindow(Application.Current.MainWindow);

            taskID = Int32.Parse(id);

            border = new Border();
            border.Height = 40;
            border.Margin = new Thickness(8,4,8,4);
            border.CornerRadius = new CornerRadius(15, 15, 15, 15);
            border.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#253A4C");
            border.Background.Opacity = 0.42;

            grid = new Grid();
            border.Child = grid;

            taskName = new TextBlock();
            grid.Children.Add(taskName);
            taskName.Margin = new Thickness(5, 5, 263, 5);
            taskName.HorizontalAlignment = HorizontalAlignment.Left;
            taskName.Text = name;
            taskName.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
            taskName.Width = 80;
            taskName.Height = 30;
            taskName.FontFamily = new FontFamily("PMingLiU-ExtB");
            taskName.FontSize = 15;
            taskName.Padding = new Thickness(20, 5, 0, 0);

            endDate = new TextBlock();
            grid.Children.Add(endDate);
            endDate.Margin = new Thickness(136, 5, 137, 5);
            modifiedDate = date.Split(' ');
            endDate.Text = modifiedDate[0];
            endDate.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
            endDate.Width = 80;
            endDate.Height = 30;
            endDate.FontFamily = new FontFamily("PMingLiU-ExtB");
            endDate.FontSize = 15;
            endDate.Padding = new Thickness(20, 5, 0, 0);

            recover = new Button();
            grid.Children.Add(recover);
            recover.Height = 30;
            recover.Margin = new Thickness(320, 5, 0, 5);
            recover.Width = 40;
            recover.Background = null;
            recover.BorderBrush = null;
            recover.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
            recover.Cursor = Cursors.Hand;
            recover.FontFamily = new FontFamily("PMingLiU-ExtB");
            recover.Template = homeScreenObj.ChangeNameButton.Template;
            recover.Content = new Image()
            {
                Source = new BitmapImage(new Uri("/recoverC3.png", UriKind.Relative)),
                VerticalAlignment = VerticalAlignment.Center,
                Stretch = Stretch.Uniform
            };
            recover.Click += recover_Click;

            delete = new Button();
            grid.Children.Add(delete);
            delete.Height = 30;
            delete.Margin = new Thickness(380, 0, 0, 0);
            delete.Width = 40;
            delete.Background = null;
            delete.BorderBrush = null;
            delete.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
            delete.Cursor = Cursors.Hand;
            delete.FontFamily = new FontFamily("PMingLiU-ExtB");
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
            homeScreenObj.closePage();
            homeScreenObj.openPage(new ArchivePage());
        }

        public void delete_Click(object sedner, RoutedEventArgs e)
        {
            cntrl.DeleteTask(taskID);
            homeScreenObj.closePage();
            homeScreenObj.openPage(new ArchivePage());
        }
    }
}
