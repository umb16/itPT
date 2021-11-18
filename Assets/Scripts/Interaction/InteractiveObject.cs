using UnityEngine;

public enum ObjectMobility
{
    Static,
    Movable,
    Portable,
    Pocket
}
public enum ObjectType
{
    Thing,
    Tool,
}
public class InteractiveObject : MonoBehaviour
{
    [SerializeField] ObjectMobility _mobility;
    [SerializeField] ObjectType _type;
    public ObjectMobility Mobility => _mobility;
    public float Mass => _rigidbody == null ? float.PositiveInfinity : _rigidbody.mass;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}
