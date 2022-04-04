using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curse : MonoBehaviour
{
    [SerializeField] Transform followRig;
    private void Update()
    {
        Vector3 scale = transform.localScale;
        scale.x = followRig.localScale.x;
        transform.localScale = scale;
    }
}
