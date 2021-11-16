using UnityEngine;

public enum ObjectMobility
{
    Static,
    Movable,
    Portable,
    Pocket
}

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] ObjectMobility _mobility;
    public ObjectMobility Mobility => _mobility;
    public float Mass => _rigidbody == null ? float.PositiveInfinity : _rigidbody.mass;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}
