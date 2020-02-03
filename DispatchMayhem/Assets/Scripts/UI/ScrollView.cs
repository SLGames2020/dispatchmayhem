using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollView : MonoBehaviour
{
    public GameObject ScrollBox;
    int counter;

    public void TogglePanel()
    {
        counter++;
        if (counter % 2 == 1)
        {
            ScrollBox.gameObject.SetActive(true);
        }
        else
        {
            ScrollBox.gameObject.SetActive(false);
        }
    }
}
