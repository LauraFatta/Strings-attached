using UnityEngine;

public class CheckButton : MonoBehaviour
{
    public PuzzleManagerUI puzzleManager;

    private void OnMouseDown()
    {
        puzzleManager.CheckResult(); // يشغل الدالة لما تضغط على الزر
    }
}
