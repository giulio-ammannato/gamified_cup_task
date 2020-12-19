using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MyButtonHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    //the text of the button in the menu
    private Text[] buttonText;

    public void OnPointerClick(PointerEventData eventData)
    {
        //da migliorare, per ora se e' stato pigiato l'emento 0 (CHE DEVE ESSERE YesText) apre il cup, altrimenenti chiude solo il menu'
        //if(eventData.pointerEnter == buttonText[0].gameObject)
            //GameMaster.gm.DestroyWantToPlayMenu(true);
        //else
            //GameMaster.gm.DestroyWantToPlayMenu(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //check which text element have been accessed and update its color
        for(int x = 0; x < buttonText.Length; x++)
        {
            if(buttonText[x].gameObject == eventData.pointerEnter)
            {
                buttonText[x].color = Color.red;
                break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //check which text element have been accessed and update its color
        for (int x = 0; x < buttonText.Length; x++)
        {
            if (buttonText[x].gameObject == eventData.pointerEnter)
            {
                buttonText[x].color = Color.white;
                break;
            }
        }
    }
}
