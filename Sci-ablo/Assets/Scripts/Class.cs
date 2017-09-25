public enum Classes
{
    Unarmed,
    Sword,
    Gun
}

public class Class : DataStore<Classes, Class>
{
    public List<Ability> abilities
    {
        get; set;
    }

    public Stats stats
    {
        get; set;
    }

    public Class()
    {
        stats = new Stats();
        abilites = new List<Abilities>();
    }
}
