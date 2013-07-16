namespace Sample.Core
{
    public class Calculator
    {
        private readonly int _offset;

        public Calculator(int offset)
        {
            _offset = offset;
        }

        public int Add(int x, int y)
        {
            return _offset + x + y;
        }
    }
}
