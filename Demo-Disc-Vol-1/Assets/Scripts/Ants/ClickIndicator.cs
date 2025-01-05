using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class ClickIndicator : MonoBehaviour
{
    [SerializeField] GameObject leftClick;
    [SerializeField] GameObject rightClick;
    [SerializeField] GameObject middleClick;
    [SerializeField] float indicationTime;
    

    public IEnumerator ClickIndication(Vector3 clickLocation, int clickType)
    {
        transform.position = clickLocation;
        GameObject indicator;
        if (clickType == 1) 
        { 
            indicator = leftClick;
        }
        else if (clickType == 2)
        {
            indicator = rightClick;
        }
        else if(clickType == 3)
        {
            indicator = middleClick;
        }
        else
        {
            indicator = null;
        }

        indicator.SetActive(true);
        yield return new WaitForSeconds(indicationTime);
        indicator.SetActive(false);
    }

}
