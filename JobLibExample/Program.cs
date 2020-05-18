﻿using Example;
using Example.Factories;
using JobLib;
using System;
using System.IO;
using System.Text;
using System.Text.Json;

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

            if (!HasSample(args))
            {
                Console.WriteLine("Error: Sample filename not inserted, next time execute with sample filename as argument\n");
                Console.WriteLine("Example: .\\Example.exe samplefilename.json\n");
                return;
            }

            var sampleContent = GetSampleContent(sampleFilename);

            if (string.IsNullOrEmpty(sampleContent))
            {
                Console.WriteLine("Error: Sample file not exists, next time execute with a valid sample file as argument\n");
                return;
            }

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

        static bool HasSample(string[] args) => args.Length > 0;

        static string GetSampleName(string[] args) => args.Length > 0 ? args[0] : "No sample selected";

        static string GetSampleContent(string sampleFilename) => !File.Exists(sampleFilename) ? "" : File.ReadAllText(sampleFilename, Encoding.UTF8);

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
