using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{

    public GameObject player;
    public Animator animator;

    public void Die()
    {
        animator.Play("PlayerDeath");
    }

    public void Destroy()
    {
        player.SetActive(false);
    }

}
