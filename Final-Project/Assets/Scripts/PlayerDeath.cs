using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
     PlayerHealth _playerHp;

     bool isShrinking = false;

     float minSize = 0.1f;

    private void Awake()
    {
        _playerHp = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("KILL ZONE"))
        {
            if (_playerHp != null)
            {
                _playerHp.health = 0;
                Debug.Log($"The health is: {_playerHp.health}");

                isShrinking = true;

                DisableAllOtherScripts();
            }
        }
    }

    private void Update()
    {
        if (isShrinking && _playerHp.health <= 0)
        {
            ShrinkAndDestroy();
        }
    }

    private void ShrinkAndDestroy()
    {
        transform.localScale -= new Vector3(1.0f, 1.0f, 1.0f) * Time.deltaTime;

       transform.localScale = Vector3.Max(transform.localScale, new Vector3(minSize, minSize, minSize));

        if (transform.localScale.x <= minSize)
        {
            //add more death logic here

            //Destroy(gameObject);
        }
    }

    private void DisableAllOtherScripts()
    {
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

        foreach (var script in scripts)
        {
            if (script != this)
            {
                script.enabled = false;
            }
        }
    }
}
