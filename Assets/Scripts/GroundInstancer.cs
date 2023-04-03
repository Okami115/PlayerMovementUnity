using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundInstancer : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    void Start()
    {
        var newChild = Instantiate(prefab, transform);
    }

}
