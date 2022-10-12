using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VenminderBowling.Domain.Interfaces
{
    public interface IGame
    {
        IFrame CurrentFrame { get; }

        bool GameComplete { get; }

        int CurrentScore { get; }

        void NewGame();

        void AddRoll(int score);

        string ToString();
    }
}
