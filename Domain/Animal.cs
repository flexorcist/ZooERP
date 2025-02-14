namespace Domain
{
    public interface IAlive
    {
        int Food { get; set; }
    }

    public interface IInventory
    {
        int Number { get; set; }
        string Name { get; set; }
    }

    public abstract class Animal : IAlive, IInventory
    {
        public int Food { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public bool IsHealthy { get; set; }

        protected Animal(string name, int food, int number)
        {
            Name = name;
            Food = food;
            Number = number;
        }

        public override string ToString() => $"{Name} (ID: {Number}, Food: {Food}kg/day)";
    }
}
