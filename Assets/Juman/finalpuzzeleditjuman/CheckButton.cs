using UnityEngine;

public class CheckButton : MonoBehaviour
{
    public PuzzleManager puzzleManager;

    private void OnMouseDown()
    {
        puzzleManager.CheckResult(); // يشغل الدالة لما تضغط على الزر
    }
}
