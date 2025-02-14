using Microsoft.Extensions.DependencyInjection;
using Domain;
using Inventory;
using Services;
using Factory;

namespace ERPzoo
{
    public class Program
    {
        //Добавление нового животного
        private static void AddNewAnimal(IZoo zoo, IAnimalFactory animalFactory)
        {
            Console.WriteLine("\nAvailable species: " + string.Join(", ", animalFactory.GetRegisteredSpecies()));
            Console.Write("Enter species: ");
            string species = Console.ReadLine();

            Console.Write("Enter animal name: ");
            string name = Console.ReadLine();

            Console.Write("Enter food consumption (kg): ");
            if (!int.TryParse(Console.ReadLine(), out int food))
            {
                Console.WriteLine("Invalid input for food consumption.");
                return;
            }

            Console.Write("Enter inventory number: ");
            if (!int.TryParse(Console.ReadLine(), out int inventoryNumber))
            {
                Console.WriteLine("Invalid input for inventory number.");
                return;
            }

            int? kindness = null;
            if (species.Equals("Monkey", StringComparison.OrdinalIgnoreCase) ||
                species.Equals("Rabbit", StringComparison.OrdinalIgnoreCase) ||
                //Используем generic для регистрации нового вида
                animalFactory.GetRegisteredSpecies().Contains(species, StringComparer.OrdinalIgnoreCase) &&
                (IsRegisteredAsHerbivore(animalFactory, species)))
            {
                Console.Write("Enter kindness level (0-10): ");
                if (!int.TryParse(Console.ReadLine(), out int k))
                {
                    Console.WriteLine("Invalid input for kindness.");
                    return;
                }
                kindness = k;
            }

            Animal animal;
            try
            {
                animal = animalFactory.CreateAnimal(species, name, food, inventoryNumber, kindness);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating animal: {ex.Message}");
                return;
            }

            Console.Write("Is the animal healthy? (y/n): ");
            string healthyInput = Console.ReadLine();
            animal.IsHealthy = healthyInput.Trim().ToLower() == "y";

            zoo.AddAnimal(animal);
        }

        //Метод для выявления, является ли новый вид хищным.
        private static bool IsRegisteredAsHerbivore(IAnimalFactory factory, string species)
        {
            try
            {
                //Пытаемся создать тестовое животное
                Animal dummy = factory.CreateAnimal(species, "dummy", 0, 0, 0);
                return dummy is Herbivore;
            }
            catch
            {
                return true;
            }
        }

        //Отображение всех животных в зоопарке
        private static void DisplayCurrentInhabitants(IZoo zoo)
        {
            Console.WriteLine("\n--- Current Inhabitants ---");
            foreach (var animal in zoo.Animals)
            {
                Console.WriteLine(animal);
            }
            Console.WriteLine($"Total food consumption: {zoo.TotalFoodConsumption()} kg/day");
        }

        //Отображение контактных животных
        private static void DisplayContactable(IZoo zoo)
        {
            Console.WriteLine("\n--- Contactable Animals ---");
            foreach (var herb in zoo.ContactableAnimals())
            {
                Console.WriteLine(herb);
            }
        }

        private static void DisplayInventory(IZoo zoo)
        {
            Console.WriteLine("\n--- Inventory Items ---");
            foreach (var thing in zoo.Things)
            {
                Console.WriteLine(thing);
            }
        }

        //Регистрация нового вида для зоопарка
        private static void RegisterNewSpecies(IAnimalFactory animalFactory)
        {
            Console.Write("Enter new species name: ");
            string species = Console.ReadLine();

            Console.Write("Is this species a herbivore? (y/n): ");
            string isHerbInput = Console.ReadLine();
            bool isHerbivore = isHerbInput.Trim().ToLower() == "y";

            if (isHerbivore)
            {
                animalFactory.RegisterSpecies(species, typeof(GenericHerbivore));
                Console.WriteLine($"Species '{species}' registered as a herbivore.");
            }
            else
            {
                animalFactory.RegisterSpecies(species, typeof(GenericPredator));
                Console.WriteLine($"Species '{species}' registered as a predator.");
            }
        }

        //Настройка DI контейнера
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IVeterinaryClinic, VeterinaryClinic>();
            services.AddSingleton<IZoo, Zoo>();
            services.AddSingleton<IAnimalFactory, AnimalFactory>();
        }

        public static void Main()
        {
            //Настраиваем DI контейнер
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var zoo = serviceProvider.GetService<IZoo>();
            var animalFactory = serviceProvider.GetService<IAnimalFactory>();

            //Добавляем инвентарь
            zoo.AddThing(new Table("Feeding Table", 201));
            zoo.AddThing(new Computer("Zoo Computer", 202));

            //Весь интерфейс на английском во избежание ошибок кодировки
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n=== Welcome to Zoo ERP ===");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("[1] - Add a new animal to the zoo");
                Console.WriteLine("[2] - Display current inhabitants");
                Console.WriteLine("[3] - Display contactable animals");
                Console.WriteLine("[4] - Check inventory");
                Console.WriteLine("[5] - Exit");
                Console.WriteLine("[6] - Register new species");
                Console.Write("Your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewAnimal(zoo, animalFactory);
                        break;
                    case "2":
                        DisplayCurrentInhabitants(zoo);
                        break;
                    case "3":
                        DisplayContactable(zoo);
                        break;
                    case "4":
                        DisplayInventory(zoo);
                        break;
                    case "5":
                        exit = true;
                        Console.WriteLine("Goodbye!");
                        break;
                    case "6":
                        RegisterNewSpecies(animalFactory);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        
    }
}
