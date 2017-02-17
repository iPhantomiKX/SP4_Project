using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemAssigner : MonoBehaviour, IHasChanged {

    [SerializeField] Transform slots;
    [SerializeField] Text itemText;

	// Use this for initialization
	void Start () {
        HasChanged();
	}

    public void HasChanged()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.Append(" - ");
        foreach (Transform slotTransform in slots)
        {
            GameObject item = slotTransform.GetComponent<SlotLogic>().item;
            if (item)
            {
                //!data and name of card
                builder.Append(item.name);
                builder.Append(" - ");
            }
        }
        itemText.text = builder.ToString();

    }
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}
