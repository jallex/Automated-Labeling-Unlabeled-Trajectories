using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delay : MonoBehaviour
{
    public float startDelay;
    public Animator animator;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(DelayedAnimation());
    }

    // The delay coroutine
    IEnumerator DelayedAnimation()
    {
        yield return new WaitForSeconds(startDelay);
        animator.Play("scene-animation");
    }
}