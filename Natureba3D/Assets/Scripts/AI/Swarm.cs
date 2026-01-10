using UnityEngine;

public class Swarm : Entity
{
    public float losePlayerDistance = 20f;

    protected override void Update()
    {
        base.Update();

        if (target != null && target.CompareTag("Player"))
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance > losePlayerDistance)
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
        Debug.Log($"{gameObject.name} is chasing the player.");
    }
}
