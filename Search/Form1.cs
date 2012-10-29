using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Search
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string res = Helper.GetSearchResultHtlm(txtKeyword.Text);
            List<String> result = Helper.ParseSearchResultHtml(res);

            foreach (var VARIABLE in result)
            {
                lstResults.Items.Add(VARIABLE);
            }
        }
    }
}
