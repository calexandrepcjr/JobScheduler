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
                if (QueuesScore.HasReachedScore(queueRow, MaxEstimatedTime.ToSeconds()))
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
            queue.Enqueue(job);
            QueuesScore.RefreshScore(queuePosition, job.EstimatedTime.ToSeconds());

            return queue;
        }
    }
}
