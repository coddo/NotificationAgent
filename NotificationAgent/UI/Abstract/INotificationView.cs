using System.Drawing;
using System.Threading.Tasks;

namespace NotificationAgent.UI.Abstract
{
    public interface INotificationView
    {
        int Index { get; set; }

        bool IsEqual(INotificationView view);

        void ShowNotification(string title, string description, Image image);

        void HideNotification();
    }
}
