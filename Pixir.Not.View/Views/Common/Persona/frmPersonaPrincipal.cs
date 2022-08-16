#region Using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Linq;
using Pixir.Not.Control.Interface.Comun;
using Pixir.Not.View.Extended.Disconnect.Catalog;
using Pixir.Not.Data.Entity;

#endregion
namespace Pixir.Not.View.Views.Common.Persona
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frmPersonaPrincipal : Form, IControlRegister<Data.Entity.ComPersona>
    {
        #region Variables
        /// <summary>
        /// Variable que representa el punto de entrada de linq
        /// </summary>
        private DataContext dataContext;
        /// <summary>
        /// Variable del tipo CatalogExchange
        /// </summary>
        private CatalogExchange estadoCivilCriteria;
        /// <summary>
        /// Variable del tipo CatalogExchange
        /// </summary>
        private CatalogExchange sexoCriteria;
        /// <summary>
        /// Variable del tipo String almacena la cadena de busqueda.
        /// </summary>
        private String textoCriteria;
        /// <summary>
        /// Variable del tipo ComPersona. Hace referencia a la entidad base o principal del modulo
        /// </summary>
        private Data.Entity.ComPersona baseEntity;
        /// <summary>
        /// Variable del tipo System.Windows.Forms.Timer
        /// </summary>
        System.Windows.Forms.Timer clock = new System.Windows.Forms.Timer();
        /// <summary>
        /// Variable que contiene todas las instancias de la interface que se implementan
        /// </summary>
        private List<IRegisterRules> reglasSeleccion = new List<IRegisterRules> ();
        /// <summary>
        /// Variable que indica si ya fue asignados los datos a otro modulo
        /// </summary>
        private bool asingData = false;
        /// <summary>
        /// Variable que inicializa el estado del registro como activo
        /// </summary>
        private Data.Entity.ComCatEstadoRegistro estadoRegistroCriteria = new Data.Entity.ComCatEstadoRegistro();
        /// <summary>
        /// Variable que indica si se cerro la ventana al pulssar el boton X
        /// </summary>
        private bool closeWindow = false;
        /// <summary>
        /// Variable que indica que se ha seleccionado el filtro sexo, la seleccion es diferente de todos
        /// </summary>
        private bool inSexo = false;
        /// <summary>
        /// Variable que indica que se ha seleccionado el filtro estado civil, la seleccion es diferente de todos
        /// </summary>
        private bool inEstadoCivil = false;
        /// <summary>
        /// Variable que indica si la consulta en el setResultadoEmpleado
        /// se ejecutara en la forma abierta o en lla cerrada, donde abierta es igual a entrar al databinding normal o
        /// cerrrada donde entra a databinding source especifico, dependiendo de las combinaciones. cuando es true no entra
        /// si trae datos tipo texto en la consulta
        /// </summary>
        private bool inSetResultado = false;
        /// <summary>
        /// Variable que lleva el conteo de cuantas ocacaiones se accede
        /// a setResultadoPersona al cargar la pantalla
        /// </summary>
        private int countSetResultadoGlobal = (int)Data.Extended.Enum.EnumNumericValue.Cero;
        /// <summary>
        /// variable que verifica si se ha dado click al boton seleccionar
        /// </summary>
        private bool buttonSelect = false;
        /// <summary>
        /// Variable que indica si se ha pulsado el bototn salir
        /// </summary>
        private bool buttonExit = false;
        /// <summary>
        /// DataContext que permite realizar la eliminacion
        /// </summary>
        private DataContext dataContextDelete;
        /// <summary>
        /// Variable que contiene el mensaje para la excepcion del filtro
        /// </summary>
        private String messageFilterException = String.Empty;
        /// <summary>
        /// Objeto que establece la instancia de la clase que contiene la sentencia precompilada para principal
        /// </summary>
        View.Common.Compiled<Data.Entity.ComPersona> queryCompiled = new View.Common.Compiled<Data.Entity.ComPersona> ();
        #endregion

        #region Propiedades
        public DataContext DataContext
        {
            get { return dataContext; }
            set { dataContext = value; }
        }
        /// <summary>
        /// Propiedad que obtiene la referencia de todas las implementaciones de esta interface
        /// </summary>
        public List<IRegisterRules> ReglasSeleccion
        {
            get { return reglasSeleccion; }
            set { reglasSeleccion = value; }
        }
        #endregion

        #region Constructor
        public frmPersonaPrincipal()
        {
            InitializeComponent();
        }
        #endregion

        #region Metodo loadInitialInformation
        private void loadInitialInformation()
        {
            try
            {
                this.DataContext = new DcGeneralDataContext();
                this.setFiltroComCatEstadoCivil();
                this.setFiltroComCatSexo();
                this.setresultadoComPersona();
                this.hidePanelMessage();
                this.setStatus();
                this.setTabIndex();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Metodo setStatus
        private void setStatus()
        {
            this.lblDetalle.Location = new Point((int)Data.Extended.Enum.EnumLocation.PositionNumberFile_X,
                (int)Data.Extended.Enum.EnumLocation.PositionNumberFile_Y);
            this.btnSeleccionar.Visible = false

        }
        #endregion
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void frmPersonaPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void pnlBusqueda_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblDetalle_Click(object sender, EventArgs e)
        {

        }
    }
}
