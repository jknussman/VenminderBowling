using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VenminderBowling.Domain
{
    internal class Frame
    {
        private const int MAX_ROLL_SCORE = 10;
        private const int MAX_ROLL_COUNT = 3;

        public int FrameNumber { get; }
        public List<Roll> Rolls { get; } = new List<Roll>(MAX_ROLL_COUNT);

        public int? Score
        {
            get { return Rolls.Count > 0 ? Rolls.Sum(r => r.Score) : null; }
        }

        public bool HasSpare
        {
            get { return Rolls.Count > 1 && Rolls[0].Score + Rolls[1].Score == MAX_ROLL_SCORE; }
        }

        public bool HasStrike
        {
            get { return Rolls.Count >= 1 && Rolls[0].Score == MAX_ROLL_SCORE; }
        }

        public bool FrameComplete
        {
            get { return HasStrike || Rolls.Count >= 2; }
        }

        public bool IsListeningToNextRoll
        {
            get { return HasStrike && Rolls.Count < MAX_ROLL_COUNT || HasSpare && Rolls.Count < MAX_ROLL_COUNT; }
        }

        public Frame(int frameNumber)
        {
            FrameNumber = frameNumber;
        }

        public void AddRoll(Roll roll)
        {
            if (Rolls.Count >= MAX_ROLL_COUNT)
            {
                throw new Exception("A Frame cannot have more than 3 rolls");
            }
            if (Rolls.Count == 1 && HasStrike == false && Rolls[0].Score + roll.Score > MAX_ROLL_SCORE)
            {
                throw new Exception("the 2 rolls of a frame without a strike cannot be greater than 10");
            }
            Rolls.Add(roll);

        }

        public override string ToString()
        {
            return $"Frame {FrameNumber}: {Score}";
        }
    }
}
