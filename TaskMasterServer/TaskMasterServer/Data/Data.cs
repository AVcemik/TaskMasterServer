namespace TaskMasterServer.Data
{
    internal class Data
    {
        public List<UserData> Users;
        public List<TaskData> Tasks;
        public List<DepartmentData> Departments;
        public List<PriorityData> Priorities;
        public List<StatusData> Statuses;

        public Data()
        {
            Users = new List<UserData>();
            Tasks = new List<TaskData>();
            Departments = new List<DepartmentData>();
            Priorities = new List<PriorityData>();
            Statuses = new List<StatusData>();
        }
        //public List<UserData> GetUsers()
        //{
        //    return _users;
        //}
        //public List<TaskData> GetTasks()
        //{
        //    return _tasks;
        //}
        //public List<DepartmentData> GetDepartments()
        //{
        //    return _departments;
        //}
        //public void AddUser(UserData userData)
        //{
        //    _users.Add(userData);
        //}
        //public void AddUsers(List<UserData> usersData)
        //{
        //    _users = usersData;
        //}
        //public void AddTask(TaskData taskData)
        //{
        //    _tasks.Add(taskData);
        //}
        //public void AddTasks(List<TaskData> tasksData)
        //{
        //    _tasks = tasksData;
        //}
        //public void AddDepatment(DepartmentData departmentData)
        //{
        //    _departments.Add(departmentData);
        //}
        //public void AddDepartments(List<DepartmentData> departmentData)
        //{
        //    _departments = departmentData;
        //}
        //public void AddPriority(PriorityData priorityData)
        //{

        //}
    }
}
