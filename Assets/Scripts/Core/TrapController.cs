using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem[] particles = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach(var cur in particles)
            {
                cur.Play();
            }
            AudioController.Instance.PlayLargeBlaze();
            LevelController.Instance.DoLost();
        }
    }

}
