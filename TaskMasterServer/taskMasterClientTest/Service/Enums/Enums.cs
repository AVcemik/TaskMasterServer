using System.ComponentModel;

namespace taskMasterClientTest.Service.Enums
{
    enum RequestType
    {
        [Description("Application/Authorization")]
        Authorization,
        [Description("Application/AddUser")]
        AddUser,
        [Description("Application/AddTask")]
        AddTask,
        [Description("Application/AddDepartment")]
        AddDepartment
    }
    internal class Enums
    {
    }
}
