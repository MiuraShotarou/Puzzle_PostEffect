using UnityEngine;

public class AcquisitionAnimation : MonoBehaviour
{
    Vector3[] _bezierA;
    Vector3[] _bezierB;
    
    float _duration = 10f; // 何秒で終点まで進むか
    private float _t = 0f;

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (_bezierA == null || _bezierB == null)
        {
            _bezierA = new[] { transform.position, new Vector3(transform.position.x + 10, transform.position.y - 0.5f)}; //Enemy側のライン
            _bezierB = new[] { new Vector3(transform.position.x, -6.25f - 0.5f), new Vector3(-11.3f, -6.25f)}; //ターゲット側のライン
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_bezierA[0], _bezierA[1]);
        Gizmos.DrawLine(_bezierB[0], _bezierB[1]);
    }
#endif
    void OnEnable()
    {
        _bezierA = new[] { transform.position, new Vector3(transform.position.x + 10, transform.position.y - 0.5f)}; //Enemy側のライン
        _bezierB = new[] { new Vector3(transform.position.x, -6.25f - 0.5f), new Vector3(-11.3f, -6.25f)}; //ターゲット側のライン
    }

    void Update()
    {
        if (_t <= _duration)
        {
            BezierCurve(_bezierA, _bezierB, Mathf.Lerp(0, 1, _t/_duration));
            _t += Time.deltaTime;
        }
        else
        {
            enabled = false;
        }
    }
    
    void BezierCurve(Vector3[] bezierA, Vector3[] bezierB, float t)
    {
        Vector3 vA = Vector3.Lerp(bezierA[0], bezierA[1], t);
        Vector3 vB = Vector3.Lerp(bezierB[0], bezierB[1], t);
        transform.position = Vector3.Lerp(vA, vB, t);
    }
}