using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMenu : MonoBehaviour
{
    [SerializeField] GameObject actionCover;

    private void OnEnable()
    {
        actionCover.SetActive(true);
    }

    private void OnDisable()
    {
        actionCover.SetActive(false);
    }

    public void CastFire()
    {

    }

    public void CastIce()
    {

    }

    public void CastLightning()
    {

    }

    public void Back()
    {
        gameObject.SetActive(false);
    }
}
