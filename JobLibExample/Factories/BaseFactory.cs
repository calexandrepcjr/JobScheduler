using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Factories
{
    public abstract class BaseFactory<T>
    {
        public abstract T Build();
    }
}
