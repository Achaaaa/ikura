using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSizeCalculator : MonoBehaviour
{
    [Header("望みのサイズ[mm]")]
    public Vector2 size_mm;

    [Header("計算結果のサイズ[px]")]
    public Vector2 size_px;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        size_px.x = 2340 * size_mm.x / 147.598f;
        size_px.y = 1080 * size_mm.y / 68.1221f;
        this.GetComponent<RectTransform>().sizeDelta  = new Vector2 (size_px.x, size_px.y);
    }
}
