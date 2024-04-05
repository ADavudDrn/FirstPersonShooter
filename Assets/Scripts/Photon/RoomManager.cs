using Photon.Pun;
using Player;
using UnityEngine;
using Utility;

namespace Photon
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {
        public GameObject Player;
        [Space] public Transform SpawnPoint;
        void Start()
        {
            EditorDebug.Log("Connecting...");

            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
        
            EditorDebug.Log("Connected to Server");

            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
        
            EditorDebug.Log("We're in the Lobby");

            PhotonNetwork.JoinOrCreateRoom("test", null, null);
        
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
        
            EditorDebug.Log("We're connected and in a room now");
        
            GameObject player = PhotonNetwork.Instantiate(Player.name, SpawnPoint.position, Quaternion.identity);
            player.GetComponent<PlayerSetup>().IsLocalPlayer();
        }
    }
}
