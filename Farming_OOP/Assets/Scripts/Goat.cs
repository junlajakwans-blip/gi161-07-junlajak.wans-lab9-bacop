using UnityEngine;

class Goat : Animal
{
    #region Production
    private int _woolProduced;
    public int WoolProduced
    {
        get => _woolProduced;
        private set => _woolProduced = Mathf.Clamp(value, 0, 999);
    }

    public override string Produce() // goat produce wool
    {
        WoolProduced++;
        Debug.Log($"{AnimalName} has been sheared! Total wool: {WoolProduced} units");
        return "Wool";
    }
    #endregion

    #region Fields
    public override void AdjustHunger(int amount)
    {
        Hunger = Mathf.Max(0, Hunger - amount);
        Debug.Log($"{AnimalName} munches on {LastFedFood}. Hunger: {Hunger}");
    }

    public override void AdjustHappiness()
    {
        Happiness = Mathf.Clamp(Happiness + 3, 0, 100);
        Debug.Log($"{AnimalName} jumps on rocks happily! Happiness: {Happiness}");
    }
    #endregion

    #region Behaviors
    public override void MakeSound() // goat sound
    {
        if (Happiness >= 40)
            Debug.Log($"{AnimalName} Baaaaa~!");
        else if (Happiness >= 15)
            Debug.Log($"{AnimalName} Baa.");
        else
            Debug.Log($"{AnimalName} Silently chews grass...");
    }

    public override void Sleep() // goat sleep
    {
        Hunger = Mathf.Clamp(Hunger + 4, 0, 100);
        Happiness = Mathf.Clamp(Happiness + 3, 0, 100);
        Debug.Log($"{AnimalName} curls up near the barn and sleeps...");
    }
    #endregion

    #region Status
    public override void GetStatus() // goat status
    {
        Debug.Log($"{AnimalName} -> Hunger: {Hunger} | Happiness: {Happiness} | Wool: {WoolProduced}");
    }

    public bool IsGoatHappy()
    {
        return IsAnimalHappy();
    }
    #endregion
}
