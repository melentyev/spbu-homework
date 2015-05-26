using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Globalization;
using System.Threading;

namespace Localization
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ChangeLanguage("en-US");
            
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLanguage("en-US");
        }
        private void russianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLanguage("ru-RU");
        }
        private void ChangeLanguage(string lang)
        {
            russianToolStripMenuItem.Checked = (lang == "ru-RU");
            englishToolStripMenuItem.Checked = (lang != "ru-RU");
            CultureInfo cult = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = cult;
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            _ChangeLanguage(this, resources, cult);
            _ChangeMenuStripLanguage(menuStrip1.Items, resources, cult);

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new object[] {
            resources.GetString("comboBox1.Items"),
            resources.GetString("comboBox1.Items1")});

            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(new object[] {
            resources.GetString("comboBox2.Items"),
            resources.GetString("comboBox2.Items1"),
            resources.GetString("comboBox2.Items2")});

            comboBox3.Items.Clear();
            comboBox3.Items.AddRange(new object[] {
            resources.GetString("comboBox3.Items"),
            resources.GetString("comboBox3.Items1")});

            comboBox4.Items.Clear();
            comboBox4.Items.AddRange(new object[] {
            resources.GetString("comboBox4.Items"),
            resources.GetString("comboBox4.Items1"),
            resources.GetString("comboBox4.Items2")});
        }
        private void _ChangeLanguage(Control c, ComponentResourceManager resources, CultureInfo cult)
        {
            resources.ApplyResources(c, c.Name, cult);
            foreach (Control ch in c.Controls)
            {
                _ChangeLanguage(ch, resources, cult);
            }
        }

        private void _ChangeMenuStripLanguage(ToolStripItemCollection collection, ComponentResourceManager resources, CultureInfo cult)
        {
            foreach (ToolStripItem m in collection)
            {
                resources.ApplyResources(m, m.Name, cult);
                var item = m as ToolStripDropDownItem;
                if (item != null) 
                { 
                    _ChangeMenuStripLanguage(item.DropDownItems, resources, cult);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
