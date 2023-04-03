using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreatorAndDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject newChild;

    [ContextMenu("Create Object")]
    private void CreateObject()
    {
        newChild = new GameObject("MyChild");

        newChild.transform.SetParent(transform);
    }

    [ContextMenu("DestroyObject")]
    private void DestroyObject()
    {
        Destroy(newChild);
    }

    [ContextMenu("Destroy Object", true)]
    private bool ValidateDestroyObject()
    {
        return newChild != null;
    }
}
