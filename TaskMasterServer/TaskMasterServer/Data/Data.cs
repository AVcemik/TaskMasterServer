namespace TaskMasterServer.Data
{
    internal class Data
    {
        private List<UserData> _users;
        private List<TaskData> _tasks;

        public Data()
        {
            _users = new List<UserData>();
            _tasks = new List<TaskData>();
        }
        public List<UserData> GetUsers()
        {
            return _users;
        }
        public List<TaskData> GetTasks()
        {
            return _tasks;
        }
        public void AddUser(UserData userData)
        {
            _users.Add(userData);
        }
        public void AddUsers(List<UserData> usersData)
        {
            _users = usersData;
        }
        public void AddTask(TaskData taskData)
        {
            _tasks.Add(taskData);
        }
        public void AddTasks(List<TaskData> tasksData)
        {
            _tasks = tasksData;
        }
    }
}
