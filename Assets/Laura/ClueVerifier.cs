using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ClueVerifier : MonoBehaviour
{
    [Header("References")]
    public DropZone2D[] dropZones;
    public TextMeshProUGUI resultText;

    [Header("Success Panel")]
    public GameObject successPanel;

    private Dictionary<DropZone2D, bool> lastVerificationResults = new Dictionary<DropZone2D, bool>();
    private Coroutine hideTextRoutine;

    public void VerifyClues()
    {
        int wrongCount = 0;
        int filledCount = 0;
        lastVerificationResults.Clear();

        foreach (DropZone2D zone in dropZones)
        {
            string expected = zone.GetExpectedClue()?.Trim().ToLower();
            string actual = zone.GetAssignedClue()?.Trim().ToLower();

            if (!string.IsNullOrEmpty(actual))
                filledCount++;

            bool isCorrect = expected == actual;
            lastVerificationResults[zone] = isCorrect;

            if (!isCorrect)
                wrongCount++;
        }

        // Hide success panel first
        if (successPanel != null) successPanel.SetActive(false);

        // Case 1: Correct & complete
        if (wrongCount == 0 && filledCount == dropZones.Length)
        {
            
            if (successPanel != null) successPanel.SetActive(true);
        }

        else if (wrongCount <= 2) { ShowMessage("ONE OR TWO THREADS ARE LOOSE! ", persistent: false);  }

        else
        {
            
            ShowMessage("WRONG OR INCOMPLETE SOLUTION", persistent: false);
        }
    }

    public Dictionary<DropZone2D, bool> GetLastVerificationResults()
        => new Dictionary<DropZone2D, bool>(lastVerificationResults);

    private void ShowMessage(string msg, bool persistent)
    {
        if (resultText != null)
        {
            resultText.text = msg;
            resultText.gameObject.SetActive(true);

            // Cancel any running hide coroutine
            if (hideTextRoutine != null)
                StopCoroutine(hideTextRoutine);

            // Only auto-hide if not persistent
            if (!persistent)
                hideTextRoutine = StartCoroutine(HideTextAfterDelay(5f));
        }

        Debug.Log(msg);
    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (resultText != null)
            resultText.gameObject.SetActive(false);
    }
}
