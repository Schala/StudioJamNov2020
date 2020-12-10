using StudioJamNov2020.Battle;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
	[SerializeField] int m_Health = 50;
	[SerializeField] AudioClip m_Collect = null;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.TryGetComponent(out Combatant combatant))
			if (combatant.CompareTag("Player"))
			{
				combatant.TakeDamage(-m_Health); // negative damage = heal
				AudioSource.PlayClipAtPoint(m_Collect, transform.position, 0.5f);
				Destroy(gameObject);
			}
	}
}
