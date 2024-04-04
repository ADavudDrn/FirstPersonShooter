using Lean.Pool;
using UnityEngine;

namespace Interactables
{
    public class ColLectCube : Interactable
    {
        [SerializeField] private ParticleSystem Particle;
        protected override void Interact()
        {
            if(Particle)
                LeanPool.Spawn(Particle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
