using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEffect : MonoBehaviour
{
    [SerializeField] float destroyDelay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("RemoveEffect");
    }

    IEnumerator RemoveEffect()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
