using NotificationAgent.UI.Abstract;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace NotificationAgent
{
    public class NotificationsCenter<TNotificationView, TDisplayConfigurator>
        where TNotificationView : GenericNotificationView, INotificationView
        where TDisplayConfigurator : IDisplayConfigurator<TNotificationView>, new()
    {
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

        #region Methods

        public void SetupNotificationViewsDesign(Color notificationColor, Color textColor, Stream notificationSound)
        {
            this.NotificationColor = notificationColor;
            this.TextColor = textColor;
            this.NotificationSound = notificationSound;
        }

        public void DisplayNotification(string title, string details, Image image)
        {
            var notification = (TNotificationView)Activator.CreateInstance(typeof(TNotificationView), NotificationSound, NotificationColor, TextColor);

            if (!displayManager.IsConfigured)
            {
                displayManager.SetupNotificationViewPositioning(notification.ClientRectangle);
            }

            displayManager.DisplayView(notification, title, details, image);
        }

        #endregion
    }
}
