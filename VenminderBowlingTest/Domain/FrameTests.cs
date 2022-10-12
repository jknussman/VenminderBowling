using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace VenminderBowlingTest.Domain
{
    [TestFixture]
    internal class FrameTests
    {
        [TestCase]
        public void ZeroRollsTest()
        {
            var frame = new Frame(1);

            frame.FrameNumber.Should().Be(1);
            frame.Score.Should().BeNull();
            frame.HasSpare.Should().BeFalse();
            frame.HasStrike.Should().BeFalse();
            frame.FrameComplete.Should().BeFalse();
            frame.IsListeningToNextRoll.Should().BeFalse();
            frame.ToString().Should().Be($"Frame {1}: ");
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        public void TestOneRollNotStrike(int score)
        {
            var frame = new Frame(1);
            frame.AddRoll(new Roll(score));

            frame.FrameNumber.Should().Be(1);
            frame.Score.Should().Be(score);
            frame.HasSpare.Should().BeFalse();
            frame.HasStrike.Should().BeFalse();
            frame.FrameComplete.Should().BeFalse();
            frame.IsListeningToNextRoll.Should().BeFalse();
            frame.ToString().Should().Be($"Frame {1}: {score}");
        }
        [TestCase]
        public void TestOneRollStrike()
        {
            var frame = new Frame(1);
            frame.AddRoll(new Roll(10));

            frame.FrameNumber.Should().Be(1);
            frame.Score.Should().Be(10);
            frame.HasSpare.Should().BeFalse();
            frame.HasStrike.Should().BeTrue();
            frame.FrameComplete.Should().BeTrue();
            frame.IsListeningToNextRoll.Should().BeTrue();
            frame.ToString().Should().Be($"Frame {1}: {10}");
        }

        [TestCase]
        public void TestTwoRollsNotStrikeNotSpare()
        {
            var frame = new Frame(1);
            frame.AddRoll(new Roll(5));
            frame.AddRoll(new Roll(4));

            frame.FrameNumber.Should().Be(1);
            frame.Score.Should().Be(9);
            frame.HasSpare.Should().BeFalse();
            frame.HasStrike.Should().BeFalse();
            frame.FrameComplete.Should().BeTrue();
            frame.IsListeningToNextRoll.Should().BeFalse();
            frame.ToString().Should().Be($"Frame {1}: {9}");
        }

        [TestCase]
        public void TestTwoRollsHasSpare()
        {
            var frame = new Frame(1);
            frame.AddRoll(new Roll(5));
            frame.AddRoll(new Roll(5));

            frame.FrameNumber.Should().Be(1);
            frame.Score.Should().Be(10);
            frame.HasSpare.Should().BeTrue();
            frame.HasStrike.Should().BeFalse();
            frame.FrameComplete.Should().BeTrue();
            frame.IsListeningToNextRoll.Should().BeTrue();
            frame.ToString().Should().Be($"Frame {1}: {10}");
        }

        [TestCase]
        public void TestTwoRollsHasStrike()
        {
            var frame = new Frame(1);
            frame.AddRoll(new Roll(10));
            frame.AddRoll(new Roll(5));

            frame.FrameNumber.Should().Be(1);
            frame.Score.Should().Be(15);
            frame.HasSpare.Should().BeFalse();
            frame.HasStrike.Should().BeTrue();
            frame.FrameComplete.Should().BeTrue();
            frame.IsListeningToNextRoll.Should().BeTrue();
            frame.ToString().Should().Be($"Frame {1}: {15}");
        }

        [TestCase(10, true, false)]
        [TestCase(5, false, true)]
        public void TestThreeRolls(int rollOneScore, bool expectedHasStrike, bool expectedHasSpare)
        {
            var frame = new Frame(1);
            frame.AddRoll(new Roll(rollOneScore));
            frame.AddRoll(new Roll(5));
            frame.AddRoll(new Roll(5));

            frame.FrameNumber.Should().Be(1);
            frame.Score.Should().Be(10+rollOneScore);
            frame.HasSpare.Should().Be(expectedHasSpare);
            frame.HasStrike.Should().Be(expectedHasStrike);
            frame.FrameComplete.Should().BeTrue();
            frame.IsListeningToNextRoll.Should().BeFalse();
            frame.ToString().Should().Be($"Frame {1}: {10+rollOneScore}");
        }
        [TestCase]
        public void TestCannotAddForthRoll()
        {
            var frame = new Frame(1);
            frame.AddRoll(new Roll(5));
            frame.AddRoll(new Roll(5));
            frame.AddRoll(new Roll(5));

            Action action = () => frame.AddRoll(new Roll(4));
            action.Should().Throw<InvalidOperationException>();
        }
        [TestCase]
        public void TestRollsTooLarge()
        {
            var frame = new Frame(1);
            frame.AddRoll(new Roll(9));

            Action action = () => frame.AddRoll(new Roll(2));
            action.Should().Throw<InvalidOperationException>();
        }

        [TestCase(0)]
        [TestCase(11)]
        public void TestInvalidFrameNumber(int frameNumber)
        {
            Action action = () => new Frame(frameNumber);
            action.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
