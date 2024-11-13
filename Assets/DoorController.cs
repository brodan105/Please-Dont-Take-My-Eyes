using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public static DoorController instance;

    [Header("References")]
    [SerializeField] GameObject DoorInteractableGUI_Key;
    [SerializeField] GameObject DoorInteractableGUI_Pad;

    bool inTrigger = false;

    private void Awake()
    {
        instance = this;

        DoorInteractableGUI_Key.SetActive(false);
        DoorInteractableGUI_Pad.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Door")
        {
            inTrigger = true;

            if (KeyCollide.instance.hasKey)
            {
                var gamepad = Gamepad.current;
                if(gamepad == null)
                {
                    DoorInteractableGUI_Key.SetActive(true);
                }
                else
                {
                    DoorInteractableGUI_Pad.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Door")
        {
            inTrigger = false;

            if (KeyCollide.instance.hasKey)
            {
                var gamepad = Gamepad.current;
                if (gamepad == null)
                {
                    DoorInteractableGUI_Key.SetActive(false);
                }
                else
                {
                    DoorInteractableGUI_Pad.SetActive(false);
                }
            }
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if(context.action.WasPressedThisFrame() && inTrigger && KeyCollide.instance.hasKey)
        {
            // Load next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(context.action.WasPressedThisFrame() && !KeyCollide.instance.hasKey)
        {
            Debug.Log("I need to find a key");
        }
    }
}
