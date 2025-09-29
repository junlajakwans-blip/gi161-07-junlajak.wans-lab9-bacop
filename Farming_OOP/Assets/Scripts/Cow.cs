using UnityEngine;

class Cow : Animal
{
    #region  Production
    //produce milk
    private int _milkProduced;
    public int MilkProduced
    {
        get => _milkProduced;
        private set => _milkProduced = Mathf.Clamp(value, 0, 999);
    }

    public override string Produce()
    {
        MilkProduced++;
        Debug.Log($"{AnimalName} produced milk! Total: {MilkProduced} Unit");
        return "Milk";
    }
    #endregion

    #region Fields
    // Adjust Hunger and Happiness
    public override void AdjustHunger(int amount) // species hunger decrease
    {
        Hunger = Mathf.Max(0, Hunger - amount);
        Debug.Log($"{AnimalName} yap yap {LastFedFood} : {Hunger}");
    }

    public override void AdjustHappiness() // species happiness increase
    {
        Happiness = Mathf.Clamp(Happiness + 3, 0, 100);
        Debug.Log($"{AnimalName} Happy: {Happiness}");
    }
    #endregion

    #region Behaviors
    // MakeSound 
    public override void MakeSound()
    {
        if (Happiness >= 40)
            Debug.Log($"{AnimalName}  Moo! Moo! Moo!");
        else if (Happiness >= 15)
            Debug.Log($"{AnimalName} Moo!");
        else
            Debug.Log($"{AnimalName} is too sad to moo...");
    }

    // Sleep
    public override void Sleep()
    {
        Hunger = Mathf.Clamp(Hunger + 5, 0, 100);
        Happiness = Mathf.Clamp(Happiness + 5, 0, 100);
        Debug.Log($"{AnimalName} is resting in the barn...");
    }
    #endregion

    #region Status

    //  GetStatus
    public override void GetStatus()
    {
        Debug.Log($"{AnimalName} -> Hunger: {Hunger} | Happiness: {Happiness} | Milk: {MilkProduced}");
    }

    // Check if Cow is happy
    public bool IsCowHappy()
    {
        return IsAnimalHappy();
    }
    #endregion
}
