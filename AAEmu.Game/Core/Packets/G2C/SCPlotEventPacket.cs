using AAEmu.Commons.Network;
using AAEmu.Commons.Utils;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Models.Game.Char.Templates;
using AAEmu.Game.Models.Game.Units;
using AAEmu.Game.Utils;

namespace AAEmu.Game.Core.Packets.G2C
{
    public class SCPlotEventPacket : GamePacket
    {
        private readonly Unit _caster;
        private readonly ushort _tl;
        private readonly uint _eventId;
        private readonly uint _skillId;
        private readonly uint _casterId;
        private readonly uint _targetId;
        private readonly uint _unkId;
        private readonly ushort _castingTime;
        private byte _flag;
        private readonly byte _type;


        public SCPlotEventPacket(Unit caster, ushort tl, uint eventId, uint skillId, uint casterId, uint targetId, uint unkId, ushort castingTime, byte flag, int stepEventPosition)
            : base(SCOffsets.SCPlotEventPacket, 1)
        {
            _caster = caster;
            _tl = tl;
            _eventId = eventId;
            _skillId = skillId;
            _casterId = casterId;
            _targetId = targetId;
            _unkId = unkId;
            _castingTime = castingTime;
            _flag = flag;
            _type = (byte)stepEventPosition >= 2 ? (byte)2 : (byte)1;
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(_tl);
            stream.Write(_eventId); // type(id), eventId?
            stream.Write(_skillId);
            // --------------------------------------------
            //var type = _caster.CurrentTarget == null ? (byte)1 : (byte)2;
            stream.Write((byte)1); // type
            switch (1)
            {
                case 1:
                    stream.WriteBc(_casterId);
                    break;
                case 2:
                    stream.WritePosition(_caster.Position.X, _caster.Position.Y, _caster.Position.Z);
                    stream.Write((sbyte)0); // rot.x
                    stream.Write((sbyte)0); // rot.y
                    stream.Write((sbyte)0); // rot.z
                    break;
            }

            // --------------------------------------------
            stream.Write((byte)_type); // type
            switch (_type)
            {
                case 1:
                    stream.WriteBc(_targetId);
                    break;
                case 2:

                    //бросок камня на расстояние вперед нас на 20 метров
                    var (newX, newY) = MathUtil.AddDistanceToFront(20, _caster.Position.X, _caster.Position.Y, _caster.Position.RotationZ); //TODO расстояние броска 20 м
                    var positionX = newX;
                    var positionY = newY;

                    stream.WritePosition(positionX, positionY, _caster.Position.Z);
                    stream.Write((sbyte)0); // rot.x
                    stream.Write((sbyte)0); // rot.y
                    stream.Write((sbyte)_caster.Position.RotationZ); // rot.z
                    break;
            }

            // --------------------------------------------
            stream.Write(0L); // itemId
            stream.WriteBc(_unkId);
            stream.Write(_castingTime); // msec, castingTime / 10
            stream.WriteBc(0);
            stream.Write((short)0); // msec

            var targetUnitCount = _type == 1 ? (byte)1 : (byte)0; // TODO

            stream.Write((byte)targetUnitCount); // targetUnitCount // TODO if aoe, list of units

            // TODO targetUnitCount > 0 -> do->while() stream.WriteBc(0);
            for (var i = 0; i < targetUnitCount; i++)
            {
                stream.WriteBc(_targetId);
            }

            _flag = _type == 1 ? (byte)_flag : (byte)6; // TODO

            stream.Write(_flag);

            if (((_flag >> 3) & 1) == 1) // 8
            {
                for (var i = 0; i < 13; i++)
                    stream.Write(0); // v
            }

            return stream;
        }
    }
}
