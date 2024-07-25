using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace DBapplication
{
    public class Controller
    {
        DBManager dbMan;

        static int id;
        public Controller()
        {
            dbMan = new DBManager();
        }

        public void setID(int studentid)
        {
            Controller.id = studentid;
        }

        public int getID()
        {
            return id;
        }



        //Insert Statements

        public int InsertTask(string TaskName, string StartDate, string EndDate, int PriorityLevel, string DescriptionText, string TaskType)
        {
            TaskName = TaskName.Replace("'", "''");
            TaskName = char.ToUpper(TaskName[0]) + TaskName.Substring(1);
            DescriptionText = DescriptionText.Replace("'", "''");
            string query = "INSERT INTO Tasks(TaskName, StartDate, EndDate, PriorityLevel, DescriptionText, TaskType)" +
                "VALUES( '" + TaskName + "', '" + StartDate + "',  '" + EndDate + "', " + PriorityLevel + ", '" + DescriptionText + "', '" + TaskType + "');";
            return dbMan.ExecuteNonQuery(query);
        }

        public int InsertSubTask(string subTaskName, int TaskID)
        {
            subTaskName = subTaskName.Replace("'", "''");
            subTaskName = char.ToUpper(subTaskName[0]) + subTaskName.Substring(1);
            string query = "INSERT INTO subTask(subTaskName, TaskID)" +
                "VALUES( '" + subTaskName + "', '" + TaskID + "');";
            return dbMan.ExecuteNonQuery(query);
        }

        





        //Delete Statements
        public int DeleteAll()
        {
            string query = "DELETE FROM Tasks";
            return dbMan.ExecuteNonQuery(query);
        }

        public int DeleteTask(int ID)
        {
            string query = "DELETE FROM Tasks WHERE TaskID = " + ID;
            return dbMan.ExecuteNonQuery(query);

        }




        //Select Statements
        public DataTable SelectAllTasks()
        {
            string query = "SELECT * FROM Tasks WHERE Duration >= 0;";
            return dbMan.ExecuteReader(query);
        }
        public int SelectAllTasksCount()
        {
            string query = "SELECT count(*) FROM Tasks WHERE Duration >= 0;";
            return (int)dbMan.ExecuteScalar(query);
        }


        public DataTable SelectTask(string taskName)
        {
            string query = "SELECT * FROM Tasks WHERE TaskName LIKE '%" + taskName + "%';";
            return dbMan.ExecuteReader(query);
        }
        public int SelectTaskCount(string taskName)
        {
            string query = "SELECT COUNT(*) FROM Tasks WHERE TaskName LIKE '%" + taskName + "%';";
            return (int)dbMan.ExecuteScalar(query);
        }


        public DataTable SelectTaskRemainingDaysSorted()
        {
            string query = "SELECT * FROM Tasks WHERE Duration >= 0 ORDER BY (DATEDIFF(DAY, EndDate, CURRENT_TIMESTAMP)) DESC ;";
            return dbMan.ExecuteReader(query);
        }


        public DataTable SelectTaskPrioritySorted()
        {
            string query = "SELECT * FROM Tasks WHERE Duration >= 0 ORDER BY PriorityLevel DESC ;";
            return dbMan.ExecuteReader(query);
        }


        public DataTable SelectTaskByType(string taskType)
        {
            string query = "SELECT * FROM Tasks WHERE TaskType = '" + taskType + "' AND Duration >= 0;";
            return dbMan.ExecuteReader(query);
        }
        public int SelectTaskByTypeCount(string taskType)
        {
            string query = "SELECT COUNT(*) FROM Tasks WHERE TaskType = '" + taskType + "' AND Duration >= 0;";
            return (int)dbMan.ExecuteScalar(query);
        }


        public DataTable SelectTaskByTypePrioritySorted(string taskType)
        {
            string query = "SELECT * FROM Tasks WHERE TaskType = '" + taskType + "' AND Duration >= 0 ORDER BY PriorityLevel DESC ;";
            return dbMan.ExecuteReader(query);
        }

        public DataTable SelectTaskByTypeRemainingDaysSorted(string taskType)
        {
            string query = "SELECT * FROM Tasks WHERE TaskType = '" + taskType + "' AND Duration >= 0 ORDER BY (DATEDIFF(DAY, EndDate, CURRENT_TIMESTAMP)) ASC ;";
            return dbMan.ExecuteReader(query);
        }


        public string SelectUserName()
        {
            string query = "SELECT UserName FROM Profile;";

            return (string)dbMan.ExecuteScalar(query);
        }

        public string SelectPhotoPath()
        {
            string query = "SELECT PicturePath FROM Profile;";

            return (string)dbMan.ExecuteScalar(query);
        }

        public DataTable SelectTaskByID(int taskID)
        {
            string query = "SELECT * FROM Tasks WHERE TaskID = " + taskID.ToString() + " AND Duration >= 0;";
            return dbMan.ExecuteReader(query);
        }

        public DataTable SelectSubTasks(int taskID)
        {
            string query = "SELECT * FROM subTask WHERE TaskID = " + taskID.ToString() + ";";
            return dbMan.ExecuteReader(query);
        }

        public int SelectSubTasksCount(int taskID)
        {
            string query = "SELECT Count(*) FROM subTask WHERE TaskID = " + taskID.ToString() + ";";
            return (int)dbMan.ExecuteScalar(query);
        }

        public int SelectCompletedSubTasksCount(int taskID)
        {
            string query = "SELECT Count(*) FROM subTask WHERE TaskID = " + taskID.ToString() + " AND CompletionStatus = 'Completed';";
            return (int)dbMan.ExecuteScalar(query);
        }

        public int SelectLastTask()
        {
            string query = "SELECT MAX(TaskID) FROM Tasks;";
            return (int)dbMan.ExecuteScalar(query);
        }

        public DataTable SelectAllArchiveTasks()
        {
            string query = "SELECT * FROM Tasks WHERE Duration < 0";
            return dbMan.ExecuteReader(query);
        }
        public int SelectAllArchiveTasksCount()
        {
            string query = "SELECT count(*) FROM Tasks WHERE Duration < 0";
            return (int)dbMan.ExecuteScalar(query);
        }

        public DataTable SelectDistinctDates()
        {
            string query = "SELECT DISTINCT EndDate FROM Tasks WHERE Duration >= 0;";
            return dbMan.ExecuteReader(query);
        }

        public int SelectDistinctDatesCount()
        {
            string query = "SELECT count(DISTINCT EndDate) FROM Tasks WHERE Duration >= 0;";
            return (int)dbMan.ExecuteScalar(query);
        }

        public DataTable SelectDueTodayTasks()
        {
            string query = "SELECT * FROM Tasks WHERE DATEPART(day, EndDate) = DATEPART(day, CURRENT_TIMESTAMP);";
            return dbMan.ExecuteReader(query);
        }

        public int SelectDueTodayTasksCount()
        {
            string query = "SELECT count(*) FROM Tasks WHERE DATEPART(day, EndDate) = DATEPART(day, CURRENT_TIMESTAMP);";
            return (int)dbMan.ExecuteScalar(query);
        }

        public int SelectAllCompletedTasksCount()
        {
            string query = "SELECT count(*) FROM Tasks WHERE Duration >= 0 AND CompletionStatus = 'Completed';";
            return (int)dbMan.ExecuteScalar(query);
        }

        public int SelectAllUnfinishedTasksCount()
        {
            string query = "SELECT count(*) FROM Tasks WHERE Duration >= 0 AND CompletionStatus = 'Not Completed';";
            return (int)dbMan.ExecuteScalar(query);
        }

        public DataTable SelectCompletedTaskPrioritySorted()
        {
            string query = "SELECT * FROM Tasks WHERE Duration >= 0 AND CompletionStatus = 'Not Completed' ORDER BY PriorityLevel DESC ;";
            return dbMan.ExecuteReader(query);
        }

        public int SelectCompletedTaskPrioritySortedCount()
        {
            string query = "SELECT count(*) FROM Tasks WHERE Duration >= 0 AND CompletionStatus = 'Not Completed';";
            return (int)dbMan.ExecuteScalar(query);
        }

        public DataTable SelectAllLateArchiveTasks()
        {
            string query = "SELECT * FROM Tasks WHERE Duration < 0 AND CompletionStatus = 'Not Completed' ORDER BY Duration ASC";
            return dbMan.ExecuteReader(query);
        }

        public int SelectAllLateArchiveTasksCount()
        {
            string query = "SELECT count(*) FROM Tasks WHERE Duration < 0 AND CompletionStatus = 'Not Completed'";
            return (int)dbMan.ExecuteScalar(query);
        }





        //Update Statements
        public int UpdateUserName(string UserName)
        {
            UserName = UserName.Replace("'", "''");
            string query = "UPDATE Profile SET UserName = '" + UserName + "'";
            return dbMan.ExecuteNonQuery(query);
        }

        public int UpdatePath(string PicturePath)
        {
            string query = "UPDATE Profile SET PicturePath = '" + PicturePath + "'";
            return dbMan.ExecuteNonQuery(query);
        }

        public int UpdateTask(string taskID, string taskName, string startDate, string endDate, int priority, string description, string type)
        {
            taskName = taskName.Replace("'", "''");
            description = description.Replace("'", "''");
            string query = "UPDATE Tasks SET TaskName = '" + taskName + "', StartDate = '" + startDate + "', EndDate = '" + endDate + "', PriorityLevel = '" + priority + "', DescriptionText = '" + description + "', TaskType = '" + type + "' WHERE TaskID = " + taskID;
            return dbMan.ExecuteNonQuery(query);
        }

        public int UpdateTaskCompletion(int taskID, string completion)
        {
            string query = "UPDATE Tasks SET CompletionStatus = '" + completion + "' WHERE TaskID = " + taskID;
            return dbMan.ExecuteNonQuery(query);
        }

        public int UpdateSubTaskCompletion(int subTaskID, string completion)
        {
            string query = "UPDATE subTask SET CompletionStatus = '" + completion + "' WHERE subTaskID = " + subTaskID;
            return dbMan.ExecuteNonQuery(query);
        }

        public int UpdateEndDate(int TaskID)
        {
            string query = "UPDATE Tasks SET EndDate = DATEADD(day, 5, CURRENT_TIMESTAMP) WHERE TaskID = " + TaskID;
            return dbMan.ExecuteNonQuery(query);
        }





        public void TerminateConnection()
        {
            dbMan.CloseConnection();
        }
    }
}
