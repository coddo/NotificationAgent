using NotificationAgent.UI.Abstract;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using NotificationAgent.UI.Forms;
using System;

namespace NotificationAgent.UI.DisplayManagers
{
    public class NotificationPopupDisplayConfigurator : IDisplayConfigurator<NotificationPopup>
    {
        #region Constants

        private const int TIMER_ITERATION_INTERVAL = 10;

        private const int TIMER_CLOSE_VIEW_INTERVAL = 5000;

        private const int VIEW_SPACING_COEFICIENT = 6;

        #endregion

        #region Fields

        private Queue<QueueItem> queuedNotificationViews = new Queue<QueueItem>();
        private NotificationPopup[] activeNotificationViews = default(NotificationPopup[]);

        private Timer iterationTimer;

        #endregion

        public NotificationPopupDisplayConfigurator()
        {
            iterationTimer = new Timer();

            iterationTimer.Interval = TIMER_ITERATION_INTERVAL;
            iterationTimer.Tick += IterateAndShowViewsTimerCallback;
            iterationTimer.Enabled = true;
            iterationTimer.Start();
        }

        #region Positioning properties

        private int NotificationPositionX { get; set; }

        private int NotificationViewHeight { get; set; }

        public bool IsConfigured { get; set; } = false;

        #endregion

        #region Configuration

        public void SetupNotificationViewPositioning(Rectangle notificationViewArea)
        {
            var screenWorkingArea = Screen.PrimaryScreen.WorkingArea;
            var rightMarginPointX = screenWorkingArea.X + screenWorkingArea.Width;
            var spacingFromRightMargin = notificationViewArea.Width / VIEW_SPACING_COEFICIENT;
            var spacingBetweenViews = notificationViewArea.Height / VIEW_SPACING_COEFICIENT;

            NotificationViewHeight = notificationViewArea.Height + spacingBetweenViews;
            NotificationPositionX = rightMarginPointX - notificationViewArea.Width - spacingFromRightMargin;

            var maximumActivePopups = screenWorkingArea.Height / notificationViewArea.Height - 1;
            activeNotificationViews = new NotificationPopup[maximumActivePopups];

            IsConfigured = true;
        }

        #endregion

        #region View positioning

        public void DisplayView(NotificationPopup view, string title, string description, Image image)
        {
            queuedNotificationViews.Enqueue(new QueueItem
            {
                View = view,
                Title = title,
                Description = description,
                Image = image
            });

            var closeViewTimer = new Timer();

            closeViewTimer.Interval = TIMER_CLOSE_VIEW_INTERVAL;
            closeViewTimer.Tick += delegate
            {
                CloseActiveViewTimerCallback(view);
            };

            closeViewTimer.Enabled = true;
            closeViewTimer.Start();
        }

        #endregion

        #region Timer callbacks

        private void IterateAndShowViewsTimerCallback(object sender, EventArgs e)
        {
            if (queuedNotificationViews.Count == 0)
                return;

            if (!activeNotificationViews.Any(v => v == null))
                return;

            var queueItem = queuedNotificationViews.Dequeue();
            var view = queueItem.View;
            int index = 0;

            for (index = 0; index < activeNotificationViews.Length && activeNotificationViews[index] != null; index++) ;

            view.Index = index;
            activeNotificationViews[index] = view;

            view.ShowNotification(queueItem.Title, queueItem.Description, queueItem.Image);
            view.Location = new Point(this.NotificationPositionX, Screen.PrimaryScreen.WorkingArea.Height - this.NotificationViewHeight * (view.Index + 1));
        }

        private void CloseActiveViewTimerCallback(NotificationPopup view)
        {
            activeNotificationViews[view.Index] = null;
            view.HideNotification();
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
