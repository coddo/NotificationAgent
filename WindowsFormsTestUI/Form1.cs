using System;
using System.Windows.Forms;
using NotificationAgent;
using NotificationAgent.UI.DisplayManagers;
using NotificationAgent.UI.Forms;

namespace WindowsFormsTestUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            notificationCenter = new NotificationsCenter<NotificationPopup, NotificationPopupDisplayConfigurator>();
        }

        private int notificationIndex = 0;
        private NotificationsCenter<NotificationPopup, NotificationPopupDisplayConfigurator> notificationCenter;

        private async void showNotificationButton_Click(object sender, EventArgs e)
        {
            await notificationCenter.DisplayNotification($"Title {notificationIndex}", $"Description {notificationIndex}", null);
            notificationIndex++;
        }
    }
}
