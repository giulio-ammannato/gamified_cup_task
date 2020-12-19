using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, IPointerClickHandler {

    private static MainMenu mainMenu;

    private void Awake()
    {
        if(mainMenu == null)
        {
            mainMenu = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenu>();
          
        }
       
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if the play button is pressed start the game
        if (transform.Find("PlayButton").gameObject.transform.Find("PlayText").gameObject == eventData.pointerEnter)
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        //if the qui button is pressed quit the game
        else if (transform.Find("QuitButton").gameObject.transform.Find("QuitText").gameObject == eventData.pointerEnter)
        {
            Application.Quit();
        }
        
    }

    
}
