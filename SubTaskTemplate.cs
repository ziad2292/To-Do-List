using DBapplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Expando;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Input;
using System.Security.Policy;

namespace ToDoList
{
    internal class SubTaskTemplate
    {
        public static bool order = true;
        public static int rightIndex = 0;
        public static int leftIndex = 0;
        Controller cntrl;
        public Grid outerGrid;
        Button check;
        Label subTaskName;
        HomeScreen homeScreenObj;
        bool completion;
        int subID;
        Image Tic;


        public SubTaskTemplate(string id, string name, string completionStatus)
        {
            cntrl = new Controller();
            homeScreenObj = (HomeScreen)Window.GetWindow(Application.Current.MainWindow);

            subID = Int32.Parse(id);

            outerGrid = new Grid();
            outerGrid.Name = "SubTaskGrid";
            outerGrid.Height = 40;
            outerGrid.Width = 250;

            Tic = new Image();
            Tic.Source = new BitmapImage(new Uri("/tic.png", UriKind.Relative));
            Tic.VerticalAlignment = VerticalAlignment.Top;
            Tic.HorizontalAlignment = HorizontalAlignment.Left;
            Tic.Stretch = Stretch.UniformToFill;
            outerGrid.Children.Add(Tic);
            Tic.Margin = new Thickness(3,5,0,0);
            Tic.Height = 30;
            Tic.Width = 40;
            Tic.Visibility = Visibility.Collapsed;
            Tic.Opacity = 0.5;

            check = new Button();
            outerGrid.Children.Add(check);
            check.HorizontalAlignment = HorizontalAlignment.Left;
            check.Margin = new Thickness(4,10,0,0);
            check.VerticalAlignment = VerticalAlignment.Center;
            check.Height = 30;
            check.Width = 40;
            check.Background = null;
            check.BorderBrush = null;
            check.Template = homeScreenObj.SearchPage.SearchButton.Template;
            check.Cursor = Cursors.Hand;
            check.MouseEnter += check_MouseEnter;
            check.MouseLeave += check_MouseLeave;
            check.Click += check_Click;
            if (completionStatus == "Completed")
            {
                completion = true;
                check.Content = new Image()
                {
                    Source = new BitmapImage(new Uri("/done.png", UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center,
                    Stretch = Stretch.UniformToFill
                };
            }
            else
            {
                completion = false;
                check.Content = new Image()
                {
                    Source = new BitmapImage(new Uri("/notDone.png", UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center,
                    Stretch = Stretch.UniformToFill
                };
            }

            subTaskName = new Label();
            outerGrid.Children.Add(subTaskName);
            subTaskName.HorizontalAlignment = HorizontalAlignment.Left;
            subTaskName.Margin = new Thickness(40,15,0,0);
            subTaskName.Height = 35;
            subTaskName.Width = 181;
            subTaskName.VerticalAlignment = VerticalAlignment.Center;
            subTaskName.Content = name;
            subTaskName.FontSize = 14;
            subTaskName.FontFamily = new FontFamily("Perpetua Titling MT");
            subTaskName.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
        }

        public void check_MouseEnter(object sedner, RoutedEventArgs e)
        {
            if (completion)
            {
                check.Content = new Image()
                {
                    Source = new BitmapImage(new Uri("/notDone.png", UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center,
                    Stretch = Stretch.UniformToFill
                };
            }
            else
            {
                Tic.Visibility = Visibility.Visible;
            }  
        }
        public void check_MouseLeave(object sedner, RoutedEventArgs e)
        {
            if (completion)
            {
                check.Content = new Image()
                {
                    Source = new BitmapImage(new Uri("/done.png", UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center,
                    Stretch = Stretch.UniformToFill
                };
            }
            else
            {
                check.Content = new Image()
                {
                    Source = new BitmapImage(new Uri("/notDone.png", UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center,
                    Stretch = Stretch.UniformToFill
                };
                Tic.Visibility = Visibility.Hidden;
            }          
        }
        public void check_Click(object sedner, RoutedEventArgs e)
        {
            if (completion)
            {
                completion = false;
                check.Content = new Image()
                {
                    Source = new BitmapImage(new Uri("/notDone.png", UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center,
                    Stretch = Stretch.UniformToFill
                };
                cntrl.UpdateSubTaskCompletion(subID, "Not Completed");
            }
            else
            {
                completion = true;
                check.Content = new Image()
                {
                    Source = new BitmapImage(new Uri("/done.png", UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center,
                    Stretch = Stretch.UniformToFill
                };
                cntrl.UpdateSubTaskCompletion(subID, "Completed");
            }
            Tic.Visibility = Visibility.Hidden;
        }
    }
}
