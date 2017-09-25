public enum Abilities
{

}
public class Ability : DataStore<Abilities, Ability> {

	public string name
    {
        get; set;
    }

    public string description
    {
        get; set;
    }

    public int cost
    {
        get; set;
    }

    public int damage
    {
        get; set;
    }

    public int range
    {
        get; set;
    }
}
