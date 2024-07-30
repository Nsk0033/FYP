using System;
using System.Collections;
using UnityEngine;

public class InvokeHelper : MonoBehaviour
{
    public void InvokeMethod(Action method, float delay)
    {
        StartCoroutine(InvokeCoroutine(method, delay));
    }

    private IEnumerator InvokeCoroutine(Action method, float delay)
    {
        yield return new WaitForSeconds(delay);
        method();
        Destroy(this); // Clean up after invoking
    }
}
