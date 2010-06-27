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
    public partial class frmMain : Form
    {
        private WordProvider wordProvider = null;
        private Logger logger = new Logger();
        private ChoiceRecord currentWord = null;

        public frmMain()
        {
            InitializeComponent();
            wordProvider = WordProvider.CreateFromResource("mywordlist", logger);
            DoNextWord();
        }

        private void btnNextWord_Click(object sender, EventArgs e)
        {
            DoNextWord();
        }
        
        private void ClearEverything()
        {
            pbStatus.Visible = false;

            foreach(Control ctrl in groupBox1.Controls)
            {
                if(ctrl is RadioButton && ctrl.Name.Contains("optOption"))
                {
                    (ctrl as RadioButton).Checked = false;
                }
                if(ctrl is PictureBox && ctrl.Name.Contains("pbOption"))
                {
                    ctrl.Visible = false;
                }
            }
        }

        private void FillOptions()
        {
            optOption1.Text = currentWord.Options[0];
            optOption2.Text = currentWord.Options[1];
            optOption3.Text = currentWord.Options[2];
            optOption4.Text = currentWord.Options[3];
        }

        private void DoNextWord()
        {
            ClearEverything();
            currentWord = wordProvider.GetRandomWordWithChoices();
            lblCurrentWord.Text = currentWord.Word;
            FillOptions();
        }

        private void btnShowLogs_Click(object sender, EventArgs e)
        {
            LogDisplay logDisplay = new LogDisplay(logger);
            logDisplay.ShowDialog();
        }

        private void Success()
        {
            pbStatus.Image = Properties.Resources.correct;
            pbStatus.Visible = true;
        }

        private void Failure()
        {
            pbStatus.Image = Properties.Resources.incorrect;
            pbStatus.Visible = true;
            UpdateInformationTabs();
        }

        private void UpdateInformationTabs()
        {
            wbDictionary.Navigate(string.Format("dictionary.reference.com/browse/{0}", currentWord.Word));
            wbThesaurus.Navigate(string.Format("thesaurus.reference.com/browse/{0}", currentWord.Word));
            wbNews.Navigate(string.Format("http://www.bing.com/news/search?q={0}", currentWord.Word));
            wbWebSearch.Navigate(string.Format("http://www.bing.com/search?q={0}", currentWord.Word));
        }

        private void CheckResponse(string currentSelectionText)
        {
            if (string.Compare(currentWord.Definition, currentSelectionText, true) == 0)
            {
                Success();
            }
            else
            {
                Failure();
            }
        }

        private void optOption1_CheckedChanged(object sender, EventArgs e)
        {
            if (optOption1.Checked)
                CheckResponse(optOption1.Text);
        }

        private void optOption2_CheckedChanged(object sender, EventArgs e)
        {
            if (optOption2.Checked)
                CheckResponse(optOption2.Text);
        }

        private void optOption3_CheckedChanged(object sender, EventArgs e)
        {
            if(optOption3.Checked)
                CheckResponse(optOption3.Text);
        }

        private void optOption4_CheckedChanged(object sender, EventArgs e)
        {
            if (optOption4.Checked)
                CheckResponse(optOption4.Text);
        }

        private void ShowAnswer()
        {
            string controlName = string.Format("pbOption{0}", currentWord.CorrectChoiceIndex + 1);
            Control ctrl = Controls.Find(controlName, true)[0];
            if(ctrl is PictureBox)
            {
                ctrl.Visible = true;
            }
            UpdateInformationTabs();
        }

        private void btnShowResponse_Click(object sender, EventArgs e)
        {
            ShowAnswer();
        }

    }
}
