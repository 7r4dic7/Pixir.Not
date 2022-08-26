using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixir.Not.Control.Interface.Comun.Extended.Validate
{
    public static class ValidateComboBox
    {
        /// <summary>
        /// Metodo que valida que los comboboxes hayan sido seleccionados y los datos sean de un catalogo
        /// </summary>
        /// <param name="_comboBox">ComboBox</param>
        /// <returns>bool</returns>
        public static bool getComboBoxNull(this ComboBox _comboBox)
        {
            try
            {
                if (_comboBox.SelectedIndex >= (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    return true;
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

            return false;
        }

        /// <summary>
        /// Metodo que valida que los comboboxes hayan sido seleccionados y los datos sean de un catalogo
        /// </summary>
        /// <param name="_comboBox">ComboBox</param>
        /// <returns>bool</returns>
        public static bool getComboBoxNullNoCatalog(this ComboBox _comboBox)
        {
            try
            {
                if (_comboBox.SelectedIndex > (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    return true;
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
            return false;
        }

    }
}
