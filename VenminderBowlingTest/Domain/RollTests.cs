using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VenminderBowlingTest.Domain
{
    [TestFixture]
    public class RollTests
    {
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
        [TestCase(10)]
        public void TestValidValues(int score)
        {
            var roll = new Roll(score);
            roll.Score.Should().Be(score);
        }

        [TestCase(-1)]
        [TestCase(11)]
        public void TestInvalidValues(int score)
        {
            Action act = () => new Roll(score);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
