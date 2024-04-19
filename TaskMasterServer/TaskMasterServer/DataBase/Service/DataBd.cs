using TaskBD = TaskMasterServer.DataBase.Task;
namespace TaskMasterServer.DataBase
{
    internal static  class DataBd
    {
        private static List<Department> _departments = new();
        private static List<Attachment> _attachments = new();
        private static List<Priority> _priorities = new();
        private static List<Status> _statuses = new();
        private static List<User> _users = new();
        private static List<TaskBD> _tasks = new();
        private static List<Authorization> _authorizations = new();
        private static List<Comment> _comments = new();


        public static List<TaskBD> ReadTask()
        {
            return _tasks;
        }
        public static List<User> ReadUser()
        {
            return _users;
        }
        public static List<Department> ReadDepartment()
        {
            return _departments;
        }
        public static List<Attachment> ReadAttachment()
        {
            return _attachments;
        }
        public static List<Priority> ReadPriority()
        {
            return _priorities;
        }
        public static List<Status> ReadStatuses()
        {
            return _statuses;
        }
        public static List<Authorization> ReadAuthorization()
        {
            return _authorizations;
        }
        public static List<Comment> ReadComments()
        {
            return _comments;
        }
        public static void UpdateTempBD()
        {
            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
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
    }
}
