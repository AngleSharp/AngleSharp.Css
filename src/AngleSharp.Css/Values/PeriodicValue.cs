namespace AngleSharp.Css.Values
{
    using System;

    public sealed class PeriodicValue<T>
    {
        private readonly T[] _values;

        public PeriodicValue(T[] values)
        {
            _values = values;
        }

        public T Top
        {
            get { return _values.Length > 0 ? _values[0] : default(T); }
        }

        public T Right
        {
            get { return _values.Length > 1 ? _values[1] : Top; }
        }

        public T Bottom
        {
            get { return _values.Length > 2 ? _values[2] : Top; }
        }

        public T Left
        {
            get { return _values.Length > 3 ? _values[3] : Right; }
        }

        public override String ToString()
        {
            return String.Join(" ", _values);
        }
    }
}
