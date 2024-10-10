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
        [Description("Application/UpdateStatus")]
        UpdateStatus,
        [Description("Application/DeleteUser")]
        DelteUser,
        [Description("Application/DeleteTask")]
        DeleteTask,
        [Description("Application/DeleteDepartment")]
        DeleteDepartment,
        [Description("Application/DeletePrioritet")]
        DeletePrioritet,
        [Description("Application/DeleteStatus")]
        DeleteStatus
    }
    internal class Enums
    {
    }
}
