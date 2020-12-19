using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroChest : MonoBehaviour {

    public Sprite openChestSprite;
    public Sprite closedChestSprite;

    public float cupYOffset = -12f;

    public Transform cupTask;
    bool cupIson = false;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        Canvas.ForceUpdateCanvases();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        IntroMaster.introMaster.SetInactiveUI("RedArrowChest");
        IntroMaster.introMaster.SetInactiveUI("RedCirleChest");

        //if the player move near the chest the chest will open and a menu will pop
        transform.GetComponent<SpriteRenderer>().sprite = openChestSprite;
        Vector3 newPos = new Vector3(transform.position.x, cupTask.position.y, cupTask.position.z);
        if(!cupIson)
        {
            Instantiate(cupTask, newPos, transform.rotation);
            cupIson = true;
        }

        //Transform openchest = Instantiate(openChest, transform.position, transform.rotation);

        // Destroy(this.gameObject);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        transform.GetComponent<SpriteRenderer>().sprite = closedChestSprite;
        cupIson = false;

    }
}
