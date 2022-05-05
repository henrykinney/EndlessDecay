using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float CurrentHealth;
    public float MaxHealth;
    public bool IsDead;
    public UnityEvent OnHealthChanged;
    public UnityEvent OnDeath;
    
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        IsDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float amount) {
        AddHealth(-amount);
    }
    public void AddHealth(float value) {
        if (!IsDead) {
            CurrentHealth += value;
            OnHealthChanged.Invoke();
            if (CurrentHealth <= 0) {
                IsDead = true;
                OnDeath.Invoke();
            }
        }
    }
    public void AddMaxHealth(float value) {
        MaxHealth += value;
        AddHealth(value);
    }
    public void Revive() {
        IsDead = false;
        CurrentHealth = MaxHealth;
        OnHealthChanged.Invoke();
    }
}
