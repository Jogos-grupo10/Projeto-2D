using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public string spawnID;

    void Start()
    {
        if (SpawnManager.spawnID == spawnID)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                player.transform.position = transform.position;
            }
        }
    }
}