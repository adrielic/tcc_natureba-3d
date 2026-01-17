using UnityEngine;

public class StructureDeadTree : Structure
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    } 

    public override void Use(PlayerInteraction player, Interactable target)
    {
        if (target != null)
            return;

        base.Use(player, target);

        anim.SetTrigger("Interact");
    }
}