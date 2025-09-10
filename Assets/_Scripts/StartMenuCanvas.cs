using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuCanvas : MonoBehaviour
{
    [SerializeField] GameObject PlayerCanvas, MenuCanvas, InfoCanvas, CreditsText;
    [SerializeField] AudioClip playSound, menuSound;

    Animator camAni;

    private void Start()
    {
        camAni = Camera.main.gameObject.GetComponent<Animator>();

        MenuCanvas.SetActive(true);
        InfoCanvas.SetActive(false);
        PlayerCanvas.SetActive(false);
        CreditsText.SetActive(false);
    }
    private void Update()
    {
        if (PlayerCanvas.activeSelf && !InfoCanvas.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) MenuButton();
        }
        else if (CreditsText.activeSelf)
        {
            if(Input.anyKeyDown) MenuButton();
        }
    }

    public void StartButton()
    {
        camAni.SetTrigger("TransCamera");
        AudioSource.PlayClipAtPoint(playSound, Camera.main.transform.position);

        MenuCanvas.SetActive(false);
        StartCoroutine(WaitToShow(InfoCanvas));
        StartCoroutine(WaitForPlayer());
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void MenuButton()
    {
        camAni.SetTrigger("TransCamera");
        AudioSource.PlayClipAtPoint(menuSound, Camera.main.transform.position);

        PlayerCanvas.SetActive(false);
        CreditsText.SetActive(false);
        StartCoroutine(WaitToShow(MenuCanvas));
    }
    public void CreditsButton()
    {
        camAni.SetTrigger("CreditsTrans");
        AudioSource.PlayClipAtPoint(menuSound, Camera.main.transform.position);

        MenuCanvas.SetActive(false);
        StartCoroutine(WaitToShow(CreditsText));
    }

    IEnumerator WaitForPlayer()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space));
        InfoCanvas.SetActive(false);
        PlayerCanvas.SetActive(true);
    }

    IEnumerator WaitToShow(GameObject menu)
    {
        yield return new WaitForSeconds(1f);

        menu.SetActive(true);
    }
}
