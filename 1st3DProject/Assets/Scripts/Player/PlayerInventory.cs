using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Runtime.InteropServices;
public class PlayerInventory : MonoBehaviour
{
    [HideInInspector] public List<GameObject> Inventory = new List<GameObject>();
    [SerializeField] private List<RawImage> objectImages= new List<RawImage>();
    [SerializeField] private AudioClip m_finalAudio;
    [SerializeField] private PlayerAudio m_playerAudio;
    
    private int m_imageListBoundChecker = 0;

    private void Start()
    {
        
    }
    public void AddToInventory(GameObject objectToAdd)
    {
        RawImage objectImage= objectToAdd.GetComponent<RawImage>();
        Color tempColor = objectImages[m_imageListBoundChecker].color;
        tempColor.a = 1.0f;
        
        if (objectImage == null)
        {
            Debug.LogWarning($"Image does not exist for this {objectToAdd}");
        }
        if (Inventory.Contains(objectToAdd))
        {
            return;
        }
        Inventory.Add(objectToAdd);
        if (m_imageListBoundChecker <= objectImages.Count)
        {
            objectImages[m_imageListBoundChecker].color = tempColor;
            objectImages[m_imageListBoundChecker].texture=objectImage.texture;
            m_imageListBoundChecker++;
        }
        Debug.Log(objectToAdd + " added to inventory");

    }

}
