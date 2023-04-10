using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager<T> : MonoBehaviour where T : MonoBehaviour
{
    private List<string> blackList;

    [SerializeField] bool Show = true;
   public void Log(string mensaje)
    {
        if (Show) 
        {
            Debug.Log(mensaje);
        
        }
    }

    public void LogError(string mensaje)
    {
        if (Show)
        {
            Debug.LogError(mensaje);
        }
    }

    public void LogWarning(string mensaje)
    {
        if (Show)
        {
            Debug.LogWarning(mensaje);
        }
    }

    public void DrawLine(Vector3 start, Vector3 end)
    {
        if (Show)
        {
            Debug.DrawLine(start, end);
        }
    }

    public void DrawRay(Vector3 start, Vector3 end)
    {
        if (Show)
        {
            Debug.DrawRay(start, end);
        }
    }
}
