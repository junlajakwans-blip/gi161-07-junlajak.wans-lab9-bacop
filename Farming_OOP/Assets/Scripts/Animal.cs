using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    /*=======================
      ENUM & STATE HANDLING
     ========================*/
    public enum AnimalState
    {
        Happy,
        Neutral,
        Sad,
        Hungry,
        Dead
    }

    // Dictionary of current status
    public Dictionary<string, bool> GetStatusDictionary()
    {
        return new Dictionary<string, bool> {
            { "Alive", IsAlive },
            { "Hungry", IsAnimalHungry() },
            { "Happy", IsAnimalHappy() },
            { "Sad", IsAnimalSad() }
        };
    }

    // return current state as enum
    public AnimalState GetAnimalState()
    {
        if (!IsAlive) return AnimalState.Dead;
        if (IsAnimalHungry()) return AnimalState.Hungry;
        if (IsAnimalSad()) return AnimalState.Sad;
        if (IsAnimalHappy()) return AnimalState.Happy;
        return AnimalState.Neutral;
    }

    /*=======================
     FIELDS & PROPERTIES
    ========================*/
    private string _animalName;
    public string AnimalName
    {
        get { return _animalName; }
        set { _animalName = string.IsNullOrEmpty(value) ? "Unknown" : value; }
    }

    private int _ID;
    public int AnimalID
    {
        get { return _ID; }
        set { _ID = Mathf.Max(0, value); }
    }

    private int _health;
    public int Health
    {
        get { return _health; }
        set { _health = Mathf.Max(0, value); }
    }

    private int _hunger;
    public int Hunger
    {
        get { return _hunger; }
        set { _hunger = Mathf.Clamp(value, 0, 50); }
    }

    private int _happiness;
    public int Happiness
    {
        get { return _happiness; }
        set { _happiness = Mathf.Clamp(value, 0, 50); }
    }

    private int _feedAmount = 1;
    public int FeedAmount
    {
        get => _feedAmount;
        set => _feedAmount = Mathf.Clamp(value, 1, 10);
    }

    private string _feedObject;
    public string FeedObject
    {
        get { return _feedObject; }
        set { _feedObject = string.IsNullOrEmpty(value) ? "Food" : value; }
    }


    /*=======================
      BEHAVIORS / ACTIONS
     ========================*/
    public virtual void AdjustHunger(int amount) // decrease Hunger by specified amount
    {
        Hunger = Mathf.Max(0, Hunger - amount);
        Debug.Log($"{AnimalName} Hunger: {Hunger}");
    }

    public virtual void AdjustHappiness() //  increase Happiness by 3
    {
        Happiness = Mathf.Clamp(Happiness + 3, 0, 50);
        Debug.Log($"{AnimalName} Happiness: {Happiness}");
    }

    public void AdjustHealth(int amount) // increase or decrease Health
    {
        Health = Mathf.Max(0, Health + amount);
        Debug.Log($"{AnimalName} Health: {Health}");
    }

    public void Feed(string food, int amount) 
    {
        FeedObject = string.IsNullOrEmpty(food) ? "Food" : food;
        FeedAmount = Mathf.Clamp(amount, 1, 10);
        
        AdjustHunger(FeedAmount);
        AdjustHappiness();
        Debug.Log($"{AnimalName} was fed {FeedAmount} of {FeedObject}.");
    }


    public abstract void MakeSound();
    
    public virtual void Sleep() // sleeping increases both hunger and happiness slightly
    {
        Hunger = Mathf.Clamp(Hunger + 5, 0, 50);
        Happiness = Mathf.Clamp(Happiness + 5, 0, 50);
    }

    public virtual void GetStatus() // Show main status
    {
        switch (GetAnimalState())
        {
            case AnimalState.Happy:
                Debug.Log($"{AnimalName} is happy!");
                break;
            case AnimalState.Hungry:
                Debug.Log($"{AnimalName} is hungry...");
                break;
            case AnimalState.Sad:
                Debug.Log($"{AnimalName} looks sad.");
                break;
            case AnimalState.Dead:
                Debug.Log($"{AnimalName} has died...");
                break;
            default:
                Debug.Log($"{AnimalName} -> Hunger: {Hunger} | Happiness: {Happiness}");
                break;
        }
    }

    /*========================
     CONDITION CHECKS
     ========================*/
    public bool IsAnimalHappy() => Happiness >= 15;
    public bool IsAnimalHungry() => Hunger >= 15;
    public bool IsAnimalSad() => Happiness < 15;
    public bool IsAlive => Health > 0;
}
