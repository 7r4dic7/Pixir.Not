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
using System.Linq.Expressions;

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
            this.btnSeleccionar.Visible = false;
            this.btnSalir.Visible = false;

        }
        #endregion

        #region Metodo setTabIndex
        /// <summary>
        /// Metodo que establece el tabindex de los controles, en forma manual
        /// </summary>
        private void setTabIndex()
        {
            //Controles superiores
            this.btnSalir.TabIndex = 1;
            this.btnAgregar.TabIndex = 2;
            this.pnlBusqueda.TabIndex = 3;
            this.txtCriteria.TabIndex = 4;
            this.btnActualizar.TabIndex = 5;
            this.btnSeleccionar.TabIndex = 6;
            this.btnEditar.TabIndex = 7;
            this.btnEliminar.TabIndex = 8;
            //Filtros
            this.dgvFiltroComCatSexo.TabIndex = 9;
            this.dgvFiltroComCatEstadoCivil.TabIndex = 10;
            //Resultado
            this.dgvResultadoPersona.TabIndex = 11;
            //Detalle
            this.pnlPrincipalDetalle.TabIndex = 12;
            this.btnImprimir.TabIndex = 13;
            //Detalle datagridview
            this.dgvDetalleComDatoContacto.TabIndex = 14;
            //Botones inferiores
            this.btnSms.TabIndex = 15;
            this.btnCorreoElectronico.TabIndex = 16;
        }
        #endregion

        #region Metodo setDataContextRefresh
        private void setDataContextRefresh()
        {
            try
            {
                if(this.dgvResultadoPersona.RowCount > (int)Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    this.dataContext.Refresh(RefreshMode.OverwriteCurrentValues,this.baseEntity);
                    for (int i = 0; i < this.baseEntity.ComDatoContacto.Count; i++)
                    {
                        ComDatoContacto comDatoContacto = new ComDatoContacto();
                        comDatoContacto = this.baseEntity.ComDatoContacto[i];
                        this.dataContext.Refresh(RefreshMode.OverwriteCurrentValues, comDatoContacto);
                    }
                    //for (int i = 0; i < this.baseEntity.CliPersonaCliente.Count; i++)
                    //{
                    //    for (int j = 0; j < this.baseEntity.CliPersonaCliente[i].CliCliente.ComDatoContacto.Count; j++)
                    //    {
                    //        ComDatoContacto comDatoContacto = new ComDatoContacto();
                    //        comDatoContacto = this.baseEntity.CliPersonaCliente[i].CliCliente.ComDatoContacto[j];
                    //        this.dataContext.Refresh(RefreshMode.OverwriteCurrentValues, this.baseEntity.CliPersonaCliente[i].CliCliente.ComDatoContacto[j]);
                    //    }
                    //}
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }
        #endregion

        #region metodos setFiltro
        private void setFiltroComCatEstadoCivil()
        {
            try
            {
                List<CatalogExchange> list = CtrlCatalogFilter.getCatalog<ComCatEstadoCivil>(this.DataContext).ToList();
                if(list.Count == (int)Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    //this.messageFilterException = "Resources.MES_COM_CAT_ESTADO_CIVIL_VACIO;
                    //filterExceptionPrincipal.throwExceptionInFilter();
                }
                list.Insert(0, new CatalogExchange()
                {
                    id = (int)Data.Extended.Enum.EnumNumericValue.MenosUno,
                    strValor = Data.Extended.Enum.EnumFilterHead.TODOS.ToString()
                });
                dgvFiltroComCatEstadoCivil.DataSource = list;
                dgvFiltroComCatEstadoCivil.Rows[0].Selected = true;
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }
        private void setFiltroComCatSexo()
        {
            try
            {
                List<CatalogExchange> list = CtrlCatalogFilter.getCatalog<ComCatSexo>(this.DataContext).ToList();
                if(list.Count == (int)Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    //this.messageFilterException = Resources.MES_COM_CAT_SEXO_VACIO;
                    //filterExceptionPrincipal.throwExceptionInFilter();
                }
                list.Insert(0, new CatalogExchange()
                {
                    id = (int)Data.Extended.Enum.EnumNumericValue.MenosUno,
                    strValor = Data.Extended.Enum.EnumFilterHead.TODOS.ToString()
                });
                dgvFiltroComCatSexo.DataSource = list;
                dgvFiltroComCatSexo.Rows[0].Selected = true;
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }
        #endregion

        #region Metodo setResultado
        private void setResultadoComPersona()
        {
            bool estadoCivilCriteria = false;
            bool estadoSexoCriteria = false;
            bool estadoTextoCriteria = false;

            int valueEstadoCivilCriteria = (int)Data.Extended.Enum.EnumNumericValue.Cero;
            string valueTextoCriteria = String.Empty;
            int valueSexoCriteria = (int)Data.Extended.Enum.EnumNumericValue.Cero;

            if(this.estadoCivilCriteria != null)
            {
                estadoCivilCriteria = true;
                valueEstadoCivilCriteria = this.estadoCivilCriteria.id;
            }
            if(this.sexoCriteria != null)
            {
                estadoSexoCriteria = true;
                valueSexoCriteria = this.sexoCriteria.id;
            }
            if (!this.txtCriteria.Text.Trim().Equals(""))
            {
                estadoTextoCriteria = true;
            }
            try
            {
                Expression<Func<ComPersona, bool>> predicate = c => (
                 ((estadoCivilCriteria) ? c.ComCatEstadoCivil.id == valueEstadoCivilCriteria : true) &&
                 ((estadoSexoCriteria) ? c.ComCatSexo.id == valueSexoCriteria : true) &&
                 ((estadoTextoCriteria) ? (((estadoTextoCriteria) ? c.strNombreCompleto.Contains(txtCriteria.Text.Trim()) : false))
                 : true)
                 );
                this.countSetResultadoGlobal++;
                List<ComPersona> lista = this.queryCompiled.getListEntity(this.DataContext)
                    .Where(predicate).OrderBy(p => p.strAPaterno).ToList();
                this.dgvResultadoPersona.DataSource = lista;
                this.lblNumeroFilas.Text = "{" + this.dgvResultadoPersona.RowCount.ToString() + "}";
                this.busquedaSinDatos();
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }
        private void limpiarSetDetalle()
        {
            try
            {
               
            }
            catch (Exception)
            {

                throw;
            }
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
