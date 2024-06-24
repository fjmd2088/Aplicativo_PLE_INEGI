using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace App_PLE.Vistas
{
    public partial class FormDashboard : Form
    {
        private Chart chart;
        public FormDashboard()
        {
            InitializeComponent();

            chart = new Chart();
            chart.Dock = DockStyle.Fill;
            this.Controls.Add(chart);

            // Crear áreas de gráfico separadas
            ChartArea barChartArea = new ChartArea("BarChartArea");
            ChartArea lineChartArea = new ChartArea("LineChartArea");
            ChartArea pieChartArea = new ChartArea("PieChartArea");

            chart.ChartAreas.Add(barChartArea);
            chart.ChartAreas.Add(lineChartArea);
            chart.ChartAreas.Add(pieChartArea);

            // Agregar serie para gráfico de barras
            Series barSeries = new Series("Barras");
            barSeries.ChartType = SeriesChartType.Bar;
            barSeries.Points.AddXY("A", 10);
            barSeries.Points.AddXY("B", 20);
            barSeries.Points.AddXY("C", 30);
            barSeries.ChartArea = "BarChartArea"; // Asignar la serie al área de gráfico correspondiente
            chart.Series.Add(barSeries);

            // Agregar serie para gráfico de líneas
            Series lineSeries = new Series("Líneas");
            lineSeries.ChartType = SeriesChartType.Line;
            lineSeries.Points.AddXY("Enero", 5);
            lineSeries.Points.AddXY("Febrero", 15);
            lineSeries.Points.AddXY("Marzo", 25);
            lineSeries.ChartArea = "LineChartArea"; // Asignar la serie al área de gráfico correspondiente
            chart.Series.Add(lineSeries);

            // Agregar serie para gráfico de pastel
            Series pieSeries = new Series("Pastel");
            pieSeries.ChartType = SeriesChartType.Pie;
            pieSeries.Points.AddXY("Rojo", 40);
            pieSeries.Points.AddXY("Verde", 30);
            pieSeries.Points.AddXY("Azul", 20);
            pieSeries.Points.AddXY("Amarillo", 10);
            pieSeries.ChartArea = "PieChartArea"; // Asignar la serie al área de gráfico correspondiente
            chart.Series.Add(pieSeries);

            // Configurar el diseño del gráfico de barras
            barChartArea.AxisX.Interval = 1;
            barChartArea.AxisX.Title = "Categorías";
            barChartArea.AxisY.Title = "Valores";

            // Configurar el diseño del gráfico de líneas
            lineChartArea.AxisX.Interval = 1;
            lineChartArea.AxisX.Title = "Meses";
            lineChartArea.AxisY.Title = "Valores";

            // Configurar el diseño del gráfico de pastel
            pieChartArea.AxisX.Interval = 1;
            pieChartArea.AxisX.Title = "Colores";
            pieChartArea.AxisY.Title = "Porcentaje";
        }

  

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            

        }
        
    }
}
