using TaskMasterServer.Data;
using TaskMasterServer.Service.Business.CRUD;
using TaskBD = TaskMasterServer.DataBase.Task;
namespace TaskMasterServer.DataBase
{
    internal static  class DataBd
    {
        private static Data.Data _data = new();
        private static List<Department> _departments = [];
        private static List<Attachment> _attachments = [];
        private static List<Priority> _priorities = [];
        private static List<Status> _statuses = [];
        private static List<User> _users = [];
        private static List<TaskBD> _tasks = [];
        private static List<Authorization> _authorizations = [];
        private static List<Comment> _comments = [];


        public static Data.Data ReadData() { return _data; }
        public static List<TaskBD> ReadTask() { return _tasks; }
        public static List<User> ReadUser() { return _users; }
        public static List<Department> ReadDepartment() { return _departments; }
        public static List<Attachment> ReadAttachment() { return _attachments; }
        public static List<Priority> ReadPriority() { return _priorities; }
        public static List<Status> ReadStatuses() { return _statuses; }
        public static List<Authorization> ReadAuthorization() { return _authorizations; }
        public static List<Comment> ReadComments() { return _comments; }
        public static void UpdateTempBD()
        {
            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                ClearData();
                ClearTempBD();
                _departments = dbContext.Departments!.ToList();
                _attachments = dbContext.Attachments!.ToList();
                _priorities = dbContext.Priorities!.ToList();
                _statuses = dbContext.Statuses!.ToList();
                _users = dbContext.Users!.ToList();
                _tasks = dbContext.Tasks!.ToList();
                _authorizations = dbContext.Authorizations!.ToList();
                _comments = dbContext.Comments!.ToList();
            }
            ConvertationBdToData();
        }
        private static void ConvertationBdToData()
        {
            foreach (var item in _users)
            {
                _data.Users.Add(item.ConvertToData());
            }
            foreach (var item in _tasks)
            {
                _data.Tasks.Add(item.ConvertToData());
            }
            foreach (var item in _departments)
            {
                _data.Departments.Add(item.ConvertToData());
            }
            foreach (var item in _statuses)
            {
                _data.Statuses.Add(item.ConvertToData());
            }
            foreach (var item in _priorities)
            {
                _data.Priorities.Add(item.ConvertToData());
            }

        }
        private static void ClearTempBD()
        {
            _departments.Clear();
            _attachments.Clear();
            _priorities.Clear();
            _statuses.Clear();
            _users.Clear();
            _tasks.Clear();
            _authorizations.Clear();
            _comments.Clear();
        }
        private static void ClearData()
        {
            _data.Users.Clear();
            _data.Tasks.Clear();
            _data.Departments.Clear();
            _data.Statuses.Clear();
            _data.Priorities.Clear();
        }
        public static void CheckStatusTask()
        {
            string statusOverdue = "Просрочена";
            List<TaskBD> updateTaskDb = new();

            if (!_statuses.Any(s => s.StatusType == statusOverdue))
            {
                Status status = new() { StatusType = statusOverdue };
                using (TaskUser_dbContext dbContext = new())
                {
                    dbContext.Add(status);
                    dbContext.SaveChanges();
                    _statuses = dbContext.Statuses!.ToList();
                }
            }

            int statusOverdueId = _statuses.Where(s => s.StatusType == statusOverdue).First().StatusId;

            foreach (var task in _tasks)
            {
                if (DateTime.Now > task.Deadline)
                {
                    task.StatusId = statusOverdueId;
                    task.Status = _statuses.Where(s => s.StatusType == statusOverdue).First();
                    updateTaskDb.Add(task);
                }
            }
            if (updateTaskDb.Count >= 0)
            {
                using (TaskUser_dbContext dbContext = new())
                {
                    dbContext.UpdateRange(updateTaskDb);
                    dbContext.SaveChanges();
                }
            }
            UpdateTempBD();
        }
    }
}
