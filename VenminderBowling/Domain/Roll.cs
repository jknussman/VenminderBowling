﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VenminderBowling.Domain
{
    internal class Roll
    {
        public int Score { get; }

        public Roll(int score)
        {
            if (score < 0 || score > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(score));
            }
            Score = score;
        }
    }
}
