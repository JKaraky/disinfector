using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disinfection : MonoBehaviour
{
    StateChange stateChange;
    // Start is called before the first frame update
    void Start()
    {
        stateChange = StateChange.instance;
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Infected"))
        {
            MeshRenderer hitObjMsh = collision.gameObject.GetComponent<MeshRenderer>();

            hitObjMsh.material = stateChange.normalMaterial;

            stateChange.score++;
        }
    }
}
