using System;
using System.Collections.Generic;
using System.Text;

namespace JobLib
{
    public class SchedulerQueueScore
    {
        private readonly Dictionary<int, int> QueuesScore;

        public SchedulerQueueScore(Dictionary<int, int> dict = null)
        {
            QueuesScore = dict ?? new Dictionary<int, int>();
        }

        public int GetScore(int queueRow = 0)
        {
            return !QueuesScore.ContainsKey(queueRow) ? -1 : QueuesScore[queueRow];
        }

        public bool HasReachedScore(int queueRow, int score)
        {
            return this.GetScore(queueRow) >= score;
        }

        public void RefreshScore(int queuePosition, int score)
        {
            if (QueuesScore.ContainsKey(queuePosition))
            {
                QueuesScore[queuePosition] += score;
                return;
            }

            QueuesScore.Add(queuePosition, score);
        }
    }
}
