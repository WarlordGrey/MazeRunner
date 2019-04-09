using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public static AudioController Instance { get; private set; } = null;

    [SerializeField]
    private AudioSource mainSound = null;
    [SerializeField]
    private AudioSource btnClick = null;
    [SerializeField]
    private AudioSource win = null;
    //[SerializeField]
    //private AudioSource lost = null;
    [SerializeField]
    private AudioSource largeBlaze = null;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
            Instance = this;
            mainSound.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (Instance.Equals(this))
        {
            Instance = null;
        }
    }

    public void PlayButtonClick()
    {
        btnClick.Play();
    }

    public void PlayWin()
    {
        win.Play();
    }

    public void PlayLargeBlaze()
    {
        largeBlaze.Play();
    }

    private void OnLevelWasLoaded(int level)
    {
        //btnClick.Stop();
        win.Stop();
        largeBlaze.Stop();
    }

}
