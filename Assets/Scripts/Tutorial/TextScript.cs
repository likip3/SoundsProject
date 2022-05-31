using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    private bool isHere;
    public Text text;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            isHere = true;
        }

    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            isHere = false;
        }
    }
    private void FixedUpdate()
    {
        if (isHere && text.color.a < 1)
        {
            var tempColor = text.color;
            tempColor.a += 0.01f;
            text.color = tempColor;
        }
        else if (!isHere && text.color.a > 0)
        {
            var tempColor = text.color;
            tempColor.a -= 0.01f;
            text.color = tempColor;
        }
    }
}
