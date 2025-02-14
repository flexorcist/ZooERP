using Domain;
using Inventory;

namespace Services
{
    public interface IZoo
    {
        void AddAnimal(Animal animal);
        void AddThing(Thing thing);
        IEnumerable<Animal> Animals { get; }
        IEnumerable<Thing> Things { get; }
        int TotalFoodConsumption();
        IEnumerable<Herbivore> ContactableAnimals();
    }

    public class Zoo : IZoo
    {
        private readonly List<Animal> _animals = new List<Animal>();
        private readonly List<Thing> _things = new List<Thing>();
        private readonly IVeterinaryClinic _clinic;

        public Zoo(IVeterinaryClinic clinic) => _clinic = clinic;

        public IEnumerable<Animal> Animals => _animals;
        public IEnumerable<Thing> Things => _things;

        public void AddAnimal(Animal animal)
        {
            if (_clinic.Examine(animal))
            {
                _animals.Add(animal);
                Console.WriteLine($"{animal.Name} has been accepted into the zoo.");
            }
            else
            {
                Console.WriteLine($"{animal.Name} has been rejected by the clinic.");
            }
        }

        public void AddThing(Thing thing) => _things.Add(thing);

        public int TotalFoodConsumption() => _animals.Sum(a => a.Food);

        public IEnumerable<Herbivore> ContactableAnimals() => _animals.OfType<Herbivore>().Where(h => h.CanContact);
    }
}
