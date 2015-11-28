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

        public async Task SetupNotificationViewsDesign(Color notificationColor, Color textColor, Stream notificationSound)
        {
            await Task.Run(() =>
            {
                this.NotificationColor = notificationColor;
                this.TextColor = textColor;
                this.NotificationSound = notificationSound;
            });
        }

        public async Task DisplayNotification(string title, string details, Image image)
        {
            var notification = (TNotificationView)Activator.CreateInstance(typeof(TNotificationView), new object[] { title, details, image });

            if (!displayManager.IsConfigured)
            {
                await displayManager.SetupNotificationViewPositioning(notification.DisplayRectangle);
            }

            await displayManager.DisplayView(notification, title, details, image);
        }

        #endregion
    }
}
