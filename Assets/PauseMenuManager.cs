using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PauseMenuManager : MonoBehaviour
{
    bool isPaused = false;

    [SerializeField] GameObject _pauseMenu;
    [SerializeField] EventSystem _event;
    [SerializeField] GameObject firstSelectedButton;

    private void Start()
    {
        CheckPause();
    }

    public void PauseButton(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;

        CheckPause();

        Debug.Log("PAUSE");
    }

    void CheckPause()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
            _event.firstSelectedGameObject = firstSelectedButton;
        }
        else
        {
            Time.timeScale = 1;
            _pauseMenu.SetActive(false);
        }
    }
}
