using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fifteen
{
    public partial class Form1 : Form
    {

        static int gridSize = 4;

        public Form1()
        {
            InitializeComponent();

            this.KeyPreview = true;

            this.gamer.GameSolved += Gamer_GameSolved;
        }

        private void Gamer_GameSolved(object sender, EventArgs e)
        {
            int moves = ((GameSolvedEventArgs)e).moves;
            MessageBox.Show(this, $"Hurray! Game solved! {Environment.NewLine}Moves: {moves}", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            if (gamer.loaded && MessageBox.Show(this, "This will reset current progress!\r\nAre you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images|*.jpg";
            ofd.Title = "Select image";

            if (ofd.ShowDialog() == DialogResult.OK) gamer.LoadImage(ofd.FileName, gridSize);
        }

        private void RandomizeButton_Click(object sender, EventArgs e)
        {
            if (!gamer.loaded)
            {
                MessageBox.Show(this, "No image loaded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < RandomStepsNUD.Value; i++)
            {
                gamer.RandomizeSwap();
                System.Diagnostics.Debug.WriteLine($"DD: {i}");
            }
        }
    }
}
