using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using Hilo;
using System.Net;

namespace Navegador
{
    public partial class frmWebBrowser : Form
    {
        private const string ESCRIBA_AQUI = "Escriba aquí...";
        Archivos.Texto archivos;
        private Descargador descargador;

        /// <summary>
        /// Constructor defecto
        /// </summary>
        public frmWebBrowser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Inicializa campos del Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmWebBrowser_Load(object sender, EventArgs e)
        {
            this.txtUrl.SelectionStart = 0;  //This keeps the text
            this.txtUrl.SelectionLength = 0; //from being highlighted
            this.txtUrl.ForeColor = Color.Gray;
            this.txtUrl.Text = frmWebBrowser.ESCRIBA_AQUI;
            this.tspbProgreso.Minimum = 0;
            this.tspbProgreso.Maximum = 100;
            archivos = new Archivos.Texto(frmHistorial.ARCHIVO_HISTORIAL);
            
        }

        #region "Escriba aquí..."
        private void txtUrl_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.IBeam; //Without this the mouse pointer shows busy
        }

        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.txtUrl.Text.Equals(frmWebBrowser.ESCRIBA_AQUI) == true)
            {
                this.txtUrl.Text = "";
                this.txtUrl.ForeColor = Color.Black;
            }
        }

        private void txtUrl_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.txtUrl.Text.Equals(null) == true || this.txtUrl.Text.Equals("") == true)
            {
                this.txtUrl.Text = frmWebBrowser.ESCRIBA_AQUI;
                this.txtUrl.ForeColor = Color.Gray;
            }
        }

        private void txtUrl_MouseDown(object sender, MouseEventArgs e)
        {
            this.txtUrl.SelectAll();
        }
        #endregion

        delegate void ProgresoDescargaCallback(int progreso);
        /// <summary>
        /// Suscripto a evento de Descargador para mostrar progreso
        /// </summary>
        /// <param name="progreso"></param>
        private void ProgresoDescarga(int progreso)
        {
            if (statusStrip.InvokeRequired)
            {
                ProgresoDescargaCallback d = new ProgresoDescargaCallback(ProgresoDescarga);
                this.Invoke(d, new object[] { progreso });
            }
            else
            {
                tspbProgreso.Value = progreso;
            }
        }
        delegate void FinDescargaCallback(string html);
        /// <summary>
        /// Suscripto a evento de Descargador para mostrar fin progreso
        /// </summary>
        /// <param name="html"></param>
        private void FinDescarga(string html)
        {
            if (rtxtHtmlCode.InvokeRequired)
            {
                FinDescargaCallback d = new FinDescargaCallback(FinDescarga);
                this.Invoke(d, new object[] { html });
            }
            else
            {
                rtxtHtmlCode.Text = html;
            }
        }
        /// <summary>
        /// Muestra el historial de sitios visitados en un cuadro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mostrarTodoElHistorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHistorial historial = new frmHistorial();
            historial.Show();
        }

        /// <summary>
        /// Carga todo el código fuente de la página en un hilo aparte mientras con un tercer hilo ejecuto un método para guardar url en el historial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIr_Click(object sender, EventArgs e)
        {
            string requestedURL = this.cleanUrl(this.txtUrl.Text);
            this.txtUrl.Text = requestedURL;
            Thread HiloGuardar = new Thread(this.GuardarHistorial);
            HiloGuardar.Start();
            ParameterizedThreadStart hiloparametrizado = new ParameterizedThreadStart(this.downloadData);
            Thread HiloDescargar = new Thread(hiloparametrizado);
            HiloDescargar.Start(requestedURL);
        }
        
        /// <summary>
        /// Guarda el url actual en un subproceso mientras el hilo principal carga el código fuente de la pagina
        /// </summary>
        private void GuardarHistorial()
        {
            if (!this.archivos.guardar(this.txtUrl.Text))
                MessageBox.Show("No se pudo guardar el historial");
        }

        /// <summary>
        /// Si el string comienza con https lo retorna con http
        /// </summary>
        /// <param name="requestedURL"></param>
        /// <returns></returns>
        private string cleanUrl(string requestedURL)
        {
            if (!requestedURL.StartsWith("http://") && !requestedURL.StartsWith("https://"))
            {
                return "http://" + requestedURL;
            }
            else
            {
                return requestedURL;
            }
        }

        /// <summary>
        /// Realiza descarga de la pagina utilizando el tipo Descargador del proyecto Hilo
        /// </summary>
        /// <param name="url"></param>
        private void downloadData(object url)
        {
            url = (string)url;
            this.tspbProgreso.Value = 0;
            Uri uri = new Uri((string)url);
            this.descargador = new Hilo.Descargador(uri);
            this.descargador.EnProgreso += this.ProgresoDescarga;
            this.descargador.Finalizado += this.FinDescarga;
            descargador.IniciarDescarga();
        }
    }
}
