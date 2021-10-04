using FriendsGamesTools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class EffectsList
{
    private List<ParticleSystem> list;

    [SerializeField]
    private ParticleSystem prefab;
    [SerializeField]
    private Transform parent;
    
    public void Clear()
    {
        if (list == null) list = new List<ParticleSystem>();

        foreach (var effect in list)
        {
            GameObject.Destroy(effect.gameObject);
        }
        list.Clear();
    }
    public ParticleSystem PlayEffect(Vector3 position)
    {
        if (list == null) list = new List<ParticleSystem>();

        var effect = list.Find(x => x!=null && !x.gameObject.activeSelf);
        if (!effect)
        {
            effect= GameObject.Instantiate(prefab, parent) ;
            list.Add(effect);
        }
        effect.transform.position = position;
        effect.gameObject.SetActive(true);
     
        return effect;
    }
    
}
public class EffectsManager : MonoBehaviourHasInstance<EffectsManager>
{
    [SerializeField] private EffectsList defaultEffect;

    private ParticleSystem PlayEffect(EffectsList effect, Vector3 position)
    {
        return effect.PlayEffect(position);
    }
}