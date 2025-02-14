namespace Domain
{
    public class GenericHerbivore : Herbivore
    {
        public string Species { get; }
        public GenericHerbivore(string species, string name, int food, int number, int kindness)
            : base(name, food, number, kindness)
        {
            Species = species;
        }
        public override string ToString() => $"{Species} - {base.ToString()}";
    }
}
