using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingUnityChan : MonoBehaviour
{
    public Sprite sprite;
    private float time;
    private SpriteRenderer myRenderer;

    void Start()
    {
        time = 0.0f;
        myRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        time += Time.deltaTime;
        myRenderer.material.color = GetAlphaColor(myRenderer.material.color);


        if (time > 3.0f)
        {
            Destroy(this.gameObject);
        }
        if (time > 1.0f)
        {
            GameObject.Find("GameManager").GetComponent<StageScript>().Missed();
            GetComponent<ParticleSystem>().Play();
            myRenderer.sprite = null;
        }
        else if (time > 0.5f)
        {
            myRenderer.sprite = sprite;
        }
    }

    Color GetAlphaColor(Color color)
    {
        float temp = Mathf.Sin(time*16.0f) * 0.5f + 0.5f;
        color.g = temp;
        color.b = temp;
        return color;
    }
}
