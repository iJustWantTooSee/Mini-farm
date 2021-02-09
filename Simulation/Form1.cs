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
        private Dictionary<Button, int> _position = new Dictionary<Button, int>();
        private GameEngine _gameEngine = null;


        public Form1()
        {
            InitializeComponent();
            InitializationColors();

            int countOfButton = 0;
            foreach (Button cb in splitContainer1.Panel2.Controls)
            {
                _position.Add(cb,countOfButton++);
                cb.Text = $"Уровень спелости: 0 \n"
                    + $"Состояние: Empty";
            }
            
            _gameEngine = new GameEngine(countOfButton);
            
            timer1.Interval = _gameEngine.GetSpeedOfLife(trackBar1.Value);
            timer1.Start();
            
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
            Dictionary<int, Cell> field = _gameEngine.Field;

            foreach (Button cb in splitContainer1.Panel2.Controls)
            {
                if (field[_position[cb]].IsUpdate())
                {
                    flag = true;
                }
                if (field[_position[cb]].State == Enums.State.Seeded)
                {
                    cb.ForeColor = Color.White;
                }
                else
                {
                    cb.ForeColor = Color.Black;
                }
                cb.Text = $"Уровень спелости: {field[_position[cb]].CurrentGrowth} \n"
                   + $"Состояние: {field[_position[cb]].State}";
            }
            if (flag)
            {
                DrawField();
            }
        }

        private void DrawField()
        {
            Dictionary<int, Cell> field= _gameEngine.Field; 
            foreach (Button cb in splitContainer1.Panel2.Controls)
            {
                cb.BackColor = _colors[field[_position[cb]].State];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (_gameEngine.IsNeedToRedrawn(_position[button]))
            {
                DrawField();
            }
            else
            {
                MessageBox.Show("У вас закончились деньги! Если хотите продолжить, переведите 1 балл на мой аккаунт в GoogleClass");
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
