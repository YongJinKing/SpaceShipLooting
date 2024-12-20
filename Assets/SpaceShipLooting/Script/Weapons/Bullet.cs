using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{

    private IObjectPool<Bullet> managedPool;   // 관리되는 객체 풀
    private bool isReleased = false;           // 풀에 반환되었는지 여부를 추적하는 플래그

    [Header("Bullet Settings")]
    private float lifeTime; // 총알 수명 (5초)
    private float damage;   // 총알 데미지
    [SerializeField] GameObject impactEffectPrefab;
    [SerializeField] private float effectLifetime = 2f;


    private void OnEnable()
    {
        lifeTime = GameManager.Instance.PlayerStatsData.bulletlifeTime;
        damage = GameManager.Instance.PlayerStatsData.bulletDamage;


        // 총알이 활성화되면 다시 사용할 수 있으므로 플래그를 false로 초기화
        isReleased = false;

        // 총알 활성화 시 5초 뒤 ReturnToPool 메서드 호출 예약
        Invoke(nameof(DestroyBullet), lifeTime);
    }

    private void OnDisable()
    {
        // 비활성화될 때 예약된 Invoke 취소
        CancelInvoke(nameof(DestroyBullet));
    }

    public void SetManagePool(IObjectPool<Bullet> pool)
    {
        managedPool = pool;
    }

    public void DestroyBullet()
    {
        // 풀로 반환되었는지 여부 확인
        if (isReleased) return;

        // 풀로 반환
        if (managedPool != null)
        {
            managedPool.Release(this);
        }
        else
        {
            Destroy(gameObject); // 풀 관리가 없는 경우 안전하게 파괴
        }

        // 풀에 반환되었음을 표시
        isReleased = true;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Weapons") ||
            collision.gameObject.CompareTag("Player") ||
            collision.gameObject.CompareTag("Blade")) return;

        Debug.Log($"Bullet이 {collision.gameObject.name}에 충돌");

        ContactPoint contact = collision.contacts[0];
        Vector3 hitPoint = contact.point;
        Vector3 hitNormal = contact.normal;

        // 이펙트 생성
        if (impactEffectPrefab != null)
        {
            GameObject impactEffect = Instantiate(impactEffectPrefab, hitPoint, Quaternion.LookRotation(hitNormal));
            Destroy(impactEffect, effectLifetime); // 일정 시간 후 이펙트 제거
        }

        // 적이면 데미지 입히기
        Damageable damageable = collision.gameObject.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.InflictDamage(damage);
        }

        // 풀로 반환
        DestroyBullet();
    }
}
