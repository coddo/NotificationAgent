﻿using System;
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

            var displayManager = new VerticalViewDisplayManager<StandardPopup>();
            notificationCenter = new NotificationsCenter<StandardPopup>(displayManager);
        }

        private int notificationIndex = 0;
        private NotificationsCenter<StandardPopup> notificationCenter;

        private void showNotificationButton_Click(object sender, EventArgs e)
        {
            notificationCenter.DisplayNotification($"Title {notificationIndex}", $"Description {notificationIndex}", null);
            notificationIndex++;
        }
    }
}
