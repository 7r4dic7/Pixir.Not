using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixir.Not.View2.Extended.View
{
    public static class PanelMessagePrincipal
    {
        /// <summary>
        /// metodo que establece la localizacion del panel y si esta visible.
        /// </summary>
        /// <param name="_panel">Panel</param>
        public static void setLocation(this Panel _panel)
        {
            _panel.Visible = false;
            _panel.Location = new Point((int)Not.Data.Extended.Enum.EnumLocation.PanelMessageX,
                    (int)Not.Data.Extended.Enum.EnumLocation.PanelMessageY);
        }

        /// <summary>
        /// Metodo que establece el tamaño del panel.
        /// </summary>
        /// <param name="_panel">Panel</param>
        public static void setSize(this Panel _panel)
        {
            _panel.Size = new Size((int)Not.Data.Extended.Enum.EnumPanelMessageSize.PanelMessageSizeX,
                (int)Not.Data.Extended.Enum.EnumPanelMessageSize.PanelMessageSizeY);
        }

        /// <summary>
        /// Metodo que hace visible el panel y muestra el mensaje.
        /// </summary>
        /// <param name="_panel">Panel</param>
        /// <param name="_label">Label</param>
        /// <param name="_message">String</param>
        public static void setVisiblePanelAndLabel(Panel _panel, Label _label, String _message)
        {
            _panel.Visible = true;
            _label.Text = _message;
        }

    }
}
