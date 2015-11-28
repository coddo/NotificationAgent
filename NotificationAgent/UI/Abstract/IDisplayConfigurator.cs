using System.Drawing;
using System.Threading.Tasks;

namespace NotificationAgent.UI.Abstract
{
    public interface IDisplayConfigurator<TNotificationView> where TNotificationView : GenericNotificationView, INotificationView
    {
        #region Configuration

        bool IsConfigured { get; set; }

        void SetupNotificationViewPositioning(Rectangle notificationViewArea);

        #endregion

        #region View positioning

        void DisplayView(TNotificationView view, string title, string description, Image image);

        #endregion
    }
}
