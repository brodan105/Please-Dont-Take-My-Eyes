using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    [SerializeField] Transform playerPos;

    private void Update()
    {
        transform.position = playerPos.position;
    }
}
