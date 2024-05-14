using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESAmerica.Application.Shared
{
    public abstract class ValidationCommand<T>
    {
        public abstract Task<List<string>> Validation(T command);
    }
}
