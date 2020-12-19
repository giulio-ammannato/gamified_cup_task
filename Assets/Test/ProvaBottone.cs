using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProvaBottone : MonoBehaviour {

    [SerializeField]
    private Text buttonText;
	
    public void SetButtonTextColor()
    {
        buttonText.color = Color.red;
    }
}
