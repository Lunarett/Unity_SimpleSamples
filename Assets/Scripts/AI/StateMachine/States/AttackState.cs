using UnityEngine;

public class AttackState : State
{
    [SerializeField] private float attackRange = 3.0f;
    [SerializeField] private float attackRate = 1.0f;
    [SerializeField] private float damageAmount = 10.0f;

    private float timer = 0;
    
    public override State UpdateState()
    {
        MoveTo(transform.position);
        
        timer += Time.deltaTime;
        
        if (timer >= attackRate  && IsInRange(Target.transform.position, attackRange))
        {
            timer = 0f;
            
            // Do Damage to player
            PlayerHealth damage = Target.GetComponent<PlayerHealth>();
            if (damage != null)
            {
                Debug.Log("Damaging Player!!");
                damage.Damage(damageAmount);
            }
        }
        
        return !IsInRange(Target.transform.position, attackRange) ? nextState : this;
    }
}
