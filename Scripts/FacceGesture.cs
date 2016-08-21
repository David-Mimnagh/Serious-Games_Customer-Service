using UnityEngine;
using System.Collections;

public class FacceGesture : MonoBehaviour
{

    int choice;


   Animator anim;

    // Use this for initialization
    void Start()
    {
      anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Q))
        {
            choice = 1;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            choice = 2;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            choice = 3;
        }


        anim.SetInteger("Option2", choice);

        choice = 0;

    }
}