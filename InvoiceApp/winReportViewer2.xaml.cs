using CrystalDecisions.CrystalReports.Engine;
using SAPBusinessObjects.WPF.Viewer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InvoiceApp
{
    public partial class winReportViewer2 : Window
    {
        DataTable dt;
        Item Item;
        public winReportViewer2(DataTable dt, Item Item)
        {
            InitializeComponent();
            this.dt = dt;
            this.Item = Item;
            reportViewer.Owner = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Don't Forget to add app.config file startup attribute
                ReportDocument report = new ReportDocument();
                //Change crystal report property Change to output  directory as copy always
                report.Load("CrystalReportInvoice.rpt");
                report.SetDataSource(dt);
                reportViewer.ViewerCore.ReportSource = report;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
