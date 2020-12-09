using UnityEngine;

namespace StudioJamNov2020.Entities
{
    public class DeadZone : MonoBehaviour
    {
		private void OnCollisionEnter(Collision collision) => Destroy(collision.gameObject);
	}
}
