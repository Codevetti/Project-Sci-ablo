using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthManager : MonoBehaviour
{

    public GameObject player;
    public List<GameObject> mobs;
    public ParticleSystem mobBloodEffect;

    private float maxPlayerHealth = 500;
    private float maxPlayerMana = 100;
    public float playerHealth;
    public float playerMana;
    private float healthRegen = 0.01f;
    private float manaRegen = 0.01f;

    private float maxCrawlerHealth = 50;

    private float rare = 1.5f;
    private float legendary = 2.5f;

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


        if (character == GameObject.FindGameObjectWithTag("Player"))
        {
            if (playerHealth > 0)
            {
                playerHealth -= damage;
            }

            else
            {
                //reload the level or something

                //this is just temp
                playerHealth = maxPlayerHealth;
            }
        }

        //doesn't work with multiple enemies if using findgameobjectwithtag
        else if (character.tag == "Mob")
        {
            if (character.GetComponent<crawlerAI>().mobHealth > 0)
            {
                character.GetComponent<crawlerAI>().mobHealth -= damage;
                mobBloodEffect.Play();
                if(character.GetComponent<crawlerAI>().mobHealth <= 0)
                {
                    DestroyObject(character);
                }
            }
            else
            {
                DestroyObject(character);
                //determine if loot dropped -- determineLoot(mobRarity rarity) //mobRarity being an enum
                //instantiate loot object
            }
        }

        else
        {
            //something is wrong
        }
    }
}
