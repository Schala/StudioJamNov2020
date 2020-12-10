using UnityEngine;

public class SmokeEmission : MonoBehaviour
{
    Quaternion m_Rotation;

	private void Awake() => m_Rotation = transform.rotation;

	private void LateUpdate() => transform.rotation = m_Rotation;
}
