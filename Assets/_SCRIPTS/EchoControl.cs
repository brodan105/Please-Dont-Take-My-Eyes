using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EchoControl : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator anim;
    [SerializeField] Slider cooldownBar;

    [Header("Variables")]
    [SerializeField] float cooldownTime = 8f;

    private bool canPulse = true;
    bool checkPulseValue = false;

    float cooldownValue;

    private void Awake()
    {
        cooldownBar.maxValue = cooldownTime;
        cooldownValue = cooldownTime;
    }

    private void Update()
    {
        cooldownBar.value = cooldownValue;

        ResetBar();
        UpdateBar();
    }

    void ResetBar()
    {
        if (cooldownValue >= cooldownTime && canPulse)
        {
            cooldownValue = cooldownTime;
            checkPulseValue = false;
        }
    }

    void UpdateBar()
    {
        if (!checkPulseValue && !canPulse)
        {
            checkPulseValue = true;
            cooldownValue = 0;
        }

        if (!canPulse && checkPulseValue)
        {
            cooldownValue += Time.deltaTime;
        }
    }

    public void Pulse(InputAction.CallbackContext context)
    {
        if (!canPulse || Time.timeScale == 0) return;

        anim.SetTrigger("Pulse");
        StartCoroutine(pulseCooldown());
    }
    
    private IEnumerator pulseCooldown()
    {
        canPulse = false;
        yield return new WaitForSeconds(cooldownTime);
        canPulse = true;
        ResetBar();
    }
    
}
