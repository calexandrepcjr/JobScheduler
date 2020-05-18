using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Example;
using Example.Factories;
using JobLib;
using Job = JobLib.Job;

namespace JobLibExample
{
    static class Program
    {
        static void Main(string[] args)
        {
            JobScheduler(args);
            Console.ReadKey();
        }

        static void JobScheduler(string[] args)
        {
            Console.WriteLine("=====================================================");
            Console.WriteLine("=================== JOB SCHEDULER ===================");
            Console.WriteLine("=====================================================\n");

            var sampleFilename = GetSampleName(args);

            Console.WriteLine("Sample Selected: " + sampleFilename + "\n");

            var sampleContent = GetSampleContent(sampleFilename);

            Console.WriteLine("============= SAMPLE CONTENT =================\n");
            Console.WriteLine(sampleContent);

            var sample = GetSample(sampleContent);
            var scheduleSample = ScheduleSample(sample);

            Console.WriteLine("\n============= SCHEDULE CONTENT =============\n");
            var serializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            Console.WriteLine(JsonSerializer.Serialize(scheduleSample, serializerOptions));

            Console.WriteLine("\nPress any key to close");
        }

        static string GetSampleName(string[] args) => args.Length > 0 ? args[0] : "No sample selected";

        static string GetSampleContent(string sampleFilename) => File.ReadAllText(sampleFilename, Encoding.UTF8);

        static Sample GetSample(string sampleContent) => JsonSerializer.Deserialize<Sample>(sampleContent);

        static int[][] ScheduleSample(Sample sample)
        {
            var executionWindow = new ExecutionWindowFactory(sample).Build();
            var maxEstimatedTime = new EstimatedTimeBRFactory("8 horas").Build();
            var jobs = new JobsFactory(sample).Build();

            var jobScheduler = new JobScheduler(executionWindow, maxEstimatedTime);

            jobs.ForEach(job => jobScheduler.Schedule(job));

            return jobScheduler.ToArray();
        }
    }
}
