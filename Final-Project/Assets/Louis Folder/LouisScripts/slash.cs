using UnityEngine;

public class slash : MonoBehaviour
{
    public float slashSpeed = 4; // Adjust the speed as needed.
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTransform == null)
        {
            Debug.LogError("Player not found!");
            Destroy(gameObject); // Destroy the slash if the player is not found.
        }
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            // Calculate the direction to the player.
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

            // Move the slash towards the player.
            transform.Translate(directionToPlayer * slashSpeed * Time.deltaTime);
        }
    }
}
