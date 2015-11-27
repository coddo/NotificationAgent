using System.Drawing;
using System.Threading.Tasks;

namespace NotificationAgent.UI.Abstract
{
    public interface INotificationView
    {
        Task Show(string message, string details, Image image);

        Task Hide();
    }
}
