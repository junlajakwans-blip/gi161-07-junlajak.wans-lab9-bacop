using UnityEngine;

class Chicken : Animal
{
    private int _eggCount;
    public int EggCount
    {
        get { return _eggCount; }
        set
        {
            if (value > 999) _eggCount = 999;
            else if (value < 0) _eggCount = 0;
            else _eggCount = value;
        }
    }

    //  Constructor
    public Chicken(string name = "Nugget", int health = 50, int hunger = 10, int happiness = 10)
    {
        AnimalName = string.IsNullOrEmpty(name) ? "Nugget" : name;
        Health = health;
        Hunger = hunger;
        Happiness = happiness;
        EggCount = 0;
    }

    /*blic void LayEgg() //
    {
        EggCount++;
        Debug.Log($"{AnimalName} laid an egg! Total eggs: {EggCount}");
    }*/

    public override void AdjustHunger(int amount) // species hunger decrease
    {
        Hunger = Mathf.Max(0, Hunger - amount);
        Debug.Log($"{AnimalName} pecks some corn. Hunger: {Hunger}");
    }

    public override void AdjustHappiness() // species happiness increase
    {
        Happiness = Mathf.Clamp(Happiness + 3, 0, 50);
        Debug.Log($"{AnimalName} flaps wings happily! Happiness: {Happiness}");
    }

    public override void MakeSound() //chicken sound
    {
        if (Happiness >= 40)
            Debug.Log($"{AnimalName} clucks loudly: Cluck! Cluck! Cluck!");
        else if (Happiness >= 15)
            Debug.Log($"{AnimalName} : Cluck!");
        else
            Debug.Log($"{AnimalName} is too sad to cluck...");
    }

    // chicken sleep where
    public override void Sleep()
    {
        Hunger = Mathf.Clamp(Hunger + 3, 0, 50);
        Happiness = Mathf.Clamp(Happiness + 2, 0, 50);
        Debug.Log($"{AnimalName} is roosting...");
    }

    // chicken status
    public override void GetStatus()
    {
        Debug.Log($"{AnimalName} -> Hunger: {Hunger} | Happiness: {Happiness} | Eggs: {EggCount}");
    }

    public bool IsChickenHappy()
    {
        return Happiness >= 15;
    }
}
