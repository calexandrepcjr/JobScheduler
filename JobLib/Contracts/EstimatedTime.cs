using System;
using System.Collections.Generic;

namespace JobLib.Contracts
{
    public abstract class EstimatedTime
    {
        protected readonly int EstimationInSeconds;

        protected EstimatedTime(int estimation)
        {
            EstimationInSeconds = estimation;
        }

        protected EstimatedTime(string estimation)
        {
            string[] splitEstimation = estimation.Split(' ');

            if (splitEstimation.Length < 2 || !this.EstimationLogic.ContainsKey(splitEstimation[1]))
            {
                throw new ArgumentException("Estimation must be in the format: %d time period");
            }

            EstimationInSeconds = EstimationLogic[splitEstimation[1]](Convert.ToInt32(splitEstimation[0]));
        }
        protected abstract Dictionary<string, Func<int, int>> EstimationLogic
        {
            get;
        }

        public int ToSeconds() => EstimationInSeconds;

        public int ToMinutes() => EstimationInSeconds / 60;

        public int ToHours() => EstimationInSeconds / 3600;

        public bool LessThanOrEqual(EstimatedTime estimatedTime) =>
            EstimationInSeconds <= estimatedTime.EstimationInSeconds;

        public bool GreaterThanOrEqual(EstimatedTime estimatedTime) =>
            EstimationInSeconds >= estimatedTime.EstimationInSeconds;

        public bool LessThan(EstimatedTime estimatedTime) =>
            EstimationInSeconds < estimatedTime.EstimationInSeconds;

        public bool GreaterThan(EstimatedTime estimatedTime) =>
            EstimationInSeconds > estimatedTime.EstimationInSeconds;
    }
}
