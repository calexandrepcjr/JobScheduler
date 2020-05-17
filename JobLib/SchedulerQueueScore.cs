using System.Collections.Generic;

namespace JobLib
{
    public class SchedulerQueueScore
    {
        private readonly Dictionary<int, int> QueuesScore;
        private int MaxScore;

        public SchedulerQueueScore(int maxScore = 0, Dictionary<int, int> dict = null)
        {
            QueuesScore = dict ?? new Dictionary<int, int>();
            MaxScore = maxScore;
        }

        public void UpdateMaxScore(int maxScore)
        {
            MaxScore = maxScore;
        }

        public int GetScore(int queuePosition = 0)
        {
            return !QueuesScore.ContainsKey(queuePosition) ? -1 : QueuesScore[queuePosition];
        }

        public bool HasQueueReachedScore(int queuePosition, int additionalScore = 0)
        {
            return this.GetScore(queuePosition) + additionalScore >= MaxScore;
        }

        public bool HasReachedScore(int score)
        {
            return score >= MaxScore;
        }

        public void ResetScore(int queuePosition, int score = 0)
        {
            QueuesScore[queuePosition] = score;
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
