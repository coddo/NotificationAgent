using NotificationAgent.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotificationAgent.UI.Forms
{
    public partial class NotificationPopup : Form, INotificationView
    {
        #region Static configuration fields

        private static Queue<NotificationPopup> _queuedPopupViews = new Queue<NotificationPopup>();
        private static NotificationPopup[] _activePopupViews = default(NotificationPopup[]);
        private static int _popupsPositionX = default(int);
        private static bool _isConfigured = false;

        #endregion

        public NotificationPopup()
        {
            InitializeComponent();

            this.soundPlayer = new SoundPlayer();
        }


        #region Private fields

        private readonly SoundPlayer soundPlayer;

        #endregion

        #region Popup decoration properties

        public Stream PopupSound
        {
            set
            {
                this.soundPlayer.Stream = value;
            }
        }

        public Color PopupColor
        {
            set
            {
                this.BackColor = value;
            }
        }

        public Color TextColor
        {
            set
            {
                this.titleView.ForeColor = value;
                this.descriptionView.ForeColor = value;
            }
        }

        #endregion

        #region UI Display properties

        public Image Image
        {
            set
            {
                this.imageView.Image = value;
            }
        }

        public string Title
        {
            set
            {
                this.titleView.Text = value;
            }
        }

        public string Description
        {
            set
            {
                this.descriptionView.Text = value;
            }
        }

        #endregion

        #region Overrides

        protected async override void OnBackColorChanged(EventArgs e)
        {
            var colorAdapterTask = Task.Run(() =>
            {
                this.imageView.BackColor = this.BackColor;
                this.titleView.BackColor = this.BackColor;
                this.descriptionView.BackColor = this.BackColor;
            });

            base.OnBackColorChanged(e);

            await colorAdapterTask;
        }

        #endregion

        #region Static configuration

        private static async Task SetupPopupsEngine(Rectangle screenWorkingArea, Rectangle popUpArea)
        {
            await Task.Run(() =>
            {
                var rightMarginPointX = screenWorkingArea.X + screenWorkingArea.Width;
                var spacingFromRightMargin = popUpArea.Width / 4;
                NotificationPopup._popupsPositionX = rightMarginPointX - popUpArea.Width - spacingFromRightMargin;

                var maximumActivePopups = screenWorkingArea.Height / popUpArea.Height;
                NotificationPopup._activePopupViews = new NotificationPopup[maximumActivePopups];

                NotificationPopup._isConfigured = true;
            });
        }

        #endregion

        #region Main functionality

        public void DisplayNotification(string message, string details, Image image)
        {
            throw new NotImplementedException();
        }

        public void CloseNotification()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
