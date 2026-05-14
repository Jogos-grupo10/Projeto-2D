using UnityEngine;
using Unity.Cinemachine;

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
                Vector3 oldPosition = player.transform.position;
                player.transform.position = transform.position;

                Vector3 delta = transform.position - oldPosition;

                CinemachineCamera[] virtualCameras = 
                    FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None);

                foreach (var vcam in virtualCameras)
                {
                    vcam.OnTargetObjectWarped(player.transform, delta);
                }
            }
        }
    }
}