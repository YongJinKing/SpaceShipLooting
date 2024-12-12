using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Floor1Door : XRSimpleInteractableOutline
{
    private Animator anim;

    void Start()
    {
        // Animator 가져오기
        anim = GetComponentInParent<Animator>();
        if (anim == null)
        {
            anim = GetComponentInParent<Animator>();
        }
    }
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        anim.SetTrigger("Open");
    }

}