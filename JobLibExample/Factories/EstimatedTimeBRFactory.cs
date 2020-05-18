using JobLib;
using JobLib.Contracts;

namespace Example.Factories
{
    public class EstimatedTimeBRFactory : BaseFactory<EstimatedTime>
    {
        private readonly string Estimation;
        public EstimatedTimeBRFactory(string estimation)
        {
            Estimation = estimation;
        }

        public override EstimatedTime Build()
        {
            return new EstimatedTimeBR(Estimation);
        }
    }
}
