using Lean.Pool;
using Photon.Pun;
using UnityEngine;

namespace Interactables
{
    public class CollectCube : Interactable
    {
        [SerializeField] private ParticleSystem Particle;
        
        protected override void Interact()
        {
            _photonView.RPC ("Collect", RpcTarget.AllBufferedViaServer, true);
        }
        [PunRPC]
        private void Collect(bool flag)
        {
            if(Particle)
                LeanPool.Spawn(Particle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
