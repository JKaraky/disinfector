using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopCollision : MonoBehaviour
{
    StateChange stateChange;

    [Header("Spawn Points")]
    [SerializeField]
    public List<Transform> transforms = new List<Transform>();

    public AudioClip scoresound;
    AudioSource audioSource;

    Transform newTrans;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stateChange = StateChange.instance;

        newTrans = transforms[Random.Range(0, transforms.Count)];
        transform.position = newTrans.position;
        transform.rotation = newTrans.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6) //Layer 6 is Pickable
        {
            stateChange.score++;
            audioSource.PlayOneShot(scoresound, 1f);

            newTrans = transforms[Random.Range(0, transforms.Count)];
            // If the random transform picked is the same as the previous, get another random one
            while (newTrans.position == transform.position)
                newTrans = transforms[Random.Range(0, transforms.Count)];

            Debug.Log(newTrans);
            transform.position = newTrans.position;
            transform.rotation = newTrans.rotation;

        }
    }
}
