using System;
using UnityEngine;

namespace Excercise1
{
    public class Character : MonoBehaviour, ICharacter
    {
        [SerializeField] protected string id;

        protected virtual void OnEnable()
        {
            if (!string.IsNullOrEmpty(id) && CharacterService.Instance != null)
                CharacterService.Instance.TryAddCharacter(id, this);
        }

        protected virtual void OnDisable()
        {
            if (!string.IsNullOrEmpty(id) && CharacterService.Instance != null)
                CharacterService.Instance.TryRemoveCharacter(id);
        }
    }
}