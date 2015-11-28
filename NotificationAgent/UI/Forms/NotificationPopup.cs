using NotificationAgent.UI.Abstract;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotificationAgent.UI.Forms
{
    public partial class NotificationPopup : GenericNotificationView, INotificationView
    {
        #region Fields

        private Guid id;

        #endregion

        #region Constructors

        public NotificationPopup(Stream popupSound, Color popupColor, Color textColor) : base(popupSound, popupColor, textColor)
        {
            InitializeComponent();

            id = Guid.NewGuid();
        }

        #endregion

        #region Overrides

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);

            this.imageView.BackColor = this.BackColor;
            this.titleView.BackColor = this.BackColor;
            this.descriptionView.BackColor = this.BackColor;
        }

        #endregion

        #region Main functionality & interface implementation

        public int Index { get; set; }

        public bool IsEqual(INotificationView view)
        {
            if (view is NotificationPopup)
            {
                return this.id == (view as NotificationPopup).id;
            }

            return false;
        }

        public void ShowNotification(string title, string description, Image image)
        {
            if (SoundPlayer.Stream != null)
            {
                SoundPlayer.Play();
            }

            this.titleView.Text = title;
            this.descriptionView.Text = description;
            this.imageView.Image = image;

            this.Show();
        }

        public void HideNotification()
        {
            if (this.InvokeRequired)
            {
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        this.Close();
                    }));
                }
            }
            else
            {
                this.Close();
            }
        }

        #endregion

        #region Event handlers

        private void NotificationPopup_Load(object sender, EventArgs e)
        {
            this.BackColor = base.NotificationColor;

            this.titleView.ForeColor = base.TextColor;
            this.descriptionView.ForeColor = base.TextColor;
        }

        #endregion
    }
}
