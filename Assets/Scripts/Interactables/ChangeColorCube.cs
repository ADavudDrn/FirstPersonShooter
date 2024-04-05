using Photon.Pun;
using UnityEngine;

namespace Interactables
{
    public class ChangeColorCube : Interactable
    {
        [SerializeField]private Color[] Colors;
        
        private MeshRenderer _meshRenderer;

        private int _colorIndex;
        
        protected override void Start()
        {
            base.Start();
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.material.color = Color.red;
            
        }

        
        protected override void Interact()
        {
            _colorIndex++;
            if (_colorIndex > Colors.Length - 1)
            {
                _colorIndex = 0;
            }
            
            _photonView.RPC ("ChangeColor", RpcTarget.AllBufferedViaServer, _colorIndex);
            
        }
        [PunRPC]
        private void ChangeColor(int colorIndex)
        {
            _meshRenderer.material.color = Colors[colorIndex];
        }
    }
}
