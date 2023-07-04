using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Activate spawns based on player location
/// </summary>
public class SpawnsActivator : MonoBehaviour
{
    [SerializeField] private string Tag = "Player";
    [SerializeField] private List<Transform> spawns;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Tag))
        {
            for(int i = 0; i < spawns.Count; i++) 
            {
                spawns[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag))
        {
            for (int i = 0; i < spawns.Count; i++)
            {
                spawns[i].gameObject.SetActive(true);
            }
        }
    }
}
