using System.Collections.Generic;
using System.Linq;

namespace Example.Factories
{
    public class JobsFactory : BaseFactory<List<JobLib.Job>>
    {
        private readonly Job[] Jobs;
        public JobsFactory(Sample sample)
        {
            Jobs = sample.Jobs;
        }

        public override List<JobLib.Job> Build()
        {
            return Jobs.Select(job => new JobFactory(job).Build()).ToList();
        }
    }
}
