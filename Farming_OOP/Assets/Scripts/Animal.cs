using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    #region Enum
    /*=======================
      ENUM & STATE HANDLING
     ========================*/
    // Food types for different animals
    public enum FeedObjectType
    {
        Hay, // Cow
        Corn, // Chicken
        Leaves, // Goat
        Generic, //default
        RottenFood // bad food
    }

    // Possible states for an animal
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

    #endregion

    #region Fields
    /*=======================
     FIELDS & PROPERTIES
    ========================*/
    private string _animalName;
    public string AnimalName
    {
        get { return _animalName; }
        protected set { _animalName = string.IsNullOrEmpty(value) ? "Unknown" : value; }
    }

    private static int _nextID = 1; 

    private int _ID;
    public int AnimalID => _ID; // read-only ID
    // Auto-assign unique ID
    protected Animal()
    {
        _ID = _nextID++;
    }

    // Health, Hunger, Happiness levels
    private int _health;
    public int Health
    {
        get { return _health; }
        protected set { _health = Mathf.Max(0, value); }
    }

    private int _hunger;
    public int Hunger
    {
        get { return _hunger; }
        protected set { _hunger = Mathf.Clamp(value, 0, 100); }
    }

    private int _happiness;
    public int Happiness
    {
        get { return _happiness; }
        protected set { _happiness = Mathf.Clamp(value, 0, 100); }
    }

    private int _feedAmount = 1;
    public int FeedAmount
    {
        get => _feedAmount;
        private set => _feedAmount = Mathf.Clamp(value, 1, 10);
    }

    private FeedObjectType _preferredFood = FeedObjectType.Generic;
    public FeedObjectType PreferredFood
    {
        get => _preferredFood;
        protected set => _preferredFood = value;
    }

    protected FeedObjectType LastFedFood { get; private set; } = FeedObjectType.Generic;

    #endregion

    #region Init

    /*=======================
      INITIALIZATION
     ========================*/
    public void InitAnimal(string name, int health, int hunger, int happiness, FeedObjectType preferredFood)
    {
        AnimalName = name;
        Health = health;
        Hunger = hunger;
        Happiness = happiness;
        PreferredFood = preferredFood;
    }
    #endregion


    #region Behaviors
    /*=======================
      BEHAVIORS / ACTIONS
     ========================*/
    // Adjust Hunger and Happiness levels
    public virtual void AdjustHunger(int amount) // decrease Hunger by specified amount
    {
        Hunger = Mathf.Max(0, Hunger - amount);
        Debug.Log($"{AnimalName} Hunger: {Hunger}");
    }

    public virtual void AdjustHappiness() //  increase Happiness by 3
    {
        Happiness = Mathf.Clamp(Happiness + 3, 0, 100);
        Debug.Log($"{AnimalName} Happiness: {Happiness}");
    }

    public void AdjustHealth(int amount) // increase or decrease Health
    {
        Health = Mathf.Max(0, Health + amount);
        Debug.Log($"{AnimalName} Health: {Health}");
    }

    // Feed the animal if the food matches its preference
    public void Feed(FeedObjectType food, int amount)
    {
        LastFedFood = food; // track last fed food
        FeedAmount = Mathf.Clamp(amount, 1, 10); // ensure amount is between 1 and 10

        if (food == PreferredFood) // preferred food - normal hunger decrease and happiness increase
        {
            AdjustHunger(FeedAmount);
            AdjustHappiness();
            Debug.Log($"{AnimalName} happily ate {FeedAmount} of {food}!");
        }
        else if (food == FeedObjectType.RottenFood) // bad food - big happiness drop
        {
            Happiness = Mathf.Clamp(Happiness - 20, 0, 100);
            Debug.Log($"{AnimalName} was fed with rotten food: {food}. Yuck! Current Happiness: {Happiness}");
        }
        else // non-preferred food - half hunger decrease and no happiness change
        {
            AdjustHunger(FeedAmount / 2);
            Debug.Log($"{AnimalName} reluctantly ate {FeedAmount} of {food}... Current Happiness: {Happiness}");
        }
    }

    // Each animal makes a unique sound
    public abstract void MakeSound();

    // Each animal produces some resource
    public abstract string Produce();

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
    #endregion


    #region Condition
    /*========================
     CONDITION CHECKS
     ========================*/
    public bool IsAnimalHappy() => Happiness >= 15;
    public bool IsAnimalHungry() => Hunger >= 15;
    public bool IsAnimalSad() => Happiness < 15;
    public bool IsAlive => Health > 0;

    #endregion
}
