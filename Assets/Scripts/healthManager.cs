using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthManager : MonoBehaviour
{

    public GameObject player;
    public List<GameObject> mobs;
    public GameObject[] loot;
    public int lootRarity;

    public List<ParticleSystem> mobBloodEffect;
    public List<AudioSource> mobDeathSound;


    private float maxPlayerHealth = 500;
    private float maxPlayerMana = 100;
    public float playerHealth;
    public float playerMana;
    private float healthRegen = 0.01f;
    private float manaRegen = 0.01f;

    private float chance;

	void Start ()
    {
        playerHealth = maxPlayerHealth;
        playerMana = maxPlayerMana;

        InvokeRepeating("regenerator", 0, 0.3f);
    }

    private void regenerator()
    {
        if (playerHealth < maxPlayerHealth)
        {
            if ((playerHealth + (healthRegen * maxPlayerHealth)) > maxPlayerHealth)
            {
               playerHealth = maxPlayerHealth;
            }
            else
               playerHealth += (healthRegen * maxPlayerHealth);
        }
        if (playerMana < maxPlayerMana)
        {
            if ((playerMana + (manaRegen * maxPlayerMana)) > maxPlayerMana)
            {
                playerMana = maxPlayerMana;
            }
            else
                playerMana += (manaRegen * maxPlayerMana);
        }
    }

    public void damageHealth(GameObject character, float damage)
    {


        if (character == player)
        {
            if (playerHealth > 0)
            {
                playerHealth -= damage;
            }
            else
            {
                //reload the level or something

                //this is just temp
                player.transform.position = Vector3.zero;
                playerHealth = maxPlayerHealth;
            }
        }

        else
        {
            if (character.GetComponent<crawlerAI>().mobHealth > 0)
            {
                character.GetComponent<crawlerAI>().mobHealth -= damage * ((float)character.GetComponent<crawlerAI>().rarity + 1);
                character.GetComponent<crawlerAI>().bloodEffect.Play();
                if(character.GetComponent<crawlerAI>().mobHealth <= 0)
                {
                    character.GetComponent<crawlerAI>().crawlerAudio[0].Play();
                    character.GetComponent<crawlerAI>().mobCenter.GetComponent<mobFlock>().mobs.Remove(character);

                    chance = (int)Random.Range(0, 101);
                    
                    if (chance < character.GetComponent<crawlerAI>().lootChance && chance > -1)
                    {
                        Debug.Log("drop");
                        chance = (int)Random.Range(0, 21);
                        if (chance < 13 && chance > -1)
                            lootRarity = 0;
                        else if (chance < 20 && chance > 12)
                            lootRarity = 1;
                        else
                            lootRarity = 2;

                        Instantiate(loot[lootRarity], new Vector3(character.transform.position.x, character.transform.position.y + 0.7f, character.transform.position.z), 
                                    Quaternion.Euler(-90, 0, 0));
                    }

                    DestroyObject(character);
                }
            }
            else
            {
                character.GetComponent<crawlerAI>().bloodEffect.Play();
                character.GetComponent<crawlerAI>().crawlerAudio[0].Play();

                //determine if loot dropped -- determineLoot(mobRarity rarity) //mobRarity being an enum
                //instantiate loot object
            }
        }
    }
}
