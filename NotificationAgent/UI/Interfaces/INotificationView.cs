using System.Drawing;

namespace NotificationAgent.UI.Interfaces
{
    public interface INotificationView
    {
        void DisplayNotification(string message, string details, Image image);

        void CloseNotification();
    }
}
