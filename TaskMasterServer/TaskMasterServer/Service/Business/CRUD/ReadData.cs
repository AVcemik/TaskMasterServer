﻿using TaskMasterServer.Data;
using TaskMasterServer.DataBase;
using Task = TaskMasterServer.DataBase.Task;

namespace TaskMasterServer.Service.Business.CRUD
{
    internal static class ReadData
    {
        public static Data.Data GetData(UserData currentUser)
        {
            Data.Data currentData = new();
            Data.Data data = DataBd.ReadData();

            List<TaskData> tasks = [];
            List<Department> departments = [];
            List<PriorityData> priorities = [];
            List<StatusData> statuses = [];

            currentData.Statuses = data.Statuses;
            currentData.Priorities = data.Priorities;
            currentData.Departments = data.Departments;

            if (currentUser.IsAdmin == true)
            {
                currentData.Tasks = data.Tasks;
                currentData.Users = data.Users;
                currentData.Users.Add(currentUser);
            }
            else
            {
                currentData.Tasks = data.Tasks.Where(t => t.Department == currentUser.Department).ToList();
                currentData.Users.Add(currentUser);
            }

            return currentData;
        }
    }
}
