using Pixir.Not.Data.Extended.Enum;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixir.Not.View.Common
{
    public interface ICXFormManager<T>
    {
        DataContext DataContext
        {
            get;
            set;
        }

        
        EnumAccionForm AccionForm
        {
            get;
            set;
        }

        EnumStateForm StateForm
        {
            get;
            set;
        }

        T BaseEntity
        {
            get;
            set;
        }

        String Titulo
        {
            set;
        }
        bool IgnoreClosing
        {
            get;
            set;
        }

         T show(Form _parent, DataContext _dc, EnumOperationType _tipoOperacion);

         T show(Form _parent, T _baseEntity, DataContext _dc, EnumOperationType _tipoOperacion);

         void loadInitialInformation(EnumOperationType _tipoOperacion);

         void setEntityForm(EnumOperationType _tipoOperacion);

         void getEntityForm();

         bool validateEntity();

         EnumAccionDialog AccionDialog
         {
             get;
             set;
         }

    //void setProfilePermissions(PermisosPantallaManager _permisosPantallaManager);

    }
}
