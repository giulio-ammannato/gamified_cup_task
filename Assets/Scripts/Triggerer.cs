using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerer : MonoBehaviour {

    public Transform imageToShow;

    private void Awake()
    {
        imageToShow.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

       if(!imageToShow.gameObject.activeSelf)
            imageToShow.gameObject.SetActive(true);

    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (imageToShow.gameObject.activeSelf)
            imageToShow.gameObject.SetActive(false);

    }
}
