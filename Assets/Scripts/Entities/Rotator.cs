using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float m_SpeedMultiplier = 5f;

    void Update() => transform.Rotate(Vector3.up * Time.deltaTime * m_SpeedMultiplier);
}
