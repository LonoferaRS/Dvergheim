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
    private Transform necromancer; // ������ �� ����������
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
        // �������� ��������� AudioSource ��� ��������������� �������� ��������
        audioSource = GetComponent<AudioSource>();

        Invoke(nameof(ActivateBoneShield), 2f);

        // ��������� ������ ����� ��������� � �������� ����������
        InvokeRepeating(nameof(SummonUndeadArmy), 0f, summonInterval);
    }



    public override void TakeDamage(float damage, float armorDecreaseConst)
    {
        if (!isInvulnerable)
        {
            base.TakeDamage(damage, armorDecreaseConst);

            // ������ ���������� HP � HealthBar
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

        // ������� �������� ���
        boneShieldInstance = Instantiate(boneShieldPrefab, transform.position, Quaternion.identity);
        // ��������� ���������� ��� �������� ��� ��������� ����
        boneShieldInstance.transform.SetParent(transform);
        // �������� ������ �� ��������� Transform ����������
        necromancer = transform;

        // ������������� ���� �������� ����
        //audioSource.PlayOneShot(boneShieldSound);
        //// ��������� �������� �������� ����
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
        // ������������� ���� ������� ���������
        //audioSource.PlayOneShot(summonUndeadSound);
        //// ��������� �������� ������� ���������
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
        // ���������� �������� ��� �� �����������
        if (boneShieldInstance != null && necromancer != null)
        {
            boneShieldInstance.transform.position = Vector3.MoveTowards(boneShieldInstance.transform.position, necromancer.position, Time.fixedDeltaTime * followSpeed);
        }
    }
}

