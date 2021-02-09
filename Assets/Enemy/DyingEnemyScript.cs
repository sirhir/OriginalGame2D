using UnityEngine;

public class DyingEnemyScript : MonoBehaviour
{
    public AudioClip dyingEnemySound;
    private bool yetSound = true;
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


        if (time > 1.4f)
        {
            Destroy(this.gameObject);
        }
        else if (time > 0.4f)
        {
            GetComponent<ParticleSystem>().Play();
            if (yetSound){
                GetComponent<AudioSource>().PlayOneShot(dyingEnemySound, 0.1f);
                yetSound = false;
            }
            myRenderer.sprite = null;
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
