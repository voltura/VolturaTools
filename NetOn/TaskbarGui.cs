#region Using statements

using System;
using System.Windows.Forms;

#endregion Using statements

namespace NetOn
{
    internal class TaskbarGui : IDisposable, IGui
    {
        #region Private variables

        private NotifyIcon _notifyIcon;
        private readonly NetOnContextMenu _contextMenu;
        private readonly Speak _speak;

        #endregion Private variables

        #region Constructor

        internal TaskbarGui(int status = 1)
        {
            _speak = new Speak();
            _contextMenu = new NetOnContextMenu(ref _speak);
            _notifyIcon = GetNotifyIcon(_contextMenu.ContextMenu);
            _notifyIcon.Click += NotifyIcon_Click;
            UpdateIcon(status, ref _notifyIcon);
            StateStatus();
        }

        #endregion Constructor

        #region Public UpdateIcon method

        /// <summary>
        /// Updates icon on GUI with given status number
        /// </summary>
        /// <param name="NetOn"></param>
        public void UpdateIcon(int NetOn) => UpdateIcon(NetOn, ref _notifyIcon);

        #endregion Public UpdateIcon method

        #region Private static UpdateIcon method

        private static void UpdateIcon(int NetOn, ref NotifyIcon notifyIcon)
        {
            notifyIcon.Text = (NetOn == 1) ? Resources.Network + Resources.Enabled : Resources.Network + Resources.Disabled;
            var prevIcon = notifyIcon.Icon;
            notifyIcon.Icon = NetOnIcon.GetIcon(NetOn);
            NetOnIcon.CleanupIcon(ref prevIcon);
        }

        #endregion Private static UpdateIcon method

        #region Events

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            var eobj = e as MouseEventArgs;
            if (eobj.Button == MouseButtons.Left)
            {
                ChangeStatus();
                StateStatus();
            }
        }

        #endregion Events

        #region Private methods

        private void StateStatus()
        {
            if (Status.Current() == 1)
            {
                _speak?.Sentence(Resources.ClearThroat + Resources.Network + Resources.Enabled);
            }
            else
            {
                _speak?.Sentence(Resources.ClearThroat + Resources.Network + Resources.Disabled);
            }
        }

        private void ChangeStatus()
        {
            if (Status.Current() == 1)
            {
                Network.Disable();
            }
            else
            {
                Network.Enable();
            }
        }

        #endregion Private methods

        #region Private helper property to create NotifyIcon

        private static NotifyIcon GetNotifyIcon(ContextMenu contextMenu) =>
            new NotifyIcon { Visible = true, ContextMenu = contextMenu };

        #endregion Private helper property to create NotifyIcon

        #region IDisposable methods

        /// <summary>
        /// Disposes the GUI resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            CleanupNotifyIcon();
            _contextMenu.Dispose();
        }

        private void CleanupNotifyIcon()
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = false;
                if (_notifyIcon.Icon != null)
                {
                    NativeMethods.DestroyIcon(_notifyIcon.Icon.Handle);
                    _notifyIcon.Icon?.Dispose();
                }
                _notifyIcon.ContextMenu?.MenuItems.Clear();
                _notifyIcon.ContextMenu?.Dispose();
                _notifyIcon.Dispose();
                _notifyIcon = null;
            }
        }

        #endregion IDisposable methods
    }
}