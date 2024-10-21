using CommunityToolkit.Mvvm.Messaging.Messages;
using ToDoList.MAUI.Models;

namespace ToDoList.MAUI.Messages
{
    public enum ListAction
    {
        Add,
        Delete
    }

    public class TaskMessage
    {
        public TaskMessage(TaskModel data, ListAction action = ListAction.Add)
        {
            Data = data;
            Action = action;
        }
        public TaskModel Data { get; set; }
        public ListAction Action { get; set; }
    }

    public class MainPageMessage : ValueChangedMessage<TaskMessage>
    {
        public MainPageMessage(TaskMessage value) : base(value)
        {
        }
    }
}
