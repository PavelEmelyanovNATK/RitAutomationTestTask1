using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RitAutomationTestTask1.Domain.Utils
{
    public static class ScopeFunctions
    {
        public static R Let<T, R>(this T self, Func<T, R> mapper) => mapper(self);

        public static void Apply<T>(this T self, Action<T> action) => action(self);
    }
}
