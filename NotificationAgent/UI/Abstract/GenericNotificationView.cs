using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace NotificationAgent.UI.Abstract
{
    public abstract class GenericNotificationView : Form
    {
        #region Constructors

        public GenericNotificationView(Point location, Stream notificationSound, Color notificationColor, Color textColor)
        {
            this.SoundPlayer = new SoundPlayer(notificationSound);

            this.NotificationColor = notificationColor;
            this.TextColor = textColor;
            this.Location = location;
        }

        #endregion

        #region Protected properties

        protected SoundPlayer SoundPlayer { get; private set; }

        protected Color NotificationColor { get; private set; }

        protected Color TextColor { get; private set; }

        #endregion
    }
}
