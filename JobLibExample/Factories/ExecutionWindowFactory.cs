using System;
using JobLib;
using Test.Factories;

namespace Example.Factories
{
    public class ExecutionWindowFactory : BaseFactory<ExecutionWindow>
    {
        private readonly ExecutionWindow Window;
        public ExecutionWindowFactory(Sample sample)
        {
            if (sample.ExecutionWindow.Length < 2)
            {
                throw new ArgumentException("Sample ExecutionWindow must be a DateString Tuple");
            }

            var date1 = new DateFactory(sample.ExecutionWindow[0]).Build();
            var date2 = new DateFactory(sample.ExecutionWindow[1]).Build();

            Window = new ExecutionWindow(date1, date2);
        }

        public override ExecutionWindow Build()
        {
            return Window;
        }
    }
}
