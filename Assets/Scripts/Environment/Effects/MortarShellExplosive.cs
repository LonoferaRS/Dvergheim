using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarShellExplosive : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void Start()
    {
        animator.Play("Explosive");
    }

    private void OnAnimatorOut()
    {
        Destroy(gameObject);
    }
}
