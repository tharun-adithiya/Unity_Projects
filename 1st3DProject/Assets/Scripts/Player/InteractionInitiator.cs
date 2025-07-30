using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class RTXInteraction : MonoBehaviour,IInteractionManager
{
    [HideInInspector]public bool isObjFound=false;
    [SerializeField]private InteractionManger m_manger;
    [SerializeField] private PlayerInventory m_Inventory;
    

    public void OnPassTrigger(string gameObjName)
    {
        if (isObjFound)
        {
            return;
        }
        
        isObjFound = true;
        m_manger.VoiceLineSwitcher(gameObjName);
    }

    public void OnPressCollect(GameObject objectToCollect)
    {
       objectToCollect.SetActive(false);
       m_Inventory.AddToInventory(objectToCollect);
    }
}
