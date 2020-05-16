using JobScheduler.Contracts;
using System;

namespace JobScheduler
{
    public class Job
    {
        public readonly int id;
        public readonly string description;
        public readonly DateTime expiresAt;
        public readonly EstimatedTime estimatedTime;

        public Job(int id, string description, DateTime expiresAt, EstimatedTime estimatedTime)
        {
            this.id = id;
            this.description = description;
            this.expiresAt = expiresAt;
            this.estimatedTime = estimatedTime;
        }

        public int Duration()
        {
            return estimatedTime.ToSeconds();
        }
    }
}
