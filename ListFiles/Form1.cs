using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListFiles
{
    public partial class Form1 : Form
    {
        string folder;

        internal string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dlg = new CommonOpenFileDialog();
            dlg.Title = "Selección de Carpeta";
            dlg.IsFolderPicker = true;
            dlg.InitialDirectory = currentDirectory;

            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.DefaultDirectory = currentDirectory;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                this.Cursor = Cursors.WaitCursor;
                folder = dlg.FileName;
                listBox1.Items.Clear();
                listBox1.Enabled = true;
                // Hacer la búsqueda
                try
                {
                    foreach (string file in Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories))
                    {
                        // Quitar todo excepto del nombre de archivo
                        string input = file;
                        string output = input.Split('\\').Last();
                        // Quitar extensión del nombre de archivo
                        if (!checkBox1.Checked)
                        {
                            string extension = System.IO.Path.GetExtension(output);
                            output = output.Replace(extension, string.Empty);
                        }
                        listBox1.Items.Add(output);
                    }
                }
                catch (Exception ex) {
                    // Si hay algun error en algun nombre de archivo, lo ignoramos y ya.
                }
                this.Cursor = Cursors.Default;
                linkLabel1.Visible = true;
                linkLabel1.Text = "Copiar";
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string acopiar = string.Empty;
            foreach (object liItem in listBox1.Items)
            acopiar += Environment.NewLine + liItem.ToString();
            Clipboard.SetText(acopiar);
            linkLabel1.Text = "Copiado!";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                listBox1.Items.Clear();
                listBox1.Enabled = true;
                // Hacer la búsqueda (sí, repito código, pero la aplicación es demasiado pequeña para que importe)
                try
                {
                    foreach (string file in Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories))
                    {
                        // Quitar todo excepto del nombre de archivo
                        string input = file;
                        string output = input.Split('\\').Last();
                        // Quitar extensión del nombre de archivo
                        if (!checkBox1.Checked)
                        {
                            string extension = System.IO.Path.GetExtension(output);
                            output = output.Replace(extension, string.Empty);
                        }
                        listBox1.Items.Add(output);
                    }
                }
                catch (Exception ex)
                {
                    // Si hay algun error en algun nombre de archivo, lo ignoramos y ya.
                }
                this.Cursor = Cursors.Default;
                linkLabel1.Visible = true;
                linkLabel1.Text = "Copiar";
            }
        }
    }
}
