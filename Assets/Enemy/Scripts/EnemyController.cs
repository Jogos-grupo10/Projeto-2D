using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject spike;
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("spike"))
            {
                spike = child.gameObject;
                break;
            }
            
        }
        
        if (spike == null) spike.SetActive(false);
        
    }

    public void TriggerSpike()
    {
        if (spike != null)
        {
            spike.SetActive(true);
            
            Animator spikeAnim = spike.GetComponent<Animator>();
            if (spikeAnim != null)
            {
                spikeAnim.Play("Spike", -1, 0f);
            }
        }
    }
}
