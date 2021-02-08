using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulation
{
    public partial class Form1 : Form
    {
        private Dictionary<Enums.State, Color> _colors = new Dictionary<Enums.State, Color>();

        private GameEngine _gameEngine = null;


        public Form1()
        {
            InitializeComponent();
            _gameEngine = new GameEngine(splitContainer1);
            InitializationColors();
            label1.Text = $"Прошло дней со старта: {_gameEngine.CurrentTime++}";
            label2.Text = $"Текущий бюджет: {_gameEngine.CurrentMoney}";
        }

        private void InitializationColors()
        {
            _colors.Add(Enums.State.Empty, Color.White);
            _colors.Add(Enums.State.Seeded, Color.Black);
            _colors.Add(Enums.State.Ascended, Color.Green);
            _colors.Add(Enums.State.AlmostRipe, Color.Yellow);
            _colors.Add(Enums.State.Ripe, Color.Red);
            _colors.Add(Enums.State.Spoiled, Color.Brown);
        }


        private void UpdatePanel()
        {
            bool flag = false;
            Dictionary<Button, Cell> field = _gameEngine.Field;

            foreach (Button cb in splitContainer1.Panel2.Controls)
            {
                if (field[cb].IsUpdate())
                {
                    flag = true;
                }
                if (field[cb].State == Enums.State.Seeded)
                {
                    cb.ForeColor = Color.White;
                }
                else
                {
                    cb.ForeColor = Color.Black;
                }
                cb.Text = $"Уровень спелости: {field[cb].CurrentGrowth} \n"
                   + $"Состояние: {field[cb].State}";
            }
            if (flag)
            {
                DrawField();
            }
        }

        private void DrawField()
        {
            Dictionary<Button, Cell> field= _gameEngine.Field; 
            foreach (Button cb in splitContainer1.Panel2.Controls)
            {
                cb.BackColor = _colors[field[cb].State];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (_gameEngine.IsNeedToRedrawn(button))
            {
                DrawField();
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdatePanel();
            label1.Text = $"Прошло дней со старта: {_gameEngine.CurrentTime++}";
            label2.Text = $"Текущий бюджет: {_gameEngine.CurrentMoney}";

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = _gameEngine.GetSpeedOfLife(trackBar1.Value);
        }
    }
}
