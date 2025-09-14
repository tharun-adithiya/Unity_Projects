using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Runtime.InteropServices;
public class PlayerInventory : MonoBehaviour
{
    [HideInInspector] public List<GameObject> Inventory = new List<GameObject>();
    [SerializeField] private List<RawImage> objectImages= new List<RawImage>();
    [SerializeField] private GameManager m_gameManager;
    private bool m_isCalledOnInventoryFull;

    private int m_imageListBoundChecker = 0;

    private void Start()
    {
        m_isCalledOnInventoryFull = false;
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
    private void Update()
    {
        if (Inventory.Count >= 4&&!m_isCalledOnInventoryFull)
        {
            m_gameManager.OnInventoryFull();
            m_isCalledOnInventoryFull = true;
        }
        else
        {
            return ;
        }
    }
}
