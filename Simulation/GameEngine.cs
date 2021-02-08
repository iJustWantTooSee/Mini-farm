using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulation
{
    public class GameEngine
    {
        public const int SPEED_LIFE = 200;
        public const sbyte PRICE_OF_SPROUTS = -2;
        public const sbyte PRICE_OF_SEED = 1;
        public const sbyte PRICE_OF_ASCEND = 2;
        public const sbyte PRICE_OF_ALMOST_RIPE = 3;
        public const sbyte PRICE_OF_RIPE = 5;
        public const sbyte PRICE_OF_SPOILED = -2;
        public int CurrentMoney { get; private set; } = 100;
        public ulong CurrentTime { get; set; } = 0;

        public Dictionary<Button, Cell> Field { get; private set; } = new Dictionary<Button, Cell>();

        public GameEngine(SplitContainer splitContainer)
        {
            foreach (Button cb in splitContainer.Panel2.Controls)
            {
                Field[cb] = new Cell();
                cb.Text = $"Уровень спелости: {Field[cb].CurrentGrowth} \n"
                    + $"Состояние: {Field[cb].State}";
            }
        }

        public bool IsNeedToRedrawn(Button landingPlace)
        {
            if (Field[landingPlace].State == Enums.State.Empty)
            {
                if (CurrentMoney > 1)
                {
                    Field[landingPlace].Fill();
                    CurrentMoney += PRICE_OF_SPROUTS;
                    return true;
                }
                else
                {
                    MessageBox.Show("У вас закончились деньги! Если хотите продолжить, переведите 1 балл на мой аккаунт в GoogleClass");
                }
            }
            else
            {
                SellTheProduct(Field[landingPlace].GetProduct());
                return true;
            }
            return false;
        }

        private void SellTheProduct(Enums.State stateTheProduct)
        {
            switch (stateTheProduct)
            {
                case Enums.State.Seeded:
                    CurrentMoney += PRICE_OF_SEED;
                    break;
                case Enums.State.Ascended:
                    CurrentMoney += PRICE_OF_ASCEND;
                    break;
                case Enums.State.AlmostRipe:
                    CurrentMoney += PRICE_OF_ALMOST_RIPE;
                    break;
                case Enums.State.Ripe:
                    CurrentMoney += PRICE_OF_RIPE;
                    break;
                case Enums.State.Spoiled:
                    CurrentMoney += PRICE_OF_SPOILED;
                    break;
            }
        }

        public int GetSpeedOfLife(int valueOfTracBar)
        {
            int currentSpeedLife = valueOfTracBar;
            if (currentSpeedLife > 5)
            {
                return SPEED_LIFE - (currentSpeedLife * 7);
            }
            else if (currentSpeedLife < 5)
            {
                return SPEED_LIFE * (5 - currentSpeedLife);
            }

            return SPEED_LIFE;
        }
    }
}
