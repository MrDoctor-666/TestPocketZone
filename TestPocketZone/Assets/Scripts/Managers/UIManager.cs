using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject warningShootPanel;
    [SerializeField] GameObject diedPanel;
    public InventoryDisplay inventory;
    public Button attackButton;
    public FixedJoystick fixedJoystick;

    Coroutine currentCoroutine;


    public void OpenPanelAndClose()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        warningShootPanel.SetActive(true);
        currentCoroutine = StartCoroutine(waitForAndClose(2f, warningShootPanel));
    }

    IEnumerator waitForAndClose(float seconds, GameObject toClose)
    {
        yield return new WaitForSeconds(seconds);
        toClose.SetActive(false);
    }

    public void OpenEndGamePanel()
    {
        diedPanel.SetActive(true);
    }
}
