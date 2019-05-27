using AAEmu.Game.Core.Network.Connections;

namespace AAEmu.Game.Models.Tasks.Duels
{
    public abstract class DuelFuncTask : Task
    {
        protected GameConnection _connection;
        protected uint _challengerId;
        protected uint _challengedId;


        protected DuelFuncTask(GameConnection connection, uint challengerId, uint challengedId)
        {
            _connection = connection;
            _challengerId = challengerId;
            _challengedId = challengedId;
        }
    }
}
