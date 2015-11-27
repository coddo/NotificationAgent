using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotificationAgent.UI
{
    public partial class NotificationPopup : Form
    {
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
    }
}
