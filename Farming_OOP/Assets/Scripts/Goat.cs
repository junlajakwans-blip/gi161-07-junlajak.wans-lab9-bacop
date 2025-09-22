using UnityEngine;

class Goat : Animal
{
    private int _woolProduced;
    public int WoolProduced
    {
        get { return _woolProduced; }
        set
        {
            if (value > 999) _woolProduced = 999;
            else if (value < 0) _woolProduced = 0;
            else _woolProduced = value;
        }
    }

    public Goat(string name = "Billy", int health = 45, int hunger = 10, int happiness = 10)
    {
        AnimalName = string.IsNullOrEmpty(name) ? "Billy" : name;
        Health = health;
        Hunger = hunger;
        Happiness = happiness;
        WoolProduced = 0;
    }

    /*public void ProduceWool()
    {
        WoolProduced++;
        Debug.Log($"{AnimalName} produced wool! Total wool: {WoolProduced}");
    }*/

    public override void AdjustHunger(int amount)
    {
        Hunger = Mathf.Max(0, Hunger - amount);
        Debug.Log($"{AnimalName} munches on grass. Hunger: {Hunger}");
    }

    public override void AdjustHappiness()
    {
        Happiness = Mathf.Clamp(Happiness + 3, 0, 50);
        Debug.Log($"{AnimalName} jumps on rocks happily! Happiness: {Happiness}");
    }

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
        Hunger = Mathf.Clamp(Hunger + 4, 0, 50);
        Happiness = Mathf.Clamp(Happiness + 3, 0, 50);
        Debug.Log($"{AnimalName} curls up near the barn and sleeps...");
    }

    public override void GetStatus() // goat status
    {
        Debug.Log($"{AnimalName} -> Hunger: {Hunger} | Happiness: {Happiness} | Wool: {WoolProduced}");
    }

    public bool IsGoatHappy()
    {
        return Happiness >= 15;
    }
}
