using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ModifierBank
{
    class Modifier {
        public Dictionary<string, float> values;

        public void AddValue(string k, float v) {
            if (k == null) {
                k = "";
            }
            float existing;
            if (values.TryGetValue(k, out existing)) {
                values[k] = v;
            }
            else {
                values.Add(k, v);
            }
        }
        public void RemoveValue(string k) {
            float existing;
            if (values.TryGetValue(k, out existing)) {
                values.Remove(k);
            }
        }
        public float GetObjectMult() {
            float total = 1;
            foreach(KeyValuePair<string, float> entry in values)
            {
                total *= entry.Value;
            }
            return total;
        }
    }
    Dictionary<GameObject, Modifier> modifiers = new Dictionary<GameObject, Modifier>();
    
    public void AddModifier(float value, GameObject source, string key) {
        Modifier mod;
        if (modifiers.TryGetValue(source, out mod)) {
            mod.AddValue(key, value);
        }
        else {
            mod = new Modifier();
            mod.values = new Dictionary<string, float>();
            mod.AddValue(key, value);
            modifiers.Add(source, mod);
        }
        
    }
    public void RemoveModifier(GameObject source, string key) {
        Modifier mod;
        if (modifiers.TryGetValue(source, out mod)) {
            if (key == null) {
                modifiers.Remove(source);
            }
            else {
                mod.RemoveValue(key);
            }
        }
    }
    
    public float GetMult() {
        float result = 1;
        List<GameObject> KeysToDelete = new List<GameObject>();
        foreach(KeyValuePair<GameObject, Modifier> entry in modifiers) {
            if (entry.Key == null) {
                KeysToDelete.Add(entry.Key);
            } else {
                result *= entry.Value.GetObjectMult();
            }
        }
        foreach(GameObject k in KeysToDelete) {
            modifiers.Remove(k);
        }
        return result;
    }
}
