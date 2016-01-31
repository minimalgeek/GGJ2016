using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

    private AudioSource[] sources;

	// Use this for initialization
	void Start () {
        sources = FindObjectsOfType<AudioSource>();
	}
	
	public void PlayByName(string name)
    {
        foreach (AudioSource source in sources)
        {
            if (source.clip.name == name)
            {
                source.Play();
                break;
            }
        }
    }
}
