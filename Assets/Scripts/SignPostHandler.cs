using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignPostHandler : MonoBehaviour {

    public IntroHandler introPreFab;
    private bool over = false;

    public bool hasCircles = false;

    public TextAsset signInformation;

    // Use this for initialization
    void Awake () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //if the player move near the chest the chest will open and a menu will pop
        if(!over)
        {
            Vector3 newPos = new Vector3(transform.position.x, introPreFab.transform.position.y, introPreFab.transform.position.z);

            introPreFab.SetStory(signInformation);

            if(!hasCircles)
            {
                introPreFab.HasActivePages(hasCircles);
            }

            Instantiate(introPreFab, newPos, transform.rotation);

            over = true;
        }
        

        //Transform openchest = Instantiate(openChest, transform.position, transform.rotation);

        // Destroy(this.gameObject);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        over = false;

    }
}
