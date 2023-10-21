using CommunityToolkit.Mvvm.Messaging.Messages;
using ToDoList.MAUI.Models;

namespace ToDoList.MAUI.Messages
{
    public enum ToDoAction
    {
        Add,
        Delete
    }
    public class ToDoTaskMessage
    {
        public ToDoTaskMessage(ToDoTaskModel data, ToDoAction action = ToDoAction.Add)
        {
            Data = data;
            Action = action;
        }
        public ToDoTaskModel Data { get; set; }
        public ToDoAction Action { get; set; }
    }
    public class MainPageMessage : ValueChangedMessage<ToDoTaskMessage>
    {
        public MainPageMessage(ToDoTaskMessage value) : base(value)
        {
        }
    }
}
