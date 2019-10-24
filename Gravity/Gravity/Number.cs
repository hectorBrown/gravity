using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gravity
{
    public partial class Number : Form
    {

        public int Result;
        private Form1 form1;

        public Number(Form1 form1In)
        {
            InitializeComponent();
            form1 = form1In;
        }

        private void BUT_submit_Click(object sender, EventArgs e)
        {
            Result = Convert.ToInt32(NUD_main.Value);
            Hide();
            form1.RandomObjs();
        }
    }
}
