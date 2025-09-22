using UnityEngine;

class Cow : Animal
{
    private int _milkProduced;
    public int MilkProduced
    {
        get { return _milkProduced; }
        set
        {
            if (value > 999) _milkProduced = 999;
            else if (value < 0) _milkProduced = 0;
            else _milkProduced = value;
        }
    }

    public Cow(string name = "Bessie", int health = 50, int hunger = 10, int happiness = 10)
    {
        AnimalName = string.IsNullOrEmpty(name) ? "Bessie" : name;
        Health = health;
        Hunger = hunger;
        Happiness = happiness;
        MilkProduced = 0;
    }

    public void ProduceMilk()
    {
        MilkProduced++;
        Debug.Log($"{AnimalName} produced milk! Total: {MilkProduced}");
    }

        public override void AdjustHunger(int amount) // species hunger decrease
    {
        Hunger = Mathf.Max(0, Hunger - amount);
        Debug.Log($"{AnimalName} yap yap Grass. Hunger: {Hunger}");
    }

    public override void AdjustHappiness() // species happiness increase
    {
        Happiness = Mathf.Clamp(Happiness + 3, 0, 50);
        Debug.Log($"{AnimalName} Happy: {Happiness}");
    }


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

    public override void Sleep()
    {
        Hunger = Mathf.Clamp(Hunger + 5, 0, 50);
        Happiness = Mathf.Clamp(Happiness + 5, 0, 50);
        Debug.Log($"{AnimalName} is resting in the barn...");
    }

    //  GetStatus
    public override void GetStatus()
    {
        Debug.Log($"{AnimalName} -> Hunger: {Hunger} | Happiness: {Happiness} | Milk: {MilkProduced}");
    }

    // Check if Cow is happy
    public bool IsCowHappy()
    {
        return Happiness >= 15;
    }
}
