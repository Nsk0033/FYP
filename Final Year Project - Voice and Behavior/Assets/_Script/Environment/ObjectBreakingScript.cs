using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBreakingScript : MonoBehaviour
{

    public GameObject breakedBox;

    public void Break()
    {
        GameObject breaked = Instantiate(breakedBox, transform.position, transform.rotation);
        Rigidbody[] rbs = breaked.GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rb in rbs)
        {
            rb.AddExplosionForce(150, transform.position, 30);
        }
        this.gameObject.SetActive(false);
    }
}
