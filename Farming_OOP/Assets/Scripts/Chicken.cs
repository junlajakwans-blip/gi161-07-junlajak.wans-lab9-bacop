using UnityEngine;

class Chicken : Animal
{
    #region Production
    private int _eggCount;
    public int EggCount
    {
        get => _eggCount;
        private set => _eggCount = Mathf.Clamp(value, 0, 999);
    }

    public override string Produce() //chicken produce egg
    {
        EggCount++;
        Debug.Log($"{AnimalName} laid an egg! Total eggs: {EggCount} eggs");
        return "Egg";
    }
    #endregion

    #region Fields
    public override void AdjustHunger(int amount) // species hunger decrease
    {
        Hunger = Mathf.Max(0, Hunger - amount);
        Debug.Log($"{AnimalName} pecks some {LastFedFood}. Hunger: {Hunger}");
    }

    public override void AdjustHappiness() // species happiness increase
    {
        Happiness = Mathf.Clamp(Happiness + 3, 0, 100);
        Debug.Log($"{AnimalName} flaps wings happily! Happiness: {Happiness}");
    }
    #endregion

    #region Behaviors
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
        Hunger = Mathf.Clamp(Hunger + 3, 0, 100);
        Happiness = Mathf.Clamp(Happiness + 2, 0, 100);
        Debug.Log($"{AnimalName} is roosting...");
    }
    #endregion


    #region Status
    // chicken status
    public override void GetStatus()
    {
        Debug.Log($"{AnimalName} -> Hunger: {Hunger} | Happiness: {Happiness} | Eggs: {EggCount}");
    }

    public bool IsChickenHappy()
    {
        return IsAnimalHappy();
    }
    #endregion
}
