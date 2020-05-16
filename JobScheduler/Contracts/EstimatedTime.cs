using System;
using System.Collections.Generic;

namespace JobScheduler.Contracts
{
    public abstract class EstimatedTime
    {
        protected readonly int estimationInSeconds;

        protected EstimatedTime(int estimation)
        {
            estimationInSeconds = estimation;
        }

        protected EstimatedTime(string estimation)
        {
            string[] splitEstimation = estimation.Split(' ');

            if (splitEstimation.Length < 2 || !this.EstimationLogic.ContainsKey(splitEstimation[1]))
            {
                throw new ArgumentException("Estimation must be in the format: %d time period");
            }

            estimationInSeconds = EstimationLogic[splitEstimation[1]](Convert.ToInt32(splitEstimation[0]));
        }
        protected abstract Dictionary<string, Func<int, int>> EstimationLogic
        {
            get;
        }

        public int ToSeconds() => estimationInSeconds;

        public int ToMinutes() => estimationInSeconds / 60;

        public int ToHours() => estimationInSeconds / 3600;
    }
}
