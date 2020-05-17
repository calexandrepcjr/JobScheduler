using JobLib.Contracts;
using System;
using System.Collections.Generic;

namespace JobLib
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
                    {"hora", time => time * 3600 },
                    {"horas", time => time * 3600 },
                    {"minuto", time => time * 60 },
                    {"minutos", time => time * 60 },
                    {"segundo", time => time },
                    {"segundos", time => time }
                };
            }
        }
    }
}
