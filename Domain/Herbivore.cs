namespace Domain
{
    public abstract class Herbivore : Animal
    {
        public int Kindness { get; set; }
        protected Herbivore(string name, int food, int number, int kindness)
            : base(name, food, number)
        {
            Kindness = kindness;
        }
        public bool CanContact => Kindness > 5;
        public override string ToString() => base.ToString() + $" [Herbivore, Kindness: {Kindness}, Can Contact: {CanContact}]";
    }
}
