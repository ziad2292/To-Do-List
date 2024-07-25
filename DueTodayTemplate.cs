using DBapplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace ToDoList
{
    internal class DueTodayTemplate
    {
        HomeScreen homeScreenObj;
        Controller cntrl;
        public Grid outerGrid;
        Border border;
        Viewbox viewBox;
        Grid innerGrid;
        TextBlock taskName;
        Border sliderBorder;
        TextBlock subTasks;
        Slider slider;
        TextBlock completionPercent;
        int taskID;
        ColumnDefinition col;
        RowDefinition row;
        static public int r = 1;
        double sliderValue;



        public DueTodayTemplate(string id, string name, string status, string completed, string subNum)
        {
            cntrl = new Controller();
            homeScreenObj = (HomeScreen)Window.GetWindow(Application.Current.MainWindow);

            taskID = Int32.Parse(id);

            outerGrid = new Grid();
            homeScreenObj.DueToday.Children.Add(outerGrid);
            Grid.SetRow(outerGrid, r++);
            Grid.SetColumnSpan(outerGrid, 2);

            border = new Border();
            outerGrid.Children.Add(border);
            border.CornerRadius = new CornerRadius(35, 35, 35, 35);
            border.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#253A4C");
            border.Background.Opacity = 0.42;
            border.BorderThickness = new Thickness(8, 8, 8, 8);

            viewBox = new Viewbox();
            border.Child = viewBox;
            viewBox.Margin = new Thickness(10, 0, 30, 0);

            innerGrid = new Grid();
            viewBox.Child = innerGrid;
            innerGrid.Width = 185;
            col = new ColumnDefinition();
            col.Width = new GridLength(1, GridUnitType.Star);
            innerGrid.ColumnDefinitions.Add(col);
            col = new ColumnDefinition();
            col.Width = new GridLength(84, GridUnitType.Star);
            innerGrid.ColumnDefinitions.Add(col);
            col = new ColumnDefinition();
            col.Width = new GridLength(93, GridUnitType.Star);
            innerGrid.ColumnDefinitions.Add(col);
            row = new RowDefinition();
            row.Height = new GridLength(10, GridUnitType.Star);
            innerGrid.RowDefinitions.Add(row);
            row = new RowDefinition();
            row.Height = new GridLength(21, GridUnitType.Star);
            innerGrid.RowDefinitions.Add(row);

            taskName = new TextBlock();
            innerGrid.Children.Add(taskName);
            taskName.Margin = new Thickness(0, 0, 0, 6);
            taskName.Text = name;
            taskName.HorizontalAlignment = HorizontalAlignment.Left;
            taskName.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
            taskName.FontFamily = new FontFamily("PMingLiU-ExtB");
            taskName.FontSize = 14;
            Grid.SetColumn(taskName, 1);
            Grid.SetRowSpan(taskName, 2);

            sliderBorder = new Border();
            innerGrid.Children.Add(sliderBorder);
            sliderBorder.Margin = new Thickness(-6, 10, -10, 10);
            sliderBorder.CornerRadius = new CornerRadius(10, 10, 10, 10);
            sliderBorder.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
            sliderBorder.Background.Opacity = 0.62;
            Grid.SetColumn(sliderBorder, 2);
            Grid.SetColumnSpan(sliderBorder, 2);
            Grid.SetRowSpan(sliderBorder, 2);

            slider = new Slider();
            sliderBorder.Child = slider;
            slider.Style = homeScreenObj.Resources["Horizontal_Slider"] as Style;
            slider.Maximum = 100;
            if (subNum == "0") {
                if (status == "Completed") sliderValue = 100;
                else sliderValue = 0;
            }
            else sliderValue = Double.Parse(completed) / Double.Parse(subNum) * 100;
            slider.Value = sliderValue;
            slider.Margin = new Thickness(2, 0, 3, 0);
            slider.IsEnabled = false;

            subTasks = new TextBlock();
            innerGrid.Children.Add(subTasks);
            subTasks.Text = subNum + " SubTasks";
            subTasks.FontFamily = new FontFamily("PMingLiU-ExtB");
            subTasks.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
            subTasks.FontSize = 10;
            subTasks.HorizontalAlignment = HorizontalAlignment.Left;
            subTasks.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(subTasks, 1);
            Grid.SetRow(subTasks, 1);

            completionPercent = new TextBlock();
            innerGrid.Children.Add(completionPercent);
            completionPercent.Text = ((int)sliderValue).ToString() + "%";
            completionPercent.HorizontalAlignment = HorizontalAlignment.Right;
            completionPercent.VerticalAlignment = VerticalAlignment.Center;
            completionPercent.Margin = new Thickness(0, 0, 5, 0);
            completionPercent.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#C3C3C3");
            completionPercent.FontFamily = new FontFamily("PMingLiU-ExtB");
            completionPercent.FontWeight = FontWeights.Bold;
            completionPercent.FontSize = 10;
            Grid.SetColumn(completionPercent, 2);
            Grid.SetRowSpan(completionPercent, 2);


        }
    }
}
