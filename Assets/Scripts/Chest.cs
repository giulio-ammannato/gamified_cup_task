using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    public Transform player;
    public Transform openChest;
    public Transform panel;

    public Transform arrow;

    public float panelYOffset = 8.0f;

    public Transform leftBoundaries;
    public Transform rightBoundaries;

    private Transform clone;
    private Sprite openChestSprite;
    private Sprite closedChestSprite;

    private float offSetY = 10f;

    private float cameraHeight;
    private float cameraWidth;
    float width;

    private float arrowTimetoLive = 1.8f;

    private bool pointingLeft = false;
    private bool isOnTop = false;

    private float distanceToPoint = 15;

    Transform arrowClone;


    // Use this for initialization
    void Start () {

        openChestSprite = openChest.GetComponent<SpriteRenderer>().sprite;
        closedChestSprite = transform.GetComponent<SpriteRenderer>().sprite;
        clone = transform;

        cameraHeight = Camera.main.orthographicSize * 2f;
        cameraWidth = cameraHeight * Camera.main.aspect;

         width = transform.GetComponent<SpriteRenderer>().bounds.size.x;


    }

    //working like a chamr
  
    public void MoveChest()
    {
        Transform oldChestPoistion = Instantiate(transform);

        //for debugging
        //bool found = false;

        //move the chest, the chest is always inside the camera FOV but it does have to not on top of the old one
        for (int x = 0; x < 10000; x++)
        {
            //the new chest position
            Vector3 newPosition = new Vector3(Random.Range(clone.position.x - cameraWidth / 2, clone.position.x + cameraWidth / 2), clone.position.y, clone.position.z);
            clone.position = newPosition;

            

            //if the chest position is not ouside of the playable area (doesn't collide with the boundaries AND is not behind them)
            if (!clone.GetComponent<SpriteRenderer>().bounds.Intersects(leftBoundaries.GetComponent<SpriteRenderer>().bounds) &&
                !clone.GetComponent<SpriteRenderer>().bounds.Intersects(rightBoundaries.GetComponent<SpriteRenderer>().bounds)
                && clone.position.x < rightBoundaries.position.x &&
                clone.position.x > leftBoundaries.position.x)
            {
                //for debugging
                /**
                if (oldChestPoistion.GetComponent<SpriteRenderer>().bounds.Intersects(clone.GetComponent<SpriteRenderer>().bounds))
                {
                    Debug.Log("newchest " + clone.GetComponent<SpriteRenderer>().bounds);
                    Debug.Log("oldchest " + oldChestPoistion.GetComponent<SpriteRenderer>().bounds);
                    Debug.Log(oldChestPoistion.GetComponent<SpriteRenderer>().bounds.Intersects(clone.GetComponent<SpriteRenderer>().bounds));
                }
    **/

                //check if the new position intersects with the old one
                if (!oldChestPoistion.GetComponent<SpriteRenderer>().bounds.Intersects(clone.GetComponent<SpriteRenderer>().bounds))
                {
                    //if the chest is not close to the old one
                    if(clone.position.x - oldChestPoistion.position.x > distanceToPoint || clone.position.x - oldChestPoistion.position.x < -distanceToPoint)
                    {
                        isOnTop = false;
                        //check what direction we need to go
                        if (clone.position.x - oldChestPoistion.position.x < 0)
                            pointingLeft = true;
                        else
                            pointingLeft = false;

                        GameMaster.gm.StartCoroutine(spawnArrow());
                    }
                    //otherwise the arrow will point the chest from above
                    else
                    {
                        isOnTop = true;
                        GameMaster.gm.StartCoroutine(spawnArrow());
                    }
                   
                    //if it doens't exit the cyle
                    break;
                }                   
                //otherwise update the old position and keep cycling
                else
                    oldChestPoistion.position = transform.position;
            }
            else
                oldChestPoistion.position = transform.position;

        }
        //for debugging
        //Debug.Log(found);

        Destroy(oldChestPoistion.gameObject);
    }

    public IEnumerator spawnArrow()
    {
        float distanceToNewChest = transform.position.x - Camera.main.transform.position.x;
        Vector3 arrowPoistion;


        //if the arrow is not close to che chest the arrow should be between the new and old chest
        if (!isOnTop)
        {
            arrowPoistion = new Vector3(Camera.main.transform.position.x + distanceToNewChest / 2, Camera.main.transform.position.y, transform.position.z);
            arrowClone = Instantiate(arrow, arrowPoistion, transform.rotation);
        }
          
        //otherwise it will be on top pointing down
        else
        {
          
            arrowPoistion = new Vector3(transform.position.x, transform.position.y +5, transform.position.z);

            //var lookPos = arrowPoistion - transform.position;

           // lookPos.z = 0;
            //lookPos.y = 0;


            //var rotation = Quaternion.LookRotation(lookPos);
            var rotation = Quaternion.Euler(0, 0, -90); // this adds a 90 degrees Y rotation

            arrowClone = Instantiate(arrow, arrowPoistion, rotation);

            //clone.rotation = Quaternion.Slerp(clone.rotation, rotation, 1f);


        }

        //render the arrow in the correct direction
        if (isOnTop)
            arrowClone.GetComponent<SpriteRenderer>().flipX = true;
        if(!pointingLeft)
            arrowClone.GetComponent<SpriteRenderer>().flipX = true;

       

        yield return new WaitForSeconds(arrowTimetoLive);

        if(arrowClone != null)
            Destroy(arrowClone.gameObject);
    }
   




    private void OnTriggerEnter2D(Collider2D collision)
    {

        //if the player move near the chest the chest will open and a menu will pop
        GameMaster.gm.OpenCupTask(transform);

        transform.GetComponent<SpriteRenderer>().sprite = openChestSprite;

        if (arrowClone != null)
            Destroy(arrowClone.gameObject);

        //Transform openchest = Instantiate(openChest, transform.position, transform.rotation);

        // Destroy(this.gameObject);

    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if the player move away from the chest the chest will close and the menu will disappear
        //GameMaster.gm.DestroyWantToPlayMenu(false);

        transform.GetComponent<SpriteRenderer>().sprite = closedChestSprite;

    }

}
