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
        AddDepartment,
        [Description("Application/AddPrioritet")]
        AddPrioritet,
        [Description("Application/AddStatus")]
        AddStatus,
        [Description("Application/ReadData")]
        ReadData,
        [Description("Application/UpdateUser")]
        UpdateUser,
        [Description("Application/UpdateTask")]
        UpdateTask,
        [Description("Application/UpdateDepartment")]
        UpdateDepartment,
        [Description("Application/UpdatePrioritet")]
        UpdatePrioritet,
        [Description("Application/AddStatus")]
        UpdateStatus
    }
    internal class Enums
    {
    }
}
