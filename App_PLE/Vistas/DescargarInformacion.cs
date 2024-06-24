using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML;
using ClosedXML.Excel;

namespace App_PLE.Vistas
{
    public partial class DescargarInformacion : Form
    {
        public DescargarInformacion()
        {
            InitializeComponent();

            LoadTables();
        }

        private void DescargarInformacion_Load(object sender, EventArgs e)
        {

        }

        private void LoadTables()
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    // abrir la conexion
                    conexion.Open();

                    DataTable tables = conexion.GetSchema("Tables");

                    foreach (DataRow row in tables.Rows)
                    {
                        clbTablasDB.Items.Add(row["TABLE_NAME"]);
                    }

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al llenar el ComboBox: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }

            }

           
        }

        private void btnDescargarForm_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Guardar archivo Excel",
                FileName = "Export.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string tableName in clbTablasDB.CheckedItems)
                {
                    ExportTableData(tableName, saveFileDialog.FileName);
                }

                MessageBox.Show("Exportación completa.");
            }
        }

        private void ExportTableData(string tableName, string filePath)
        {
            string cadena = "Data Source = DB_PLE.db;Version=3;";

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                string query = $"SELECT * FROM {tableName}";
                SQLiteCommand command = new SQLiteCommand(query, conexion);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Aquí puedes elegir el formato de exportación, por ejemplo CSV.
                //ExportToCsv(dataTable, $"{tableName}.csv");
                ExportToExcel(dataTable, filePath);
            }
                
        }

        private void ExportToCsv(DataTable dataTable, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Escribir encabezados
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    writer.Write(dataTable.Columns[i]);
                    if (i < dataTable.Columns.Count - 1)
                        writer.Write(",");
                }
                writer.WriteLine();

                // Escribir filas
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        writer.Write(row[i].ToString());
                        if (i < dataTable.Columns.Count - 1)
                            writer.Write(",");
                    }
                    writer.WriteLine();
                }
            }
        }
        private void ExportToExcel(DataTable dataTable, string filePath)
        {
            using (XLWorkbook workbook = new XLWorkbook())
            {
                var sheetName = !string.IsNullOrWhiteSpace(dataTable.TableName) ? dataTable.TableName : "Export";
                var worksheet = workbook.Worksheets.Add(dataTable, sheetName);
                workbook.SaveAs(filePath);
            }
        }
    }
}
