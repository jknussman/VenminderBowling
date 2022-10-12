using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VenminderBowling.Domain.Interfaces
{
    public interface IFrame
    {
        int FrameNumber { get; }

        int? Score { get; }

        bool HasSpare { get; }

        bool HasStrike { get; }

        bool FrameComplete { get; }

        bool IsListeningToNextRoll { get; }

        void AddRoll(IRoll roll);

        string ToString();
    }
}
