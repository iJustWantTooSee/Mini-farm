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
        public const int SPEED_LIFE = 200;
        public const sbyte PRICE_OF_SPROUTS = -2;
        public const sbyte PRICE_OF_SEED = 1;
        public const sbyte PRICE_OF_ASCEND = 2;
        public const sbyte PRICE_OF_ALMOST_RIPE = 3;
        public const sbyte PRICE_OF_RIPE = 5;
        public const sbyte PRICE_OF_SPOILED = -2;

        private Dictionary<Button, Cell> _field = new Dictionary<Button, Cell>();
        private Dictionary<Enums.State, Color> _colors = new Dictionary<Enums.State, Color>();
        
        private int _currentMoney = 100;
        private ulong _currentTime = 0;

        public Form1()
        {
            InitializeComponent();
            foreach (Button cb in splitContainer1.Panel2.Controls)
            {
                _field[cb] = new Cell();
                cb.Text = $"Уровень спелости: {_field[cb].CurrentGrowth} \n"
                    + $"Состояние: {_field[cb].State}";
            }
            InitializationColors();
            label1.Text = $"Прошло дней со старта: {_currentTime++}";
            label2.Text = $"Текущий бюджет: {_currentMoney}";
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
            foreach (Button cb in splitContainer1.Panel2.Controls)
            {
                if (_field[cb].IsUpdate())
                {
                    flag = true;
                }
                if (_field[cb].State == Enums.State.Seeded)
                {
                    cb.ForeColor = Color.White;
                }
                else
                {
                    cb.ForeColor = Color.Black;
                }
                cb.Text = $"Уровень спелости: {_field[cb].CurrentGrowth} \n"
                   + $"Состояние: {_field[cb].State}";
            }
            if (flag)
            {
                DrawField();
            }
        }

        private void DrawField()
        {
            foreach (Button cb in splitContainer1.Panel2.Controls)
            {
                cb.BackColor = _colors[_field[cb].State];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (_field[button].State == Enums.State.Empty)
            {
                if (_currentMoney > 1)
                {
                    _field[button].Fill();
                    _currentMoney += PRICE_OF_SPROUTS;
                    button.BackColor = _colors[_field[button].State];
                }
                else
                {
                    MessageBox.Show("У вас закончились деньги! Если хотите продолжить, переведите 1 балл на мой аккаунт в GoogleClass");
                }
            }
            else
            {
                SellTheProduct(_field[button].GetProduct());
                DrawField();
            }
        }

        private void SellTheProduct(Enums.State stateTheProduct)
        {
            switch (stateTheProduct)
            {
                case Enums.State.Seeded:
                    _currentMoney += PRICE_OF_SEED;
                    break;
                case Enums.State.Ascended:
                    _currentMoney += PRICE_OF_ASCEND;
                    break;
                case Enums.State.AlmostRipe:
                    _currentMoney += PRICE_OF_ALMOST_RIPE;
                    break;
                case Enums.State.Ripe:
                    _currentMoney += PRICE_OF_RIPE;
                    break;
                case Enums.State.Spoiled:
                    _currentMoney += PRICE_OF_SPOILED;
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdatePanel();
            label1.Text = $"Прошло дней со старта: {_currentTime++}";
            label2.Text = $"Текущий бюджет: {_currentMoney}";

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int currentSpeedLife = trackBar1.Value;
            if (currentSpeedLife > 5)
            {
                timer1.Interval = SPEED_LIFE-(currentSpeedLife*7);
            }
            else if (currentSpeedLife < 5)
            {
                timer1.Interval = SPEED_LIFE * (5-currentSpeedLife);
            }
            else
            {
                timer1.Interval = SPEED_LIFE;
            }

        }
    }
}
