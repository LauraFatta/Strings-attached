using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [Header("Tutorial Panels")]
    public GameObject[] tutorialPanels; // Assign all your panels here in order

    [Header("Scene Settings")]
    public string nextSceneName = "PoliceStation"; // Scene to load after tutorial

    [Header("Navigation Settings")]
    public KeyCode nextKey = KeyCode.RightArrow;
    public KeyCode previousKey = KeyCode.LeftArrow;
    public KeyCode skipKey = KeyCode.S;

    private int currentPanelIndex = 0;
    private bool canNavigate = true;

    void Start()
    {
        // Initialize tutorial - show first panel, hide others
        InitializeTutorial();
    }

    void Update()
    {
        if (!canNavigate) return;

        // Handle input
        HandleInput();
    }

    void InitializeTutorial()
    {
        // Hide all panels first
        foreach (GameObject panel in tutorialPanels)
        {
            if (panel != null)
                panel.SetActive(false);
        }

        // Show first panel if available
        if (tutorialPanels.Length > 0 && tutorialPanels[0] != null)
        {
            tutorialPanels[0].SetActive(true);
            currentPanelIndex = 0;
        }

        Debug.Log($"Tutorial initialized with {tutorialPanels.Length} panels");
    }

    void HandleInput()
    {
        // Skip tutorial
        if (Input.GetKeyDown(skipKey))
        {
            SkipTutorial();
            return;
        }

        // Navigate forward
        if (Input.GetKeyDown(nextKey))
        {
            NavigateNext();
        }

        // Navigate backward
        if (Input.GetKeyDown(previousKey))
        {
            NavigatePrevious();
        }
    }

    public void NavigateNext()
    {
        if (currentPanelIndex < tutorialPanels.Length - 1)
        {
            // Move to next panel
            StartCoroutine(SwitchPanel(currentPanelIndex + 1));
        }
        else
        {
            // Last panel - finish tutorial
            FinishTutorial();
        }
    }

    public void NavigatePrevious()
    {
        if (currentPanelIndex > 0)
        {
            StartCoroutine(SwitchPanel(currentPanelIndex - 1));
        }
    }

    public void SkipTutorial()
    {
        Debug.Log("Tutorial skipped");
        LoadNextScene();
    }

    public void FinishTutorial()
    {
        Debug.Log("Tutorial completed");
        LoadNextScene();
    }

    IEnumerator SwitchPanel(int newPanelIndex)
    {
        canNavigate = false;

        // Hide current panel
        if (tutorialPanels[currentPanelIndex] != null)
            tutorialPanels[currentPanelIndex].SetActive(false);

        // Small delay for smoother transition (optional)
        yield return new WaitForSeconds(0.1f);

        // Show new panel
        if (tutorialPanels[newPanelIndex] != null)
            tutorialPanels[newPanelIndex].SetActive(true);

        currentPanelIndex = newPanelIndex;

        Debug.Log($"Switched to panel {currentPanelIndex + 1}/{tutorialPanels.Length}");

        canNavigate = true;
    }

    void LoadNextScene()
    {
        canNavigate = false;

        // Optional: Add fade out or transition effect here
        StartCoroutine(LoadSceneWithDelay());
    }

    IEnumerator LoadSceneWithDelay()
    {
        // Small delay before scene change (optional)
        yield return new WaitForSeconds(0.5f);

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }

    // Public methods for UI buttons (for future use)
    public void OnNextButtonClicked()
    {
        NavigateNext();
    }

    public void OnPreviousButtonClicked()
    {
        NavigatePrevious();
    }

    public void OnSkipButtonClicked()
    {
        SkipTutorial();
    }

    // Helper method to get current progress
    public float GetProgress()
    {
        return (float)(currentPanelIndex + 1) / tutorialPanels.Length;
    }

    // Method to jump to specific panel (useful for testing)
    public void JumpToPanel(int panelIndex)
    {
        if (panelIndex >= 0 && panelIndex < tutorialPanels.Length)
        {
            StartCoroutine(SwitchPanel(panelIndex));
        }
    }
}