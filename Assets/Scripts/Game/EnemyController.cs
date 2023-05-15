using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private List<GameObject> listEnemies;

    public event Action<int> dead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < listEnemies.Count; i++)
        {
            if (listEnemies[i].GetComponent<Enemy>().GetHealt() < 0)
            {
                Destroy(listEnemies[i].gameObject);
                listEnemies.Remove(listEnemies[i]);
                dead?.Invoke(i);
            }
        }
    }
}
