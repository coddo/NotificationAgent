using System.Drawing;

namespace NotificationAgent.UI.Abstract
{
    public interface INotificationView
    {
        void DisplayNotification(string message, string details, Image image);

        void CloseNotification();
    }
}
