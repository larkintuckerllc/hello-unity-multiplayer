using UnityEngine;


namespace Com.Larkintuckerllc.HelloMultiplayer
{
    public class Launcher : Photon.PunBehaviour
    {
        public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;

        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        public byte MaxPlayersPerRoom = 4;

        string _gameVersion = "1";

        void Awake()
        {
            PhotonNetwork.autoJoinLobby = false;
            PhotonNetwork.automaticallySyncScene = true;
            PhotonNetwork.logLevel = Loglevel;
        }

        void Start()
        {
            Connect();
        }

        void Connect()
        {
            if (PhotonNetwork.connected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings(_gameVersion);
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
            PhotonNetwork.JoinRandomRoom();
        }


        public override void OnDisconnectedFromPhoton()
        {
            Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            Debug.Log("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        }

    }
}