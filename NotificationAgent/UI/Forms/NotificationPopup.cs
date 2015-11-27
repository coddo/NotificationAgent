using NotificationAgent.UI.Abstract;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace NotificationAgent.UI.Forms
{
    public partial class NotificationPopup : GenericNotificationView, INotificationView
    {
        #region Constructors

        public NotificationPopup(Stream popupSound, Color popupColor, Color textColor) : base(popupSound, popupColor, textColor)
        {
            InitializeComponent();
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

        public void DisplayNotification(string message, string details, Image image)
        {
            throw new NotImplementedException();
        }

        public void CloseNotification()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Event handlers



        #endregion
    }
}
