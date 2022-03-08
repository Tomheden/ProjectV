using BeardedManStudios.Forge.Networking.Frame;
using System;
using MainThreadManager = BeardedManStudios.Forge.Networking.Unity.MainThreadManager;

namespace BeardedManStudios.Forge.Networking.Generated
{
	public partial class NetworkObjectFactory : NetworkObjectFactoryBase
	{
		public override void NetworkCreateObject(NetWorker networker, int identity, uint id, FrameStream frame, Action<NetworkObject> callback)
		{
			if (networker.IsServer)
			{
				if (frame.Sender != null && frame.Sender != networker.Me)
				{
					if (!ValidateCreateRequest(networker, identity, id, frame))
						return;
				}
			}
			
			bool availableCallback = false;
			NetworkObject obj = null;
			MainThreadManager.Run(() =>
			{
				switch (identity)
				{
					case ChatManagerNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new ChatManagerNetworkObject(networker, id, frame);
						break;
					case CubeForgeGameNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new CubeForgeGameNetworkObject(networker, id, frame);
						break;
					case ExampleProximityPlayerNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new ExampleProximityPlayerNetworkObject(networker, id, frame);
						break;
					case NetworkCameraNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new NetworkCameraNetworkObject(networker, id, frame);
						break;
					case TestNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new TestNetworkObject(networker, id, frame);
						break;
					case XCamaraNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new XCamaraNetworkObject(networker, id, frame);
						break;
					case XGestorVidaNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new XGestorVidaNetworkObject(networker, id, frame);
						break;
					case XKillsNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new XKillsNetworkObject(networker, id, frame);
						break;
					case XListaJugadoresNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new XListaJugadoresNetworkObject(networker, id, frame);
						break;
					case XLogicaNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new XLogicaNetworkObject(networker, id, frame);
						break;
					case XMovimientoNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new XMovimientoNetworkObject(networker, id, frame);
						break;
					case XRifleNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new XRifleNetworkObject(networker, id, frame);
						break;
				}

				if (!availableCallback)
					base.NetworkCreateObject(networker, identity, id, frame, callback);
				else if (callback != null)
					callback(obj);
			});
		}

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}