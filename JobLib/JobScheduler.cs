using JobLib.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace JobLib
{
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

        protected void AddJobToQueue(Job job)
        {
            if (Queues.Count == 0)
            {
                Queues.Add(EnqueueJob(job, new Queue<Job>()));

                return;
            }

            var queuesLength = Queues.Count;

            for (var queueRow = 0; queueRow < queuesLength; queueRow++)
            {
                if (QueuesScore.HasQueueReachedScore(queueRow))
                {
                    continue;
                }

                EnqueueJob(job, Queues[queueRow], queueRow);

                return;
            }

            Queues.Add(EnqueueJob(job, new Queue<Job>()));
        }

        protected Queue<Job> EnqueueJob(Job job, Queue<Job> queue, int queuePosition = 0)
        {
            if (queue.Count > 0 && QueuesScore.HasQueueReachedScore(queuePosition, job.Duration()))
            {
                ReorderQueue(job, queue, queuePosition);

                return queue;
            }

            queue.Enqueue(job);
            QueuesScore.RefreshScore(queuePosition, job.EstimatedTime.ToSeconds());

            return queue;
        }

        protected void ReorderQueue(Job job, Queue<Job> queue, int queuePosition)
        {
            var remainingJobs = new List<Job>();
            var hasReordered = false;

            foreach (var queueJob in queue)
            {
                if (QueuesScore.HasReachedScore(job.Duration() + queueJob.Duration()))
                {
                    remainingJobs = queue.Where(j => j.Id != queueJob.Id).ToList();
                    var rebalancedQueue = new Queue<Job>();
                    rebalancedQueue.Enqueue(queueJob);
                    rebalancedQueue.Enqueue(job);
                    Queues[queuePosition] = rebalancedQueue;
                    QueuesScore.ResetScore(queuePosition, job.Duration() + queueJob.Duration());

                    hasReordered = true;

                    break;
                }

                remainingJobs.Add(queueJob);
            }

            if (!hasReordered)
            {
                var rebalancedQueue = new Queue<Job>();
                rebalancedQueue.Enqueue(job);
                Queues[queuePosition] = rebalancedQueue;
            }

            foreach (var remainingJob in remainingJobs)
            {
                AddJobToQueue(remainingJob);
            }
        }
    }
}
