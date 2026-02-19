using UnityEngine;

public class ImprovisedBridge : Structure
{
    [SerializeField] private GameObject invisibleWall;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    } 

    public override void Use(PlayerInteraction player, Interactable itemInHands)
    {
        if (itemInHands != null) return;

        base.Use(player, null);

        animator.SetTrigger("Kick");
        invisibleWall.SetActive(false);
    }
}