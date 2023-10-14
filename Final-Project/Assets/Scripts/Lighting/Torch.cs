using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Torch : MonoBehaviour
{


    public float glowDuration = 1.0f; 
    public Color glowColor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            SpriteRenderer renderer = other.GetComponent<SpriteRenderer>();
            TilemapRenderer tileRend = other.GetComponent<TilemapRenderer>();

            if (renderer != null)
            {
                renderer.color = glowColor;

                StartCoroutine(RevertColor(renderer, glowColor, glowDuration));
            }
            if (tileRend != null)
            {
                renderer.color = glowColor;

                //StartCoroutine(ReverttColor(tileRend, glowColor, glowDuration));
            }
        }
    }

    private IEnumerator RevertColor(SpriteRenderer renderer, Color originalColor, float duration)
    {
        yield return new WaitForSeconds(duration);

        renderer.color = originalColor;
    }
    //private IEnumerator ReverttColor(TilemapRenderer renderer, Color originalColor, float duration)
    //{
    //    yield return new WaitForSeconds(duration);

    //    renderer.color = originalColor;
    //}

}
