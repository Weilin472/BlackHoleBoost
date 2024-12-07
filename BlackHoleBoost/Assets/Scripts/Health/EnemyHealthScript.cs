using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : BaseHealthScript
{
    private VFXInstantiate _vfxInstantiate;

    private bool isBeingDamaged;
    private void Awake()
    {
        _vfxInstantiate = GetComponent<VFXInstantiate>();
        ResetHealth();
    }

    protected override void OnDeath()
    {
        _vfxInstantiate.SpawnVFX(transform.position);
        Destroy(gameObject);
    }

    public override void Damage(int damage)
    {
        if (!_invincible)
        {
            if (_damageSFX != null)
            {
                AudioManager.Instance.PlaySFX(_damageSFX);
            }
            _currentHealth -= damage;
            
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                OnDeath();
                return;
            }
            if (!isBeingDamaged)
            {
                StartCoroutine(DamageIndicator());
            }
        }
    }

    private IEnumerator DamageIndicator()
    {
        isBeingDamaged = true;
        Dictionary<MeshRenderer, Color> originalColor = new Dictionary<MeshRenderer, Color>();
        MeshRenderer[] renderer = transform.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < renderer.Length; i++)
        {
            originalColor.Add(renderer[i], renderer[i].material.color);
            renderer[i].material.color = Color.red;
        }
        yield return new WaitForSeconds(0.5f);
        foreach (var item in originalColor)
        {
            item.Key.material.color = item.Value;
        }
        isBeingDamaged = false;
    }
}
