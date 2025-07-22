using UnityEngine;

public interface IInteractionManager
{
    public void OnPassTrigger(string gameObjName);
    public void OnPressCollect(GameObject objectToCollect);
}
