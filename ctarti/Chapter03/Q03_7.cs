using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ctarti.DataStructures;

namespace Chapter03
{
    /*	7. An animal shelter hold only dogs and cats, and operates on a strictly 
     * "first in, first out" basis. People must adopt either the "oldest" (based 
     * on arrival time) of all animals at the shelter, or they can select whether 
     * they would prefer a dog or a cat (and will receive the oldest animal of that 
     * type). They cannot select which specific animal they would like. Create the
     * data structures to maintain this system and implement such as enqueue, 
     * dequeueAny, dequeueDog and dequeueCat. You may use the build-in Linked 
     * List data structure. */

    public abstract class Animal : ctarti.DataStructures.LinkedListNode
    {
        public enum AnimalType { Cat, Dog }
        public AnimalType Type;
    }

    public class Dog : Animal
    {   
        public Dog()
        {
            base.Type = AnimalType.Dog;
        }
    }

    public class Cat : Animal
    {
        public Cat()
        {
            base.Type = AnimalType.Cat;
        }
    }

    public class AnimalShelter
    {
        Queue<Dog> Dogs = new Queue<Dog>();
        Queue<Cat> Cats = new Queue<Cat>();
        int Order;

        public void Enqueue(Animal animal)
        {
            if (animal.GetType() == typeof(Dog))
            {
                animal.Data = Order;
                Dogs.Enqueue((Dog)animal);
                Order++;
            }
            else if (animal.GetType() == typeof(Cat))
            {
                animal.Data = Order;
                Cats.Enqueue((Cat)animal);
                Order++;
            }
            else
                throw new Exception("Animal Not Supported");
        }
        public Animal DequeueAny()
        {
            if (Dogs.Peek().Data < Cats.Peek().Data)
                return DequeueDog();
            else
                return DequeueCat();
        }
        public Dog DequeueDog() { return Dogs.Dequeue(); }
        public Cat DequeueCat() { return Cats.Dequeue(); }

    }



    public class Q03_7 : IQuestion
    {
        public void Run()
        {
            AnimalShelter shelter = new AnimalShelter();
            shelter.Enqueue(new Cat());
            shelter.Enqueue(new Dog());
            shelter.Enqueue(new Cat());
            shelter.Enqueue(new Dog());

            Console.WriteLine(shelter.DequeueDog().Type.ToString());
            Console.WriteLine(shelter.DequeueAny().Type.ToString());
            Console.WriteLine(shelter.DequeueDog().Type.ToString());
            Console.WriteLine(shelter.DequeueCat().Type.ToString());


        }
    }
}
