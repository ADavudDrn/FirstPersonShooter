using Photon.Pun;
using Player;
using UnityEngine;
using Utility;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        public int Damage;
        public float FireRate;

        public Camera Camera;
        private float _nextFire;

        private InputManager _inputManager;

        [Header("VFX")] [SerializeField] private GameObject HitVFX;
        
        void Awake()
        {
            _inputManager = GetComponentInParent<InputManager>();
        }

        private void Update()
        {
            if (_nextFire > 0)
            {
                _nextFire -= Time.deltaTime;
            }
            if (_inputManager.OnFoot.Fire.inProgress && _nextFire <= 0)
            {
                _nextFire = 1/FireRate;
                
                Fire();
                
            }
        }

        private void Fire()
        {
            Ray ray = new Ray(Camera.transform.position, Camera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f))
            {
                PhotonNetwork.Instantiate(HitVFX.name, hit.point, Quaternion.identity);
                if (hit.transform.TryGetComponent(out PlayerHealth health))
                {
                    if (hit.transform.TryGetComponent(out PhotonView pView))
                    {
                        pView.RPC("TakeDamage", RpcTarget.All, Damage);
                    }
                }
            }
        }
    }
}