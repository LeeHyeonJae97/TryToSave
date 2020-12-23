using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, new Vector3(0.1f, 0, -1), 15, 1 << LayerMask.NameToLayer("Zombie"));
        Debug.Log(hits.Length);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, new Vector3(0.1f, 0, -1) * 15);
    }
}
