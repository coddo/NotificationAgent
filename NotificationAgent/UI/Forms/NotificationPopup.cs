using NotificationAgent.UI.Abstract;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

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

        public async Task Show(string title, string description, Image image)
        {
            await Task.Run(() =>
            {
                this.titleView.Text = title;
                this.descriptionView.Text = description;
                this.imageView.Image = image;

                this.Show();
            });
        }

        public async Task Hide()
        {
            await Task.Run(() =>
            {
                this.Close();
            });
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
