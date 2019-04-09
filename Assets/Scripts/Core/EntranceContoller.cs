using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceContoller : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem particles = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            particles.Play();
            LevelController.Instance.DoWin();
        }
    }

}
