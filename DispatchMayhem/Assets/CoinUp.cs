using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinUp : MonoBehaviour
{
    public Transform moneyTarget;

    public float value = 0.0f;

    private Vector3 start;
    private float coinSpeed = 20.0f;
    private bool clicked = false;
   
    // Start is called before the first frame update
    void Start()
    {
        start = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked)
        {
            if ((this.transform.position - moneyTarget.position).magnitude < 0.1f)
            {
                Finances.inst.AddMoney(value);
                //TODO: Add a "money gained" sound
                Destroy(this.gameObject);
            }
            this.transform.position = Vector3.Lerp(start, moneyTarget.position, coinSpeed * Time.deltaTime);
        }
    }

    public void CoinClick()
    {
        clicked = true;
    }
}
