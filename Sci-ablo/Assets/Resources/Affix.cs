using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Affix : MonoBehaviour
{

    //brainstorm note:
    //need to create weapon/armor specific base items
    //(Gravity Hammer, Super Sledge, etc...)
    //need to implement base stats for said items then apply the affix rolls in another method

    string[] WeaponPrefix = new string[]
    {
        "Weak ", "Average ", "Strong ", //+phys damage
        "Fiery ", "Burning ", "Blazing ", //+fire damage
        "Frosted ", "Freezing ", "Frigid " //+cold damage
    };

    int[] WeaponPrefixStats = new int[]
    {
        5, 10, 10, 15, 20, 30,
        5, 10, 10, 15, 20, 30,
        5, 10, 10, 15, 20, 30,
    };

    string[] WeaponSuffix = new string[]
    {
        " of Skill", " of Mastery", " of Fame", //+attack speed
        " of Piercing", " of Puncturing", " of Incision", //+crit chance
        " of Nourishment", " of Restoration", " of the Leech" //+life leech
    };
    int[] WeaponSuffixStats = new int[]
    {
        5, 10, 20, 30, 40, 50,
        5, 7, 7, 15, 15, 25,
        10, 20, 20, 30, 30, 50
    };

    string[] ArmorPrefix = new string[]
    {
        "Brittle ", "Reinforced ", "Plated ", //+armor rating
        "Healthy ", "Sturdy ", "Vigorous ", //+health
        "Armadillo's ", "Rhino's ", "Mammoth's " //+all res
    };

    int[] ArmorPrefixStats = new int[]
    {
        25, 50, 50, 100, 100, 250,
        25, 50, 50, 100, 100, 250,
        5, 10, 10, 15, 15, 25
    };

    string[] ArmorSuffix = new string[]
    {
        " of the Lizard", " of the Hydra", " of the Phoenix", //+life regen
        " of Quickness", " of the Stallion", " of the Cheetah", //+movement speed
        " of Joy", " of Bliss", " of Euphoria" //-skill cooldown
    };

    int[] ArmorSuffixStats = new int[]
    {
        1, 2, 2, 4, 4, 6,
        10, 20, 20, 30, 30, 50,
        1, 2, 2, 3, 3, 4
    };

    public void ConstructItem(string name, string type, int prefix1, int prefix2, int prefix3, int suffix1, int suffix2, int suffix3)
    {
        if (type == "Hammer" || type == "Gun")
        {
            WeaponEntry entry = new WeaponEntry();
            entry.name = name;
            entry.type = (weaponType)System.Enum.Parse(typeof(weaponType), type);
            entry.physDamage = prefix1;
            entry.fireDamage = prefix2;
            entry.coldDamage = prefix3;
            entry.speed = suffix1;
            entry.critChance = suffix2;
            entry.lifeLeech = suffix3;

            XMLManager.ins.itemDB.list1.Add(entry);
        }

        else
        {
            ArmorEntry entry = new ArmorEntry();
            entry.name = name;
            entry.type = (armorType)System.Enum.Parse(typeof(armorType), type);
            entry.armor = prefix1;
            entry.health = prefix2;
            entry.allRes = prefix3;
            entry.lifeRegen = suffix1;
            entry.moveSpeed = suffix2;
            entry.cooldown = suffix3;

            XMLManager.ins.itemDB.list2.Add(entry);
        }

    }

    public void ConstructItem()
    {
        string name = "";
        int temp = (int)Random.Range(0,2);
        if (temp == 1)
        {
            weaponType item = (weaponType)((int)Random.Range(0,2));
            name = WeaponPrefix[Random.Range(0, WeaponPrefix.Length)] + "|" + item + "|" + WeaponSuffix[Random.Range(0, WeaponSuffix.Length)];
        }
        else
        {
            armorType item = (armorType)((int)Random.Range(0,5));
            name = ArmorPrefix[Random.Range(0, ArmorPrefix.Length)] + "|" + item + "|" + ArmorSuffix[Random.Range(0, ArmorSuffix.Length)];
        }

        string[] affixes = name.Split('|');
        string prefix = affixes[0];
        string type = affixes[1];
        type = type.Replace("|", "");
        name = name.Replace("|", "");
        string suffix = affixes[2];

        int physDamage = 0;
        int fireDamage = 0;
        int coldDamage = 0;
        int speed = 0;
        int critChance = 0;
        int lifeLeech = 0;

        int armor = 0;
        int health = 0;
        int allRes = 0;
        int lifeRegen = 0;
        int moveSpeed = 0;
        int cooldown = 0;

        //Weapons
        if (type == "Hammer" || type == "Gun")
        {
            bool terminate = false;

            int i = -1;
            if (terminate == false)
            {
                foreach (string s in WeaponPrefix)
                {
                    i++;
                    if (s == prefix)
                    {
                        if (i <= 3)
                        {
                            physDamage = (int)Random.Range((float)WeaponPrefixStats[2*i], (float)WeaponPrefixStats[2*i + 1]);
                        }
                        else if (i <= 6 && i >= 4)
                        {
                            fireDamage = (int)Random.Range((float)WeaponPrefixStats[2*i], (float)WeaponPrefixStats[2*i + 1]);
                        }
                        else
                        {
                            coldDamage = (int)Random.Range((float)WeaponPrefixStats[2*i], (float)WeaponPrefixStats[2*i + 1]);
                        }
                    }
                }
            }
            //this should probably just be a method since I use it four times in a row...
            i = -1;
            if (terminate == false)
            {
                foreach (string s in WeaponSuffix)
                {
                    i++;
                    if (s == suffix)
                    {
                        if (i <= 3)
                        {
                            speed = (int)Random.Range((float)WeaponSuffixStats[2*i], (float)WeaponSuffixStats[2*i + 1]);
                        }
                        else if (i <= 6 && i >= 4)
                        {
                            critChance = (int)Random.Range((float)WeaponSuffixStats[2*i], (float)WeaponSuffixStats[2*i + 1]);
                        }
                        else
                        {
                            lifeLeech = (int)Random.Range((float)WeaponSuffixStats[2*i], (float)WeaponSuffixStats[2*i + 1]);
                        }
                    }
                }
            }

            ConstructItem(name, type, physDamage, fireDamage, coldDamage, speed, critChance, lifeLeech);
        }


        //Armor
        else
        {
            bool terminate = false;

            int i = -1;
            if (terminate == false)
            {
                foreach (string s in ArmorPrefix)
                {
                    i++;
                    if (s == prefix)
                    {
                        if (i <= 3)
                        {
                            armor = (int)Random.Range((float)ArmorPrefixStats[2*i], (float)ArmorPrefixStats[2*i + 1]);
                        }
                        else if (i <= 4 && i >= 6)
                        {
                            health = (int)Random.Range((float)ArmorPrefixStats[2*i], (float)ArmorPrefixStats[2*i + 1]);
                        }
                        else
                        {
                            allRes = (int)Random.Range((float)ArmorPrefixStats[2*i], (float)ArmorPrefixStats[2*i + 1]);
                        }
                    }
                }
            }

            i = -1;
            if (terminate == false)
            {
                foreach (string s in ArmorSuffix)
                {
                    i++;
                    if (s == suffix)
                    {
                        if (i <= 3)
                        {
                            lifeRegen = (int)Random.Range((float)ArmorSuffixStats[2*i], (float)ArmorSuffixStats[2*i + 1]);
                        }
                        else if (i <= 6 && i >= 4)
                        {
                            moveSpeed = (int)Random.Range((float)ArmorSuffixStats[2*i], (float)ArmorSuffixStats[2*i + 1]);
                        }
                        else
                        {
                            cooldown = (int)Random.Range((float)ArmorSuffixStats[2*i], (float)ArmorSuffixStats[2*i + 1]);
                        }
                    }
                }
            }

            ConstructItem(name, type, armor, health, allRes, lifeRegen, moveSpeed, cooldown);
        }
    }
}
