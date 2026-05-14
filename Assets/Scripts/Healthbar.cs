using System;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public GameObject heartPrefab;
    public Entity player;

    List<HealthHeart> hearts = new List<HealthHeart>();

    public int healthPerHeart = 10;

    public void Start()
    {
        DrawHearts();
    }

    public void DrawHearts()
    {
        ClearHearts();

        int heartsToMake = player.maxHealth / healthPerHeart;

        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            int currentHeartHealth = player.health - (i * healthPerHeart);

            if (currentHeartHealth >= healthPerHeart)
            {
                hearts[i].SetHeartImage(HeartStatus.full);
            }
            else
            {
                hearts[i].SetHeartImage(HeartStatus.empty);
            }
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab, transform);
        
        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(HeartStatus.empty);

        hearts.Add(heartComponent);
    }

    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeart>();
    }
}