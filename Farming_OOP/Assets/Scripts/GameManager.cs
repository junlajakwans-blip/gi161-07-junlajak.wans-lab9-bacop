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

            // ตั้งชื่อให้ตรงกับชนิด
            if (a is Cow) a.AnimalName = "Milky";
            else if (a is Chicken) a.AnimalName = "Nugget";
            else if (a is Goat) a.AnimalName = "Billy";
            else a.AnimalName = $"Animal_{i}"; // fallback

            a.Health = 50;
            a.Hunger = 10;
            a.Happiness = 10;

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
                    cow.Feed("Grass", 3);
                    break;

                case Chicken chicken:
                    chicken.Feed("Corn", 2);
                    break;

                case Goat goat:
                    goat.Feed("Leaves", 4);
                    break;

                default:
                    a.Feed("Food", 1);
                    break;
            }


            a.MakeSound();
            a.Sleep();
            a.GetStatus();
        }
    }
}