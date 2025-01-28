using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : MonoBehaviour
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

    public void UsePotion()
    {

    }

    public void UseEther()
    {

    }

    public void UseElixir()
    {

    }

    public void Back()
    {
        gameObject.SetActive(false);
    }
}
