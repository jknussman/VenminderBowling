using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VenminderBowling.Domain.Interfaces;

namespace VenminderBowling.Domain
{
    public class Game : IGame
    {
        private int _currentFrame;
        private const int TOTAL_FRAMES = 10;

        private List<IFrame> Frames { get; set; } = new List<IFrame>();
        private int CurrentFrameNumber
        {
            get { return _currentFrame + 1; }
        }

        public IFrame CurrentFrame
        {
            get { return Frames[_currentFrame]; }
        }

        public bool GameComplete
        {
            get { return Frames.Last().FrameComplete && Frames.Last().IsListeningToNextRoll == false; }
        }

        public int CurrentScore
        {
            get { return Frames.Sum(f => f.Score != null ? f.Score.Value : 0); }
        }
        public Game()
        {
            NewGame();
        }

        public void NewGame()
        {
            Frames = new List<IFrame>(TOTAL_FRAMES);
            for (_currentFrame = 0; _currentFrame < TOTAL_FRAMES; _currentFrame++)
            {
                Frames.Add(new Frame(CurrentFrameNumber));
            }
            _currentFrame = 0;
        }

        public void AddRoll(int score)
        {
            if (GameComplete)
            {
                throw new InvalidOperationException();
            }
            var roll = new Roll(score);
            //only using a loop here because the collection is always small and not large
            for (int i = 0; i < _currentFrame; i++)
            {
                if (Frames[i].IsListeningToNextRoll)
                {
                    Frames[i].AddRoll(roll);
                }
            }

            Frames[_currentFrame].AddRoll(roll);

            //on final frame bonus rolls we stay on the current frame instead of advancing to next frame
            if (Frames[_currentFrame].FrameComplete && CurrentFrameNumber < TOTAL_FRAMES)
            {
                _currentFrame++;
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var frame in Frames)
            {
                stringBuilder.AppendLine(frame.ToString());
            }
            stringBuilder.AppendLine($"Total Score: {CurrentScore}");
            return stringBuilder.ToString();
        }
    }
}
