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
        public const int SPEED_LIFE = 2000;
        public const sbyte PRICE_OF_SPROUTS = -2;
        public const sbyte PRICE_OF_SEED = 1;
        public const sbyte PRICE_OF_ASCEND = 2;
        public const sbyte PRICE_OF_ALMOST_RIPE = 3;
        public const sbyte PRICE_OF_RIPE = 5;
        public const sbyte PRICE_OF_SPOILED = -2;
        public int CurrentMoney { get; private set; } = 100;
        public ulong CurrentTime { get; set; } = 0;

        public Dictionary<int, Cell> Field { get; private set; } = new Dictionary<int, Cell>();

        public GameEngine(int amountOfCell)
        {
            for (int i = 0; i < amountOfCell; i++)
            {
                Field.Add(i, new Cell());
            }
        }

        public bool IsNeedToRedrawn(int landingPlace)
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
                    return false;
                }
            }
            else
            {
                SellTheProduct(Field[landingPlace].GetProduct());
                return true;
            }
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
            return SPEED_LIFE / valueOfTracBar;
        }
    }
}
