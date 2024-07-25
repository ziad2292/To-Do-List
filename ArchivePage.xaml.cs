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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Markup;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for ArchivePage.xaml
    /// </summary>
    public partial class ArchivePage : Page
    {
        Controller cntrl;
        public ArchivePage()
        {
            InitializeComponent();
            cntrl = new Controller();
            setTasks();
        }

        DataTable queryResult;
        int numOfArchive;
        public void setTasks()
        {
            queryResult = cntrl.SelectAllArchiveTasks();
            numOfArchive = cntrl.SelectAllArchiveTasksCount();
            displayArchive(queryResult, numOfArchive);
        }

        ArchiveTemplate temp;
        bool status;
        public void displayArchive(DataTable queryResult, int numOfArchive)
        {
            ArchiveTemplate.doneIndex = 0;
            ArchiveTemplate.lateIndex = 0;
            for (int i = 0; i < numOfArchive; i++)
            {
                temp = new ArchiveTemplate(queryResult.Rows[i][0].ToString(), queryResult.Rows[i][1].ToString(), queryResult.Rows[i][3].ToString());
                if (queryResult.Rows[i][4].ToString() == "Completed") status = true;
                else status = false;
                if (status)
                {
                    DonePanel.Children.Insert(ArchiveTemplate.doneIndex++, temp.border);
                }
                else
                {
                    LatePanel.Children.Insert(ArchiveTemplate.lateIndex++, temp.border);
                }
        }
        }
    }
}
