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


        if (time > 2.4f)
        {
            Destroy(this.gameObject);
        }
        else if (time > 0.4f)
        {
            GameObject.Find("GameManager").GetComponent<StageScript>().Missed();
            GetComponent<ParticleSystem>().Play();
            myRenderer.sprite = null;
        }
        else if (time > 0.2f)
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
