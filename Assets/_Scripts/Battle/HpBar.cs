using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    public GameObject healthBar;

    /// <summary>
    /// Updates HP bar from the normalized value of itself
    /// </summary>
    /// <param name="normalizedValue">Normalized value between 0 & 1</param>
    public void SetHP(float normalizedValue)
    {
        healthBar.transform.localScale = new Vector3(normalizedValue, 1.0f);
    }
}
