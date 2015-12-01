using NotificationAgent.UI.Abstract;
using System;
using System.Drawing;
using System.IO;

namespace NotificationAgent
{
    public class NotificationsCenter<TNotificationView> where TNotificationView : GenericNotificationView, INotificationView
    {
        #region Fields

        private IDisplayConfigurator<TNotificationView> displayManager;

        #endregion

        #region Constructors

        public NotificationsCenter(IDisplayConfigurator<TNotificationView> displayManager)
        {
            this.displayManager = displayManager;
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
