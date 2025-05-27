using UnityEngine;

public class BubbleFollowPlayer : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        direction.y = 0f; // Only rotate on the horizontal plane

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            // Rotate 180 degrees on the Y axis
            rotation *= Quaternion.Euler(0, 180f, 0);
            transform.rotation = rotation;
        }
    }
}
