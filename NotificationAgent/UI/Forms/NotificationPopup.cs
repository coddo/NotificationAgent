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
        #region Private constants

        private const string NOT_CONFIGURED_EXCEPTION_MESSAGE = "The behavior of the popups hasn't been configured!";

        #endregion

        #region Static configuration fields

        private static Queue<NotificationPopup> _queuedPopupViews = new Queue<NotificationPopup>();
        private static NotificationPopup[] _activePopupViews = default(NotificationPopup[]);

        private static bool _isPositioningConfigured = false;
        private static bool _isDesignConfigured = false;

        #endregion

        #region Private fields

        private readonly SoundPlayer soundPlayer;

        #endregion

        #region Static configuration properties

        private static int _PopupPositionX { get; set; }

        private static Stream _PopupSound { get; set; }

        private static Color _PopupColor { get; set; }

        private static Color _TextColor { get; set; }

        private static bool _IsConfigured
        {
            get
            {
                return _isDesignConfigured && _isPositioningConfigured;
            }
        }

        #endregion

        #region Constructors

        public NotificationPopup()
        {
            InitializeComponent();

            this.soundPlayer = new SoundPlayer(_PopupSound);
            this.BackColor = _PopupColor;

            this.titleView.ForeColor = _TextColor;
            this.descriptionView.ForeColor = _TextColor;
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

        #region Static configuration methods

        public static async Task SetupPopupsPositioning(Rectangle screenWorkingArea, Rectangle popUpArea)
        {
            await Task.Run(() =>
            {
                var rightMarginPointX = screenWorkingArea.X + screenWorkingArea.Width;
                var spacingFromRightMargin = popUpArea.Width / 4;
                _PopupPositionX = rightMarginPointX - popUpArea.Width - spacingFromRightMargin;

                var maximumActivePopups = screenWorkingArea.Height / popUpArea.Height;
                _activePopupViews = new NotificationPopup[maximumActivePopups];

                _isPositioningConfigured = true;
            });
        }

        public static async Task SetupPopupsDesign(Color popupColor, Color textColor, Stream popupSound)
        {
            await Task.Run(() =>
            {
                _PopupColor = popupColor;
                _TextColor = textColor;
                _PopupSound = popupSound;

                _isDesignConfigured = true;
            });
        }

        #endregion

        #region Main functionality & interface implementation

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
