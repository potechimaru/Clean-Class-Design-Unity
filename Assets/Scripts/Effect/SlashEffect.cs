using UnityEngine;
using UnityEngine.Pool;
using Cysharp.Threading.Tasks;

public class SlashEffect : MonoBehaviour
{
    private IObjectPool<SlashEffect> _pool;

    public void SetPool(IObjectPool<SlashEffect> pool) => _pool = pool;

    public void Play(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
        gameObject.SetActive(true);

        var ps = GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
            ReleaseAfter(ps.main.duration).Forget();
        }
    }

    private async UniTaskVoid ReleaseAfter(float seconds)
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(seconds));

        if (this != null && gameObject.activeSelf) // Šù‚É”jŠü‚³‚ê‚Ä‚¢‚È‚¢‚©Šm”F
        {
            _pool?.Release(this);
        }
    }

    public void StopAndRelease()
    {
        gameObject.SetActive(false);
        _pool?.Release(this);
    }
}
