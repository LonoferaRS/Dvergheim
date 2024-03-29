using UnityEngine;
using UnityEngine.UI;

public class NecromancerWeak : Enemy
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private GameObject lichPrefab;
    [SerializeField] private GameObject skeletonPrefab;
    [SerializeField] private float shieldInterval = 10f;
    [SerializeField] private float summonInterval = 10f;
    [SerializeField] private float invulnerabilityDuration = 5f;
    [SerializeField] private AudioClip boneShieldSound;
    [SerializeField] private AudioClip summonUndeadSound;
    [SerializeField] private Animation boneShieldAnimation;
    [SerializeField] private Animation summonUndeadAnimation;

    [SerializeField] private GameObject boneShieldPrefab;
    private GameObject boneShieldInstance;
    private Transform necromancer; // Ссылка на некроманта
    private float followSpeed = 1f;
    private AudioSource audioSource;
    private bool isInvulnerable = false;

    private void Awake()
    {
        healthPoints = 4000f;
        armorPoints = 0f;
        damage = 50f;
        costForDeath = 25f;

        moveSpeed = 1f;
        // Получаем компонент AudioSource для воспроизведения звуковых эффектов
        audioSource = GetComponent<AudioSource>();

        Invoke(nameof(ActivateBoneShield), 2f);

        // Запускаем призыв армии мертвецов с заданным интервалом
        InvokeRepeating(nameof(SummonUndeadArmy), 0f, summonInterval);
    }



    public override void TakeDamage(float damage, float armorDecreaseConst)
    {
        if (!isInvulnerable)
        {
            base.TakeDamage(damage, armorDecreaseConst);

            // Меняем количество HP в HealthBar
            if (healthBarSlider != null)
            {
                float currentHealthPercent = 100 * healthPoints / startHealth;
                healthBarSlider.value = currentHealthPercent / 100;
            }
        }
    }

    public void ActivateBoneShield()
    {
        isInvulnerable = true;

        // Создаем костяной щит
        boneShieldInstance = Instantiate(boneShieldPrefab, transform.position, Quaternion.identity);
        // Назначаем некроманта как родителя для костяного щита
        boneShieldInstance.transform.SetParent(transform);
        // Получаем ссылку на компонент Transform некроманта
        necromancer = transform;

        // Воспроизводим звук создания щита
        //audioSource.PlayOneShot(boneShieldSound);
        //// Запускаем анимацию создания щита
        //boneShieldAnimation.Play();
        Debug.Log("immortal");
        Invoke(nameof(DeactivateBoneShield), invulnerabilityDuration);
    }

    private void DeactivateBoneShield()
    {
        isInvulnerable = false;
        Destroy(boneShieldInstance);
        Debug.Log("mortal");
        Invoke(nameof(ActivateBoneShield),shieldInterval);
    }

    private void SummonUndeadArmy()
    {
        // Воспроизводим звук призыва мертвецов
        //audioSource.PlayOneShot(summonUndeadSound);
        //// Запускаем анимацию призыва мертвецов
        //summonUndeadAnimation.Play();
        Instantiate(skeletonPrefab, transform.position, Quaternion.identity);
    }

    public override void Die()
    {
        GameObject deathEffectObject = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        // Перемещаем костяной щит за некромантом
        if (boneShieldInstance != null && necromancer != null)
        {
            boneShieldInstance.transform.position = Vector3.MoveTowards(boneShieldInstance.transform.position, necromancer.position, Time.fixedDeltaTime * followSpeed);
        }
    }
}

