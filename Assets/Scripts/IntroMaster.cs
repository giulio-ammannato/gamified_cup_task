using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMaster : MonoBehaviour {

    public static IntroMaster introMaster;


    private void Awake()
    {
        if (introMaster == null)
        {
            introMaster = GameObject.FindGameObjectWithTag("IM").GetComponent<IntroMaster>();
        }

    }

    // Use this for initialization
    void Start()
    {

        //hide all the UI element that are not necessary
        //check that all the ui elements are in the scene, in some scenes some element are missing
        /**
        if(transform.Find("UIOverlay").transform.Find("RedArrowChest").gameObject != null && transform.Find("UIOverlay").transform.Find("RedCirleChest").gameObject != null)
        {
            transform.Find("UIOverlay").transform.Find("RedArrowChest").gameObject.SetActive(false);
            transform.Find("UIOverlay").transform.Find("RedCirleChest").gameObject.SetActive(false);
        }
    **/

        if(GameObject.FindGameObjectWithTag("ChestPointers") != null)
        {
            GameObject.FindGameObjectWithTag("ChestPointers").transform.Find("RedArrowChest").gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("ChestPointers").transform.Find("RedCirleChest").gameObject.SetActive(false);
        }
       

        transform.Find("UIOverlay").transform.Find("TurnCounterArrow").gameObject.SetActive(false);
        transform.Find("UIOverlay").transform.Find("MoneyCounterArrow").gameObject.SetActive(false);
        transform.Find("UIOverlay").transform.Find("TurnCounterCircle").gameObject.SetActive(false);
        transform.Find("UIOverlay").transform.Find("MoneyCounterCircle").gameObject.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetActiveUI(string childName)
    {
        if (transform.Find("UIOverlay").transform.Find(childName) != null)
            transform.Find("UIOverlay").transform.Find(childName).gameObject.SetActive(true);
        else
            GameObject.FindGameObjectWithTag("ChestPointers").transform.Find(childName).gameObject.SetActive(true);
    }

    public void SetInactiveUI(string childName)
    {
        if(transform.Find("UIOverlay").transform.Find(childName) != null)
            transform.Find("UIOverlay").transform.Find(childName).gameObject.SetActive(false);
        else
            GameObject.FindGameObjectWithTag("ChestPointers").transform.Find(childName).gameObject.SetActive(false);
    }
            
}



