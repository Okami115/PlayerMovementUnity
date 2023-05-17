using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsActivator : MonoBehaviour
{

    [SerializeField] private List<Transform> spawns;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            for(int i = 0; i < spawns.Count; i++) 
            {
                spawns[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < spawns.Count; i++)
            {
                spawns[i].gameObject.SetActive(true);
            }
        }
    }
}
