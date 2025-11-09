using UnityEngine;

public class SwarmAI : AnimalAIBase
{
    public float losePlayerDistance = 20f;

    protected override void Update()
    {
        base.Update();

        if (target != null && target.CompareTag("Player"))
        {
            float dist = Vector3.Distance(transform.position, target.position);
            if (dist > losePlayerDistance)
            {
                target = null;
                agent.ResetPath();
            }
        }
        else if (target == null)
            Destroy(gameObject, 1f);
    }

    protected override void OnChasingTarget()
    {
        Debug.Log("Swarm is chasing you.");
    }
}
