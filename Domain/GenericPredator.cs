namespace Domain
{
    public class GenericPredator : Predator
    {
        public string Species { get; }
        public GenericPredator(string species, string name, int food, int number)
            : base(name, food, number)
        {
            Species = species;
        }
        public override string ToString() => $"{Species} - {base.ToString()}";
    }
}
