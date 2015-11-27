using NotificationAgent.UI.Abstract;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Linq;

namespace NotificationAgent.UI.DisplayManagers
{
    public class NotificationPopupDisplayConfigurator<TNotificationView> : IDisplayConfigurator<TNotificationView>
        where TNotificationView : GenericNotificationView, INotificationView
    {
        #region Fields

        private Queue<TNotificationView> queuedNotificationViews = new Queue<TNotificationView>();
        private TNotificationView[] activeNotificationViews = default(TNotificationView[]);

        #endregion

        internal NotificationPopupDisplayConfigurator()
        {
        }

        #region Properties

        private int NotificationPositionX { get; set; }

        private int NotificationViewHeight { get; set; }

        public bool IsConfigured { get; set; } = false;

        #endregion

        #region Configuration

        public async Task SetupNotificationViewPositioning(Rectangle notificationViewArea)
        {
            await Task.Run(() =>
            {
                var screenWorkingArea = Screen.PrimaryScreen.WorkingArea;
                var rightMarginPointX = screenWorkingArea.X + screenWorkingArea.Width;
                var spacingFromRightMargin = notificationViewArea.Width / 4;

                NotificationPositionX = rightMarginPointX - notificationViewArea.Width - spacingFromRightMargin;

                var maximumActivePopups = screenWorkingArea.Height / notificationViewArea.Height;
                activeNotificationViews = new TNotificationView[maximumActivePopups];

                IsConfigured = true;
            });
        }

        #endregion

        #region View positioning

        public async Task<bool> HasScreenAvailableSpace()
        {
            return await new Task<bool>(() => {
                return activeNotificationViews.Any(v => v == null);
            });
        }

        public async Task DisplayView(TNotificationView view)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
