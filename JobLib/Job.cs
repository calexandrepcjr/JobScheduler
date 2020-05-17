using JobLib.Contracts;
using System;

namespace JobLib
{
    public class Job
    {
        public readonly int Id;
        public readonly string Description;
        public readonly DateTime ExpiresAt;
        public readonly EstimatedTime EstimatedTime;

        public Job(int id, string description, DateTime expiresAt, EstimatedTime estimatedTime)
        {
            Id = id;
            Description = description;
            ExpiresAt = expiresAt;
            EstimatedTime = estimatedTime;
        }

        public int Duration()
        {
            return EstimatedTime.ToSeconds();
        }
    }
}
