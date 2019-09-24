namespace SquaredString.v2
{
    public class SquaredStringService
    {
        private readonly SquaredCharCollection _collection;

        public SquaredStringService(string input)
        {
            _collection = new SquaredCharCollection(input);
        }

        public string Symmetry()
        {
            var symmetryStrategy = new SymmetryStrategy();
            var result = symmetryStrategy.Transform(_collection);
            return result.Output();
        }

        public string Rot90Clock()
        {
            var rot90ClockStrategy = new Rot90ClockStrategy();
            var result = rot90ClockStrategy.Transform(_collection);
            return result.Output();
        }

        public string SelfieAndSymmetry()
        {
            var symmetryStrategy = new SymmetryStrategy();
            var selfieCollection = _collection;
            var symmetryCollection = symmetryStrategy.Transform(_collection);

            return selfieCollection.LineJoin(symmetryCollection);
        }
    }
}