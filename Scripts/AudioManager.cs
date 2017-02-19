using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

    static bool musicStart = false;
    //!include sfx

    void Awake()
    {
        if (!musicStart)
        {
            //audio.Play();
            DontDestroyOnLoad(gameObject);
            musicStart = true;
        }
    }

    //void Update()
    //{
    //    if (SceneManager.LoadScene(""))
    //}
}
