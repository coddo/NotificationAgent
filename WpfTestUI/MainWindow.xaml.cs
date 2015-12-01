using NotificationAgent;
using NotificationAgent.UI.DisplayManagers;
using NotificationAgent.UI.Forms;
using System.Windows;

namespace WpfTestUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var displayManager = new VerticalViewDisplayManager<StandardPopup>();
            notificationCenter = new NotificationsCenter<StandardPopup>(displayManager);
        }

        private int notificationIndex = 0;
        private NotificationsCenter<StandardPopup> notificationCenter;

        private void displayNotificationButton_Click(object sender, RoutedEventArgs e)
        {
            notificationCenter.DisplayNotification($"Title {notificationIndex}", $"Description {notificationIndex}", null);
            notificationIndex++;
        }
    }
}
