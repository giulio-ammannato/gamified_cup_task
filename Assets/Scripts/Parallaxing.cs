using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

    public Transform[] backgrounds;     //array list of all the back and forgrounds to be parallax
    private float[] parralaxScales;     //the proportion of the camera movement to move the backgrounds by
    public float smoothing = 1f;        //How smooth the parallax is going to be. Make sure to set this above 0

    private Transform cam;              //reference to the main cam transform
    private Vector3 previousCamPos;     //store the position of the camera in the previous frame

    //is called before start(), great for references prima inizializzazione
    private void Awake()
    {
        //set up the camera references
        cam = Camera.main.transform;
    }

    // Use this for initialization
    void Start () {
        //theprevioous frame had the current frame's camera position
        previousCamPos = cam.position;

        //assigning corrisponding parallazScales
        parralaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parralaxScales[i] = backgrounds[i].position.z * -1;
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        //for each backgroun
        for(int i = 0; i < backgrounds.Length; i++)
        {
            //parallax is the opposite of the camera movement because the previous frame multiplued by the scale
            float parallax = (previousCamPos.x - cam.position.x) * parralaxScales[i];

            //set a target x postiion which is the current position plus the parallax
            float backgroundTargetPositionX = backgrounds[i].position.x + parallax;

            //create a target position which is the backgrounds current position with its target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPositionX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade between current position and the target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        //set the previous campos to the camera position at the end of the frame
        previousCamPos = cam.position;
		
	}
}
