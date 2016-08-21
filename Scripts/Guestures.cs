using UnityEngine;
using System.Collections;


public class Guestures : MonoBehaviour {
    public Conversations convo;
    int choices;
    Animator anim;
    bool aniupdate;

	// Use this for initialization
	void Start () {

	    anim = GetComponent<Animator>();


    }
	
	// Update is called once per frame
	void Update ()
    {
        aniupdate = false;

        if (convo.getUpdateCount())
        {
            aniupdate = true;
        }

        //Debug.Log(convo.getCounter());
        if (convo.getGoodOpt() && aniupdate)
        {
            choices = 1;
        }

        if (convo.getOkayOpt() && aniupdate)
        {
            choices = 2;
        }

        if (convo.getBadOpt() && aniupdate)
        {
            choices = 3;
        }

        aniupdate = false;

        anim.SetInteger("Option1", choices);
        choices = 0;

        convo.setGoodOpt(false);
        convo.setOkayOpt(false);
        convo.setBadOpt(false);
    }

}

 