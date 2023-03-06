using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public static GridController Instance;
    public List<GameObject> objectsTrigger;
    public List<InteractionEnabler> interactionEnablers;
    public List<MagicEnabler> magicEnablers;
    public List<CatMovement> cats;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    public void DisableTrigger()
    {
        foreach (GameObject item in objectsTrigger)
        {
            item.SetActive(false);
        }
        objectsTrigger.Clear();
    }
    public void DisableInteractionEnablers()
    {
        foreach (InteractionEnabler item in interactionEnablers)
        {
            item.gameObject.SetActive(false);
        }
        interactionEnablers.Clear();
    }
    public void DisableMagicalsEnablers()
    {
        foreach (MagicEnabler item in magicEnablers)
        {
            item.gameObject.SetActive(false);
        }
        magicEnablers.Clear();
    }
    public void ExecuteCatsMovement()
    {
        //PlayerController.Instance.PlayerAnimator.SetBool("Run", false);
        //PlayerController.Instance.PlayerAnimator.SetBool("Push", false);

        foreach (CatMovement cat in cats)
        {
            cat.ExecutePath();
        }
    }
}
