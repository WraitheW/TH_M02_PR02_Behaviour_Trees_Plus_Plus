using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jedi : MonoBehaviour
{
    public bool isHere = true;
    public Transform guardPos;
    public Transform newPos;

    // Start is called before the first frame update
    void Start()
    {
        //if (isHere)
        //{
        //    gameObject.SetActive(true);
        //}
        //else
        //{
        //    gameObject.SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (isHere)
        {
            //gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.transform.position = guardPos.position;
        }
        if (!isHere)
        {
            //gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.transform.position = newPos.position;
        }
    }

    public void Here()
    {
        if (isHere)
        {
            isHere = false;
        }
        else
        {
            isHere = true;
        }
    }
}
