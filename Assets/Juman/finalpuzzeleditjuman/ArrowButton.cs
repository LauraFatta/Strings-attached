using UnityEngine;

public class ArrowButton : MonoBehaviour
{
    public int slotIndex; // رقم الخانة (0 أو 1 أو 2)
    public int direction; // +1 للسهم اللي فوق، -1 للسهم اللي تحت
    public SimplePuzzleController controller;

    public void OnClick()
    {
        controller.ChangeDigit(slotIndex, direction);
    }
}
