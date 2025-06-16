using UnityEngine;
using TMPro; 

public class ItemButtonText : MonoBehaviour
{
    public TextMeshProUGUI itemNameText; 

    public void SetItemName(string name)
    {
        if (itemNameText != null)
        {
            itemNameText.text = name;
        }
    }
}