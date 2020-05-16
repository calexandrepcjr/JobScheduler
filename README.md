# job-scheduler

An experiment scheduling jobs in C#

I expect to create a lib that helps organizing jobs.

Given an execution window tuple [2019-11-10 09:00:00, 2019-11-11 12:00:00] with a job list sample,
the scheduler will organize chunks of jobs, which each job list will execute sequentially.

## How to see it working

1. Run the Exemple/telecom-job-scheduler.exe - the example will be fed by a [valid](https://tools.ietf.org/html/rfc8259) JSON called sample.json.