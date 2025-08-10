using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ClueVerifier : MonoBehaviour
{
    public DropZone2D[] dropZones;
    public TextMeshProUGUI resultText; // Optional: assign a UI text for messages

    private Dictionary<DropZone2D, bool> lastVerificationResults = new Dictionary<DropZone2D, bool>();

    public void VerifyClues()
    {
        int wrongCount = 0;
        lastVerificationResults.Clear();

        foreach (DropZone2D zone in dropZones)
        {
            string expected = zone.GetExpectedClue()?.Trim().ToLower();
            string actual = zone.GetAssignedClue()?.Trim().ToLower();
            bool isCorrect = expected == actual;

            lastVerificationResults[zone] = isCorrect;

            if (!isCorrect)
            {
                wrongCount++;
            }
        }

        // Display result
        if (wrongCount == 0)
        {
            ShowMessage(" All clues are correct!");
        }
        else if (wrongCount <= 2)
        {
            ShowMessage($" You have {wrongCount} incorrect entr{(wrongCount == 1 ? "y" : "ies")}.");
        }
        else
        {
            ShowMessage(" The solution is wrong.");
        }
    }

    // Get results from last verification (useful for highlighting wrong slots)
    public Dictionary<DropZone2D, bool> GetLastVerificationResults() => new Dictionary<DropZone2D, bool>(lastVerificationResults);

    private void ShowMessage(string msg)
    {
        if (resultText != null)
        {
            resultText.text = msg; // Replace text instead of appending
        }
        Debug.Log(msg);
    }
}