﻿namespace JobLib
{
    using JobLib.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class JobScheduler
    {
        private readonly List<Queue<Job>> Queues = new List<Queue<Job>>();

        private readonly SchedulerQueueScore QueuesScore = new SchedulerQueueScore();

        private readonly ExecutionWindow ExecutionWindow;

        private readonly EstimatedTime MaxEstimatedTime;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public JobScheduler(ExecutionWindow window, EstimatedTime maxEstimatedTime)
        {
            ExecutionWindow = window;
            MaxEstimatedTime = maxEstimatedTime;
            QueuesScore.UpdateMaxScore(MaxEstimatedTime.ToSeconds());
        }

        public void Schedule(Job job)
        {
            if (!ExecutionWindow.IsIn(job.ExpiresAt))
            {
                Logger.Error("Schedule aborted - Job outside Execute Window", job);
                return;
            }

            if (job.EstimatedTime.GreaterThan(MaxEstimatedTime))
            {
                Logger.Error("Schedule aborted - Job outside max estimation time", job);
                return;
            }

            AddJobToQueue(job);
        }

        public EstimatedTime MaxJobDuration()
        {
            return MaxEstimatedTime;
        }

        public List<Queue<Job>> GetQueues()
        {
            return Queues;
        }

        public int[][] ToArray()
        {
            return Queues.ConvertAll(queue => queue.Select(job => job.Id).ToArray()).ToArray();
        }

        protected void AddJobToQueue(Job job, int queueRow = 0)
        {
            if (Queues.Count <= queueRow)
            {
                Queues.Add(EnqueueJob(job, new Queue<Job>(), queueRow));

                return;
            }

            if (QueuesScore.EqualsScore(job.Duration()))
            {
                Queues.Add(EnqueueJob(job, new Queue<Job>(), Queues.Count));

                return;
            }

            while (queueRow < Queues.Count)
            {
                if (!IsSchedulable(queueRow, job))
                {
                    queueRow++;
                    continue;
                }

                EnqueueJob(job, Queues[queueRow], queueRow);

                return;
            }

            Queues.Add(EnqueueJob(job, new Queue<Job>(), queueRow));
        }

        protected bool IsSchedulable(int queuePosition, Job job)
        {
            return !QueuesScore.HasQueueReachedScore(queuePosition)
                   && !(QueuesScore.HasQueueSurpassedScore(queuePosition, job.Duration())
                       && Queues[queuePosition].Count == 1);
        }

        protected Queue<Job> EnqueueJob(Job job, Queue<Job> queue, int queuePosition = 0)
        {
            if (queue.Count > 0 && QueuesScore.HasQueueSurpassedScore(queuePosition, job.Duration()))
            {
                ReorderQueue(job, queue, queuePosition);

                return queue;
            }

            queue.Enqueue(job);
            QueuesScore.RefreshScore(queuePosition, job.Duration());

            return queue;
        }

        protected void ReorderQueue(Job job, Queue<Job> queue, int queuePosition)
        {
            var remainingJobs = new List<Job>();
            var rebalancedQueue = new Queue<Job>();

            foreach (var queueJob in queue)
            {
                if (QueuesScore.CompareScore(job.Duration() + queueJob.Duration()).Equals(0))
                {
                    remainingJobs = queue.Where(j => j.Id != queueJob.Id).ToList();
                    rebalancedQueue.Enqueue(queueJob);
                    rebalancedQueue.Enqueue(job);
                    Queues[queuePosition] = rebalancedQueue;
                    QueuesScore.ResetScore(queuePosition, job.Duration() + queueJob.Duration());

                    break;
                }

                remainingJobs.Add(queueJob);
            }

            if (rebalancedQueue.Count == 0)
            {
                rebalancedQueue.Enqueue(job);
                Queues[queuePosition] = rebalancedQueue;
                QueuesScore.ResetScore(queuePosition, job.Duration());
            }

            foreach (var remainingJob in remainingJobs)
            {
                AddJobToQueue(remainingJob, ++queuePosition);
            }
        }
    }
}
