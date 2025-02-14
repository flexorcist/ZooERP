namespace Domain
{
    public abstract class Predator : Animal
    {
        protected Predator(string name, int food, int number)
            : base(name, food, number)
        { }
        public override string ToString() => base.ToString() + " [Predator]";
    }
}
