using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Factory
{
    public interface IAnimalFactory
    {
        Animal CreateAnimal(string species, string name, int food, int inventoryNumber, int? kindness = null);

        IEnumerable<string> GetRegisteredSpecies();

        //Динамическая регистрация видов
        void RegisterSpecies(string species, Type animalType);
    }

    public class AnimalFactory : IAnimalFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, Type> _animalTypes;

        public AnimalFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _animalTypes = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
            {
                { "Monkey", typeof(Monkey) },
                { "Rabbit", typeof(Rabbit) },
                { "Tiger",  typeof(Tiger)  },
                { "Wolf",   typeof(Wolf)   }
            };
        }

        public Animal CreateAnimal(string species, string name, int food, int inventoryNumber, int? kindness = null)
        {
            if (!_animalTypes.ContainsKey(species))
            {
                throw new ArgumentException($"Species '{species}' is not registered.");
            }
            Type animalType = _animalTypes[species];

            //Для вручную добавленных видов
            if (animalType == typeof(GenericHerbivore))
            {
                if (!kindness.HasValue)
                    throw new ArgumentException("A kindness level is required for herbivores.");
                return ActivatorUtilities.CreateInstance(_serviceProvider, animalType, species, name, food, inventoryNumber, kindness.Value) as Animal;
            }
            if (animalType == typeof(GenericPredator))
            {
                return ActivatorUtilities.CreateInstance(_serviceProvider, animalType, species, name, food, inventoryNumber) as Animal;
            }

            //Для изначально добавленных видов
            if (typeof(Herbivore).IsAssignableFrom(animalType))
            {
                if (!kindness.HasValue)
                    throw new ArgumentException("A kindness level is required for herbivores.");
                return ActivatorUtilities.CreateInstance(_serviceProvider, animalType, name, food, inventoryNumber, kindness.Value) as Animal;
            }
            else
            {
                return ActivatorUtilities.CreateInstance(_serviceProvider, animalType, name, food, inventoryNumber) as Animal;
            }
        }

        public IEnumerable<string> GetRegisteredSpecies() => _animalTypes.Keys;

        public void RegisterSpecies(string species, Type animalType)
        {
            if (!typeof(Animal).IsAssignableFrom(animalType))
                throw new ArgumentException("animalType must derive from Animal.");
            _animalTypes[species] = animalType;
        }
    }
}
