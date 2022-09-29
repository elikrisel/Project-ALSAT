using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeater : MonoBehaviour
{
    [SerializeField]

    private AudioClip[] walk;
    private AudioClip[] jump;
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Step(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5)
        {
            AudioClip clip = GetRandomClip1();
            audioSource.PlayOneShot(clip);
        }
    }
    public void Hop(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5)
        {
            AudioClip clip = GetRandomClip2();
            audioSource.PlayOneShot(clip);
        }
    }


    public AudioClip GetRandomClip1()
    {
        return walk[UnityEngine.Random.RandomRange(0, walk.Length)];
    }

    public AudioClip GetRandomClip2()
    {
        return jump[UnityEngine.Random.RandomRange(0, jump.Length)];
    }

}
