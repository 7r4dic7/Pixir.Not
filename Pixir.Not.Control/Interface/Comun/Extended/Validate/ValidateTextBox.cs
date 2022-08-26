using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixir.Not.Control.Interface.Comun.Extended.Validate
{
    /// <summary>
    /// Clase que contiene metodos que validan datos requeridos
    /// </summary>
    public static class ValidateTextBox
    {
        /// <summary>
        /// metodo que valida si el campo de texto es vacio, true si es falso, false es verdadero
        /// </summary>
        /// <param name="_textField">String</param>
        /// <returns>bool</returns>
        public static bool getTextFieldNull(this String _textField)
        {
            try
            {
                return _textField.Trim().Equals(String.Empty);
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
            return true;
        }

        /// <summary>
        /// metodo que valida si el campo de texto contiene un valor mayoa a cero
        /// </summary>
        /// <param name="_textField">String</param>
        /// <returns>bool</returns>
        public static bool getValueMayorCero(this String _textField)
        {
            try
            {
                if (_textField.Trim().Equals(String.Empty))
                {
                    return false;
                }
                else
                {
                    int value = int.Parse(_textField);

                    if (value > (int)Data.Extended.Enum.EnumNumericValue.Cero)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
            return true;
        }

    }
}
