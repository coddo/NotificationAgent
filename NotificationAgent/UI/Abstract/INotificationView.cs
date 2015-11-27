using System.Drawing;
using System.Threading.Tasks;

namespace NotificationAgent.UI.Abstract
{
    public interface INotificationView
    {
        Task Show(string title, string description, Image image);

        Task Hide();
    }
}
