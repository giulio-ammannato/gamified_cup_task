using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour {

    Transform[] trees;

    //the parent dimension, so they will just spawn inside it
    RectTransform groundRect;

	// Use this for initialization
	void Start () {

        Transform clone =  transform.Find("baum");
        Transform parentClone = transform.parent;

        transform.Find("baum").gameObject.SetActive(true);

        //hide the origianl tree
        // transform.Find("baum").gameObject.SetActive(false);

        float width = parentClone.GetComponent<SpriteRenderer>().bounds.size.x;
        float treeWidth = clone.GetComponent<SpriteRenderer>().bounds.size.x;

        //spawn a random number of trees
        int r = Random.Range(1, 10);

        //for debug
        //int r = 5;

        trees = new Transform[r];

        for(int x = 0;x<r;x++)
        {
            //the trees should be in the ground parent, not further
            //Vector3 newTreePos = new Vector3(Random.Range(-width / 2f + treeWidth/2, width / 2f - treeWidth/2), clone.position.y, clone.position.z);
            Vector3 newTreePos = new Vector3(Random.Range(-width / 2f, width / 2f), clone.position.y, clone.position.z);
            clone.position = newTreePos;
            trees[x] = clone;
            Instantiate(trees[x]);
        }

        transform.Find("baum").gameObject.SetActive(false);


    }

}
