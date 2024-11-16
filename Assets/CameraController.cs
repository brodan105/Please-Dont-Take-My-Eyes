using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] Vector3 desiredCamPos;

    [SerializeField] float tTime = 0.5f;

    bool startTransition = false;

    private void Update()
    {
        if (!startTransition) return;

        cam.localPosition = Vector3.Lerp(cam.localPosition, desiredCamPos, tTime * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        float camDistance = Vector3.Distance(cam.position, desiredCamPos);
        if (camDistance < 0.1f)
        {
            startTransition = false;
            return;
        }

        if(collision.tag == "Player")
        {
            startTransition = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            startTransition = false;
        }
    }
}
