using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

using UnityEngine.UI;   //text component

public class InventorySystem : MonoBehaviour, IHasChanged {
    public Transform slots;
    public Text inventoryText;

	void Start () {
        HasChanged();
	}

	public void HasChanged()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.Append(" ");
        foreach (Transform slotTransform in slots)
        {
            GameObject item = slotTransform.GetComponent<SlotLogic>().item;
            if (item)
            {
                builder.Append(item.name);
                builder.Append(" ");
            }
        }
        inventoryText.text = builder.ToString();
    }
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}