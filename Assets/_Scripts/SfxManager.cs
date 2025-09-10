using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public List<AudioClip> drawSFX = new List<AudioClip>();
    public List<AudioClip> discardSFX = new List<AudioClip>();
    public List<AudioClip> playSFX = new List<AudioClip>();
    public List<AudioClip> shuffleSFX = new List<AudioClip>();

    AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    internal AudioClip DrawSFX
    {
        get { return drawSFX[Random.Range(0, drawSFX.Count)]; }
    }

    internal AudioClip PlaySFX
    {
        get { return playSFX[Random.Range(0, playSFX.Count)]; }
    }
    internal AudioClip ShuffleSFX
    {
        get { return shuffleSFX[Random.Range(0, shuffleSFX.Count)]; }
    }

    public void PlayDrawSFX()
    {
        source.clip = DrawSFX;
        source.Play();
    }

    public void PlayPlaySFX()
    {
        source.clip = PlaySFX;
        source.Play();
    }
    public void PlayShuffleSFX()
    {
        source.clip = ShuffleSFX;
        source.Play();
    }
}
