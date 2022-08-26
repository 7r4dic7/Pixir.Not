using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixir.Not.Control.Interface.Comun.Extended.Validate
{
    public static class ValidateDate
    {
        /// <summary>
        /// Metodo que valida que la fecha no sea mayor al dia de hoy
        /// </summary>
        /// <param name="_dateTimePicker">DateTimePicker</param>
        /// <returns>bool</returns>
        public static bool getDateTimePostToday(this DateTimePicker _dateTimePicker)
        {
            try
            {
                if (_dateTimePicker.Value.Date > DateTime.Now.Date)
                {
                    return false;

                }

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
            return true;
        }

        /// <summary>
        /// metodo  que valida que la edad de la persona sea mayor a los 18 años
        /// </summary>
        /// <returns>bool</returns>
        public static bool getBirthDateMinimun(this DateTimePicker _dateTimePicker, Form _form)
        {
            try
            {
                //valida si la fecha es igual o mayor al dia de hoy
                if (_dateTimePicker.Value.Date >= DateTime.Now.Date)
                {
                    //MessageBox.Show(_form, Resources.MES_FECHA_IGUAL_POSTERIOR_HOY, Resources.TIT_VERIFICAR,
                    //    MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;

                }
                else
                {

                    //si edad es mayor a 18
                    if (DateTime.Now.Year - _dateTimePicker.Value.Year > 18)
                    {
                        return true;
                    }

                    //si edad es menor a 18
                    if (DateTime.Now.Year - _dateTimePicker.Value.Year < 18)
                    {
                        //DialogResult result = MessageBox.Show(Resources.MES_EDAD_MENOR_18_ANOS, Resources.TIT_VERIFICAR,
                        //       MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        //if (result == DialogResult.Yes)
                        //{
                        //    return true;
                        //}

                        return false;
                    }

                    //si la edad es igual a 18 años
                    if (DateTime.Now.Year - _dateTimePicker.Value.Year == 18)
                    {
                        //si el mes es menor al mes de nacimiento
                        if (_dateTimePicker.Value.Month < DateTime.Now.Month)
                        {
                            return true;

                        }

                        //si el mes de nacimiento es mayor a el mes actual
                        if (_dateTimePicker.Value.Month > DateTime.Now.Month)
                        {
                            //DialogResult result = MessageBox.Show(Resources.MES_EDAD_MENOR_18_ANOS, Resources.TIT_VERIFICAR,
                                //MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            //if (result == DialogResult.Yes)
                            //{
                            //    return true;
                            //}

                            return false;
                        }
                        // si el mes de nacimiento y el mes actual son iguale
                        if (_dateTimePicker.Value.Month == DateTime.Now.Month)
                        {
                            //si el dia es menor o igual a l dia de hoy
                            if (_dateTimePicker.Value.Day <= DateTime.Now.Day)
                            {
                                return true;
                            }
                            //si dia de nacimiento es mayor a dia de hoy
                            if (_dateTimePicker.Value.Day > DateTime.Now.Day)
                            {
                               // DialogResult result = MessageBox.Show(Resources.MES_EDAD_MENOR_18_ANOS, Resources.TIT_VERIFICAR,
                                //MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                //if (result == DialogResult.Yes)
                                //{
                                //    return true;
                                //}

                                return false;

                            }
                        }
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
