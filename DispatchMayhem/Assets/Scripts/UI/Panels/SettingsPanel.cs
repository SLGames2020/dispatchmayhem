using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{

    public GameObject loadButton;
    public Sprite enabledSprite;
    public Sprite disabledSprite;

    private Image loadImage;

    // Start is called before the first frame update
    void Start()
    {
        loadImage = loadButton.GetComponent<Image>();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}


    void OnEnable()
    {
        if ((loadImage.sprite == disabledSprite) && (GM.inst.haveSave))
        {
            loadImage.sprite = enabledSprite;
        }
        else if ((loadImage.sprite == enabledSprite) && (!GM.inst.haveSave))
        {
            loadImage.sprite = disabledSprite;
        }
    }
    
}
