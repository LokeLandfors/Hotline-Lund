using UnityEngine;

public class Door : MonoBehaviour
{
    public float interactDistance = 2f; // How close the player needs to be
    public GameObject brokenDoorPrefab; // Optional: a prefab of the broken door
    public AudioClip breakSound; // Optional: sound effect when breaking

    private Transform player;
    private bool isPlayerNear = false;

    void Start()
    {
        // Find the player by tag (make sure your player has the "Player" tag)
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        // Check distance to player
        float distance = Vector2.Distance(transform.position, player.position);
        isPlayerNear = distance <= interactDistance;

        // If player is near and presses middle mouse button
        if (isPlayerNear && Input.GetMouseButtonDown(2)) // 2 = middle mouse
        {
            BreakDoor();
        }
    }

    void BreakDoor()
    {
        // Play break sound if assigned
        if (breakSound != null)
        {
            AudioSource.PlayClipAtPoint(breakSound, transform.position);
        }

        // Spawn broken door prefab if assigned
        if (brokenDoorPrefab != null)
        {
            Instantiate(brokenDoorPrefab, transform.position, transform.rotation);
        }

        // Destroy the original door
        Destroy(gameObject);
    }

    // Optional: visualize interact distance in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }
}
