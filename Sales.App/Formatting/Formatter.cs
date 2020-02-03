using System;

namespace Sales.App.Formatting
{
    public abstract class Formatter<T>
    {
        public abstract string Format(T entity, IFormatProvider formatProvider = null);
    }
}
