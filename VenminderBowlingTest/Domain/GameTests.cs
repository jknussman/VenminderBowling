using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VenminderBowlingTest.Domain
{
    [TestFixture]
    internal class GameTests
    {
        [TestCase]
        public void NewGame()
        {
            var game = new Game();
            game.CurrentFrame.FrameNumber.Should().Be(1);
            game.GameComplete.Should().BeFalse();
            game.CurrentScore.Should().Be(0);
        }

        [TestCase]
        public void ResetGame()
        {
            var game = new Game();
            game.AddRoll(10);
            game.AddRoll(5);
            game.AddRoll(4);

            game.NewGame();

            game.CurrentFrame.FrameNumber.Should().Be(1);
            game.GameComplete.Should().BeFalse();
            game.CurrentScore.Should().Be(0);
        }

        [TestCase]
        public void ScoreStrike()
        {
            var game = new Game();
            game.AddRoll(10);
            game.AddRoll(5);
            game.AddRoll(4);

            game.CurrentFrame.FrameNumber.Should().Be(3);
            game.GameComplete.Should().BeFalse();
            game.CurrentScore.Should().Be(28);
        }

        [TestCase]
        public void ScoreSpare()
        {
            var game = new Game();
            game.AddRoll(5);
            game.AddRoll(5);
            game.AddRoll(5);

            game.CurrentFrame.FrameNumber.Should().Be(2);
            game.GameComplete.Should().BeFalse();
            game.CurrentScore.Should().Be(20);
        }

        [TestCase]
        public void FinalFrameStrikes()
        {
            var game = new Game();
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(10);
            game.AddRoll(10);
            game.AddRoll(10);

            game.CurrentFrame.FrameNumber.Should().Be(10);
            game.GameComplete.Should().BeTrue();
            game.CurrentScore.Should().Be(30);
        }

        [TestCase]
        public void GameCompleteNoMoreRolls()
        {
            var game = new Game();
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            game.AddRoll(0);
            
            var action = () => game.AddRoll(0);
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
