using CommunityToolkit.Mvvm.Messaging.Messages;
using ToDoList.MAUI.Models;

namespace ToDoList.MAUI.Messages
{
    public enum ListAction
    {
        Add,
        Delete
    }
    public class ToDoTaskMessage
    {
        public ToDoTaskMessage(ToDoTaskModel data, ListAction action = ListAction.Add)
        {
            Data = data;
            Action = action;
        }
        public ToDoTaskModel Data { get; set; }
        public ListAction Action { get; set; }
    }
    public class MainPageMessage : ValueChangedMessage<ToDoTaskMessage>
    {
        public MainPageMessage(ToDoTaskMessage value) : base(value)
        {
        }
    }
}
