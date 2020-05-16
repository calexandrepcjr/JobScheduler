using JobScheduler.Contracts;
using System;
using System.Collections.Generic;

namespace JobScheduler
{
    public class EstimatedTimeBR : EstimatedTime
    {
        public EstimatedTimeBR(int estimation) : base(estimation)
        {
        }

        public EstimatedTimeBR(string estimation) : base(estimation)
        {
        }

        protected override Dictionary<string, Func<int, int>> EstimationLogic
        {
            get
            {
                return new Dictionary<string, Func<int, int>>()
                {
                    {"horas", time => time * 3600 },
                    {"minutos", time => time * 60 },
                    {"segundos", time => time },
                };
            }
        }
    }
}
