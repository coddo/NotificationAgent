using NotificationAgent.UI.Abstract;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace NotificationAgent
{
    public class NotificationsCenter<TNotificationView, TDisplayConfigurator>
        where TNotificationView : GenericNotificationView, INotificationView
        where TDisplayConfigurator : IDisplayConfigurator<TNotificationView>, new()
    {
        #region Private constants

        private const string NOT_CONFIGURED_EXCEPTION_MESSAGE = "The behavior of the popups hasn't been configured!";

        #endregion

        #region Fields

        private TDisplayConfigurator displayManager;

        #endregion

        #region Constructors

        public NotificationsCenter()
        {
            this.displayManager = new TDisplayConfigurator();
        }

        #endregion

        #region Properties

        private Stream NotificationSound { get; set; }

        private Color NotificationColor { get; set; } = Color.AliceBlue;

        private Color TextColor { get; set; } = Color.Black;

        #endregion

        #region configuration methods

        public async Task SetupNotificationViewsDesign(Color notificationColor, Color textColor, Stream notificationSound)
        {
            await Task.Run(() =>
            {
                NotificationColor = notificationColor;
                TextColor = textColor;
                NotificationSound = notificationSound;
            });
        }

        #endregion
    }
}
