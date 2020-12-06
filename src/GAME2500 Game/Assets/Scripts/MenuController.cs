using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject controlsSection;
    [SerializeField] GameObject instructionsSection;
    [SerializeField] GameObject loading;
    [SerializeField] AudioSource click;

    void Start()
    {
        click = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        click.Play();
        loading.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void ToggleControlsSection()
    {
        click.Play();
        controlsSection.SetActive(!controlsSection.activeSelf);
    }

    public void ToggleInstructionsSection()
    {
        click.Play();
        instructionsSection.SetActive(!instructionsSection.activeSelf);
    }
}
