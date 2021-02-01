using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    private float speed = 1.0f;
    private float time;
    private Text text;

    void Start()
    {
        text = this.gameObject.GetComponent<Text>();
    }

    void Update()
    {
        text.color = GetAlphaColor(text.color);
    }

    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;
        return color;
    }
}
