using Test.Factories;

namespace Example.Factories
{
    public class JobFactory : BaseFactory<JobLib.Job>
    {
        private readonly Job SampleJob;
        public JobFactory(Job job)
        {
            SampleJob = job;
        }

        public override JobLib.Job Build()
        {
            var expiresAt = new DateFactory(SampleJob.ExpiresAt).Build();
            var estimatedTime = new EstimatedTimeBRFactory(SampleJob.EstimatedTime).Build();

            return new JobLib.Job(SampleJob.Id, SampleJob.Description, expiresAt, estimatedTime);
        }
    }
}
