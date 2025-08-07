using UnityEngine;
using TMPro;

public class ClueVerifier : MonoBehaviour
{
    public DropZone2D[] dropZones;
    public TextMeshProUGUI resultText; // Optional: assign a UI text for messages

    public void VerifyClues()
    {
        int wrongCount = 0;

        foreach (DropZone2D zone in dropZones)
        {
            string expected = zone.GetExpectedClue()?.Trim().ToLower();
            string actual = zone.GetAssignedClue()?.Trim().ToLower();


            if (expected != actual)

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

    private void ShowMessage(string msg)
    {
        if (resultText != null)
        {
            resultText.text = msg;
        }
        Debug.Log(msg);
    }
}
