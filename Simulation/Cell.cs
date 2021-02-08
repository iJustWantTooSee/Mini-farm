using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulation.Enums;

namespace Simulation
{
    public class Cell
    {
        public const uint SEEDED = 20;
        public const uint ASCENDED = 55;
        public const uint ALMOST_RIPE = 90;
        public const uint RIPE = 125;
        public const uint SPOILED = 140;

        public State State { get; set; } = State.Empty;
        public uint CurrentGrowth { get; private set; } = 0;

        public void Fill()
        {
            State = State.Seeded;
        }

        public bool IsUpdate()
        {
            if (State == State.Empty)
                return false;
            CurrentGrowth++;
            return IsChangeState();
        }

        private bool IsChangeState()
        {
            Enums.State previousState = this.State;
            if ((CurrentGrowth >= SEEDED) && (CurrentGrowth < ASCENDED))
            {
                this.State = State.Ascended;
            }
            else if ((CurrentGrowth >= ASCENDED) && (CurrentGrowth < ALMOST_RIPE))
            {
                this.State = State.Ascended;
            }
            else if ((CurrentGrowth >= ALMOST_RIPE) && (CurrentGrowth < RIPE))
            {
                this.State = State.AlmostRipe;
            }
            else if ((CurrentGrowth >= RIPE) && (CurrentGrowth < SPOILED))
            {
                this.State = State.Ripe;
            }
            else if (CurrentGrowth >= SPOILED)
            {
                this.State = State.Spoiled;
            }

            return previousState != State;
        }

        public State GetProduct()
        {
            Enums.State currentState = this.State;
            CurrentGrowth = 0;
            this.State = State.Empty;

            return currentState;
        }
    }
}
