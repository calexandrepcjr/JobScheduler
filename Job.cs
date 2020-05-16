using System;
using System.Security.Cryptography.X509Certificates;

namespace JobScheduler
{
    public class Job
    {
        public readonly int id;
        public readonly string description;
        public readonly DateTime expiresAt;
        public readonly string estimatedTime;
    }
}
