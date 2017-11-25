using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XMLManager : MonoBehaviour {

    public static XMLManager ins;

    void Awake()
    {
        ins = this;
    }

    public ItemDatabase itemDB;

    public void SaveItems()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/Resources/items.xml", FileMode.Create);
        serializer.Serialize(stream, itemDB);
        stream.Close();
    }

    public void LoadItems()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/Resources/items.xml", FileMode.Open);
        itemDB = serializer.Deserialize(stream) as ItemDatabase;
        stream.Close();
    }
}

[System.Serializable]
public class WeaponEntry
{
    public string name;
    public weaponType type;

    public int physDamage;
    public int fireDamage;
    public int coldDamage;
    public int speed;
    public int critChance;
    public int lifeLeech;

}

[System.Serializable]
public class ArmorEntry
{
    public string name;
    public armorType type;

    public int armor;
    public int health;
    public int allRes;
    public int lifeRegen;
    public int moveSpeed;
    public int cooldown;

}


[System.Serializable]
public class ItemDatabase
{
    [XmlArray("Weapons")]
    public List<WeaponEntry> list1 = new List<WeaponEntry>();

    [XmlArray("Armor")]
    public List<ArmorEntry> list2 = new List<ArmorEntry>();
}

public enum weaponType
{
    Hammer = 0,
    Gun = 1,
}

public enum armorType
{
    Chest = 0,
    Helmet = 1,
    Boots = 2,
    Gloves = 3,
    Leggings = 4
}