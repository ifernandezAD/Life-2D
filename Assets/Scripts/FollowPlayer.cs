using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerPosition;
    public float offsetX;
    public float offsetY;
    public float offsetZ;

    private void FixedUpdate()
    {
        if (playerPosition != null)
        {
            OnFollowPlayer();
        }   
    }

    void OnFollowPlayer()
    {
        this.transform.position = playerPosition.position + new Vector3(offsetX, offsetY, offsetZ);
    }
}
