##### Весь интерфейс взаимодействия с пользователем написан на английском во избежание ошибок в кодировке #####

Как запустить?
1. Загрузить проект
2. Найти файл Program.cs
3. Нажать Run!

###############################################################################################################

Идеи:
1. Организация полного взаимодействия через консоль
2. Использование фабрик для добавления новых животных с помощью DI контейнера
3. Использование generic классов для регистрации новых видов для последующего добавления их в зоопарк
4. Структуризация всего проекта через практические типы классов
5. С 10% шансом клиника может найти заболевание у животного и не принять его в зоопарк

###############################################################################################################

Для SOLID:

S:
Animal и каждый его подкласс ответственны только за хранящуюся в них информацию (поля c ID, потреблением еды...) и поведение (Например, вывод информации)

O:
С помощью интерфейсов (IAnimalFactory, IZoo, IVeterinaryClinic) можно вручную добавить классы в проект. Также с помощью generic классов (GenericHerbivore и GenericPredator) можно расширять внутренний словарь классов во время runtime.

L:
Animal заменяема на любые дочерние классы. Например, код в классе Zoo использует только Animal, но работает со всеми дочерними. 

I:
Интерфейсы IAlive и IInventory используют только необходимые для них поля.

D:
Такие классы, как Zoo и AnimalFactory, используют соответствующие им интерфейсы, а не точные реализации. 

###############################################################################################################

Для ООП:

1. Инкапсуляция
Zoo, AnimalFactory и VeterinaryClinic инкапсулирует свои поля, методы и общее поведение

2. Наследование
Для примера, Herbivore наследует от Animal, Monkey наследует от Herbivore

3. Полиморфизм
Для примера, Zoo "относится" ко всем животным как к Animal, но вызывает их перегруженные методы для правильного отображения информации.

4. Абстракция
Animal, Herbo, и Predator используют абстрактные поля, общие для дочерних классов