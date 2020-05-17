using JobLib.Contracts;
using System.Collections.Generic;

namespace JobLib
{
    public class JobScheduler
    {
        private readonly List<Queue<Job>> Queues = new List<Queue<Job>>();
        private readonly Dictionary<int, int> QueuesScore = new Dictionary<int, int>();
        private readonly ExecutionWindow ExecutionWindow;
        private readonly EstimatedTime MaxEstimatedTime;
        private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

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

        public Job[][] ToArray()
        {
            return Queues.ConvertAll(queue => queue.ToArray()).ToArray();
        }

        protected void AddJobToQueue(Job job)
        {
            if (Queues.Count == 0)
            {
                var queue = new Queue<Job>();
                EnqueueJob(job, queue);

                return;
            }

            int queuesLength = Queues.Count;

            for (int queueRow = 0; queueRow <= queuesLength; queueRow++)
            {
                if (QueuesScore[queueRow] >= MaxEstimatedTime.ToHours())
                {
                    continue;
                }

                EnqueueJob(job, Queues[queueRow], queueRow);
            }
        }

        protected void EnqueueJob(Job job, Queue<Job> queue, int queuePosition = 0)
        {
            queue.Enqueue(job);
            QueuesScore.Add(queuePosition, job.EstimatedTime.ToHours());
        }
    }
}
