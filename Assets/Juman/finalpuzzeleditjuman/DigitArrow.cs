using UnityEngine;

public class DigitArrow : MonoBehaviour
{
    public PuzzleManager puzzleManager;  // نربطه بمدير البازل
    public int digitIndex;               // رقم الخانة (0، 1، أو 2)
    public int direction;                // +1 لليمين، -1 لليسار

    private void OnMouseDown()
    {
        puzzleManager.ChangeDigit(digitIndex, direction);
    }
}
