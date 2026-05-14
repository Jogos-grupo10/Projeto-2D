using System;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public GameObject heartPrefab;
    public Entity player;
    
    List<HealthHeart> hearts =  new List<HealthHeart>();
    
    public void Start()
    {
        DrawHearts();
    }

    public void DrawHearts()
    {
        ClearHearts();
        
        // how many hearts to make total
        // based of max health
        float maxHealthRemainder = player.maxHealth % 2;
        int heartsToMake = (int)((player.maxHealth / 10) + maxHealthRemainder);
        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(player.health - i, 0, 1);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }

    }
    
    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);
        
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
