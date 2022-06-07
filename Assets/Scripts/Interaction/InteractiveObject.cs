using Cysharp.Threading.Tasks;
using System.Linq;
using System.Threading;
using UnityEngine;
public class InteractiveObject : MonoBehaviour
{
    [SerializeField] ObjectMobility _mobility;
    [SerializeField] ObjectType _type;
    [SerializeField] LocKeysStableEnum _nameLocKey;
    public LocKeys LocKey => _nameLocKey;
    public ObjectMobility Mobility => _mobility;
    public float Mass => _rigidbody == null ? float.PositiveInfinity : _rigidbody.mass;

    private int _startLayer;
    private Rigidbody _rigidbody;
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    public void ToHandMode(int layer)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
        _cancellationTokenSource = new CancellationTokenSource();
        GetComponentsInChildren<Collider>().All(x => x.enabled = false);
        GetComponentsInChildren<Rigidbody>().All(x => x.isKinematic = true);
        gameObject.layer = layer;
    }
    public async void ToFreeMode(Vector3 push, int ignorePlayerLayer)
    {
        GetComponentsInChildren<Collider>().All(x => x.enabled = true);
        GetComponentsInChildren<Rigidbody>().All(x =>
        {
            x.isKinematic = false;
            x.WakeUp();
            return true;
        });
        _rigidbody.AddForce(push);
        gameObject.layer = ignorePlayerLayer;
        await UniTask.Delay(1000, cancellationToken: _cancellationTokenSource.Token);
        gameObject.layer = _startLayer;
    }
    private void Start()
    {
        var actions = GetComponents<IAction>();
        foreach (var action in actions)
           Debug.Log("action "+action.Name);
        _startLayer = gameObject.layer;
        _rigidbody = GetComponent<Rigidbody>();
    }

}
