# job-scheduler

An experiment scheduling jobs in C#

I expect to create a lib that helps organizing jobs.

Given an execution window tuple [2019-11-10 09:00:00, 2019-11-11 12:00:00] with a job list sample,
the scheduler will organize chunks of jobs, which each job list will execute sequentially.

## How to see it working

1. Unzip [v0.2 release](https://github.com/calexandrepcjr/JobScheduler/releases/tag/v0.2);
2. Execute Example.exe with the sample filename as argument (there is already one in the Example directory) - the example will be fed by a [valid](https://tools.ietf.org/html/rfc8259) JSON.

The sample schema can be seen [here](https://github.com/calexandrepcjr/JobScheduler/blob/master/JobLibExample/sample.json).
