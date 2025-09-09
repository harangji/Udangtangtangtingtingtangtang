using UnityEngine;

public class OnCollisionClass : MonoBehaviour
{
    public CharacterBase character { get; set; }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out CharacterBase col))
        {
            CombatSystem.Instance.AddCombatEvent(new CombatEvent()
            {
                Sender = character,
                Receiver = col,
            });
        }
    }
}
