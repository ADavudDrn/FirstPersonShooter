using UnityEngine;

namespace Interactables
{
    public class ChangeColorCube : Interactable
    {
        [SerializeField]private Color[] Colors;
        
        private MeshRenderer _meshRenderer;

        private int _colorIndex;
        
        void Start()
        {
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

            _meshRenderer.material.color = Colors[_colorIndex];
        }
    }
}
