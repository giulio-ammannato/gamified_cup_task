using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {


    public static LevelManager levelManager;

    private float alpha;

    bool fadeOut = true;

    Color color;
    //the image that will fade on loading
    Image fadingImage;

    private void Awake()
    {
        if (levelManager == null)
        {
            levelManager = GameObject.FindGameObjectWithTag("LM").GetComponent<LevelManager>();
        }

        DontDestroyOnLoad(this);

        fadingImage = transform.Find("FadingImage").transform.GetComponent<Image>();

        color = fadingImage.color;
        transform.gameObject.SetActive(false);
    }

    public void LoadNextLevelWithFading(int index)
    {
        transform.gameObject.SetActive(true);
        alpha = 0;
        StartCoroutine(LoadNextLevelWithFading(transform, index));
    }


    private void Update()
    {
       
        //alpha = fadingImage.color.a;
        if(fadeOut)
        {
            alpha += Time.unscaledDeltaTime;
            if (alpha >= 1)
                fadeOut = false;
        }
        else
        {
            alpha -= Time.unscaledDeltaTime;
            if (alpha <=0)
            {
                fadeOut = true;
                fadingImage.color = new Color(color.r, color.g, color.b, 0);
                transform.gameObject.SetActive(false);
            }
                
        }
        fadingImage.color = new Color(color.r, color.g, color.b, alpha);
    }

    public IEnumerator LoadNextLevelWithFading(Transform clone, int _index)
    {
        yield return new WaitForSecondsRealtime(1);
        //yield return new WaitWhile(() => alpha < 1);
        SceneManager.LoadScene(_index);
    }

}
