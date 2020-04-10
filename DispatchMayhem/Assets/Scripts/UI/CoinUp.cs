using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinUp : MonoBehaviour
{
    public Transform moneyTarget;

    public float value = 0.0f;

    private Vector3 startPos;
    private float coinSpeed = 7.5f;
    private bool clicked = false;
   
    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked)
        {
            if ((this.transform.position - moneyTarget.position).magnitude < 0.5f)
            {
                Finances.inst.AddMoney(value);
                //TODO: Add a "money gained" sound
                clicked = false;
                this.transform.position = startPos;
                this.gameObject.SetActive(false);
            }
            this.transform.position = Vector3.Lerp(this.transform.position, moneyTarget.position, coinSpeed * Time.deltaTime);
        }
    }

    public void CoinClick()
    {
        clicked = true;
    }
}
