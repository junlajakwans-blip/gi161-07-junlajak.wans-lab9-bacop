using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Animal> animalPrefabs;
    public List<Animal> animals = new List<Animal>();
    public int AnimalsCount { get; private set; }

    public void AddAnimal(Animal a)
    {
        animals.Add(a);
        AnimalsCount++;
    }

    public void RemoveAnimal(Animal a)
    {
        if (animals.Remove(a))
        {
            AnimalsCount--;
        }
    }

    void Start()
    {
        Vector3 spawnPos = Vector3.zero;
        float offset = 2.0f;

        for (int i = 0; i < animalPrefabs.Count; i++)
        {
            Animal a = Instantiate(animalPrefabs[i], spawnPos + new Vector3(i * offset, 0, 0), Quaternion.identity);

            // Init แต่ละสัตว์ด้วยค่าพื้นฐาน + PreferredFood
            if (a is Cow)
                a.InitAnimal("Milky", 50, 10, 10, Animal.FeedObjectType.Hay);
            else if (a is Chicken)
                a.InitAnimal("Nugget", 40, 8, 12, Animal.FeedObjectType.Corn);
            else if (a is Goat)
                a.InitAnimal("Billy", 45, 9, 15, Animal.FeedObjectType.Leaves);
            else
                a.InitAnimal($"Animal_{i}", 50, 10, 10, Animal.FeedObjectType.Generic);

            AddAnimal(a);
        }

        // ===== Show Farm =====
        string animalsNames = string.Join(", ", animals.Select(a => a.AnimalName));
        Debug.Log($"{animalsNames} are living in the farm.");
        Debug.Log($"There are {AnimalsCount} animals in the farm.");

        // ===== Do actions =====
        foreach (var a in animals)
        {
            switch (a)
            {
                case Cow cow:
                    cow.Feed(Animal.FeedObjectType.Hay, 3);
                    break;

                case Chicken chicken:
                    chicken.Feed(Animal.FeedObjectType.Corn, 2);
                    break;

                case Goat goat:
                    goat.Feed(Animal.FeedObjectType.Leaves, 4);
                    break;

                default:
                    a.Feed(Animal.FeedObjectType.Generic, 1);
                    break;
            }

            a.MakeSound();
            a.Produce();
            a.Sleep();
            a.GetStatus();
        }
    }
}
