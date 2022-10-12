using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VenminderBowling.Domain.Interfaces;

namespace VenminderBowling.Domain
{
    public class Frame : IFrame
    {
        private const int MAX_ROLL_SCORE = 10;
        private const int MAX_ROLL_COUNT = 3;
        private IList<IRoll> Rolls { get; } = new List<IRoll>(MAX_ROLL_COUNT);

        public int FrameNumber { get; }

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
            if(frameNumber < 1 || frameNumber > 10)
            {
                throw new ArgumentOutOfRangeException("Frame Number allowed values are 1-10");
            }
            FrameNumber = frameNumber;
        }

        public void AddRoll(IRoll roll)
        {
            if (Rolls.Count >= MAX_ROLL_COUNT)
            {
                throw new InvalidOperationException("A Frame cannot have more than 3 rolls");
            }
            if (Rolls.Count == 1 && HasStrike == false && Rolls[0].Score + roll.Score > MAX_ROLL_SCORE)
            {
                throw new InvalidOperationException("the 2 rolls of a frame without a strike cannot be greater than 10");
            }
            Rolls.Add(roll);
            if(Rolls.Count == 1 && HasStrike)
            {
                Console.WriteLine("You got a Strike!");
            }
            if(Rolls.Count == 2 && HasSpare)
            {
                Console.WriteLine("You got a Spare!");
            }

        }

        public override string ToString()
        {
            return $"Frame {FrameNumber}: {Score}";
        }
    }
}
