
using UnityEngine;

public class AugmentTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            var player = InGameHolder.Instance.playerCharacter;
            if (player != null)
            {
                Debug.Log("Applying damage augment!");
                var stats = player.GetComponent<CharacterStats>();
                if (stats != null)
                {
                    stats.AddDamageModifier(new StatModifier(5, StatModType.Flat, this));
                    Debug.Log($"New Damage: {stats.Damage.Value}");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            var player = InGameHolder.Instance.playerCharacter;
            if (player != null)
            {
                Debug.Log("Removing damage augment!");
                var stats = player.GetComponent<CharacterStats>();
                if (stats != null)
                {
                    stats.RemoveAllModifiersFromSource(this);
                    Debug.Log($"Damage restored: {stats.Damage.Value}");
                }
            }
        }
    }
}
