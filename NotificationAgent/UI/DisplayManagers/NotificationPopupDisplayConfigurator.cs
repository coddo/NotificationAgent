using NotificationAgent.UI.Abstract;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using Timer = System.Timers.Timer;
using NotificationAgent.UI.Forms;

namespace NotificationAgent.UI.DisplayManagers
{
    public class NotificationPopupDisplayConfigurator : IDisplayConfigurator<NotificationPopup>
    {
        #region Constants

        private const int TIMER_ITERATION_INTERVAL = 100;

        private const int TIMER_CLOSE_VIEW_INTERVAL = 1000;

        #endregion

        #region Fields

        private Queue<QueueItem> queuedNotificationViews = new Queue<QueueItem>();
        private NotificationPopup[] activeNotificationViews = default(NotificationPopup[]);

        private Timer iterationTimer;

        #endregion

        public NotificationPopupDisplayConfigurator()
        {
            iterationTimer = new Timer(TIMER_ITERATION_INTERVAL);

            iterationTimer.Elapsed += IterateAndShowViewsTimerCallback;
            iterationTimer.Enabled = true;
            iterationTimer.Start();
        }

        #region Positioning properties

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
                activeNotificationViews = new NotificationPopup[maximumActivePopups];

                IsConfigured = true;
            });
        }

        #endregion

        #region View positioning

        public async Task<bool> HasScreenAvailableSpace()
        {
            return await new Task<bool>(() =>
            {
                return activeNotificationViews.Any(v => v == null);
            });
        }

        public async Task DisplayView(NotificationPopup view, string title, string description, Image image)
        {
            await Task.Run(() =>
            {
                queuedNotificationViews.Enqueue(new QueueItem
                {
                    View = view,
                    Title = title,
                    Description = description,
                    Image = image
                });

                var closeViewTimer = new Timer(TIMER_CLOSE_VIEW_INTERVAL);

                closeViewTimer.Elapsed += delegate
                {
                    CloseActiveViewTimerCallback(view);
                };

                closeViewTimer.Enabled = true;
                closeViewTimer.Start();
            });
        }

        #endregion

        #region Timer callbacks

        private async void IterateAndShowViewsTimerCallback(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (queuedNotificationViews.Count == 0)
                return;

            var queueItem = queuedNotificationViews.Dequeue();
            for (int i = 0; i < activeNotificationViews.Length; i++)
            {
                queueItem.View.Index = i;
                activeNotificationViews[i] = queueItem.View;

                await queueItem.View.ShowNotification(queueItem.Title, queueItem.Description, queueItem.Image);
            }
        }

        private async void CloseActiveViewTimerCallback(NotificationPopup view)
        {
            await view.HideNotification();
            activeNotificationViews[view.Index] = null;
        }

        #endregion

        #region Helper classes

        private class QueueItem
        {
            public NotificationPopup View { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }

            public Image Image { get; set; }
        }

        #endregion
    }
}
