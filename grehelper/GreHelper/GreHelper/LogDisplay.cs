using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GreHelper
{
    public partial class LogDisplay : Form
    {
        public LogDisplay(Logger logger)
        {
            InitializeComponent();
            textBox1.Lines = logger.ReadRecords();
        }
    }
}
