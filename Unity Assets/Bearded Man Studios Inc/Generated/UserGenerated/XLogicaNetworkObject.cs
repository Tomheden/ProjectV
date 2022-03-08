using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0]")]
	public partial class XLogicaNetworkObject : NetworkObject
	{
		public const int IDENTITY = 13;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private int _i_spawn;
		public event FieldEvent<int> i_spawnChanged;
		public Interpolated<int> i_spawnInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int i_spawn
		{
			get { return _i_spawn; }
			set
			{
				// Don't do anything if the value is the same
				if (_i_spawn == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_i_spawn = value;
				hasDirtyFields = true;
			}
		}

		public void Seti_spawnDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_i_spawn(ulong timestep)
		{
			if (i_spawnChanged != null) i_spawnChanged(_i_spawn, timestep);
			if (fieldAltered != null) fieldAltered("i_spawn", _i_spawn, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			i_spawnInterpolation.current = i_spawnInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _i_spawn);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_i_spawn = UnityObjectMapper.Instance.Map<int>(payload);
			i_spawnInterpolation.current = _i_spawn;
			i_spawnInterpolation.target = _i_spawn;
			RunChange_i_spawn(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _i_spawn);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (i_spawnInterpolation.Enabled)
				{
					i_spawnInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					i_spawnInterpolation.Timestep = timestep;
				}
				else
				{
					_i_spawn = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_i_spawn(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (i_spawnInterpolation.Enabled && !i_spawnInterpolation.current.UnityNear(i_spawnInterpolation.target, 0.0015f))
			{
				_i_spawn = (int)i_spawnInterpolation.Interpolate();
				//RunChange_i_spawn(i_spawnInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public XLogicaNetworkObject() : base() { Initialize(); }
		public XLogicaNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public XLogicaNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
