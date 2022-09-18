using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public Animator anim;

    public void WelcomeAnim()
    {
        anim.Play("Welcome");
    }
    public void MenuAnim()
    {
        anim.Play("Menu");
    }

    public void Play()
    {
        GameManager.gm.Load("AR Tests");
    }

}
