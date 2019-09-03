using System;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Models.Tasks.LaborPower;
using AAEmu.Game.Utils.DB;
using MySql.Data.MySqlClient;
using NLog;

namespace AAEmu.Game.Models.Game.LaborPower
{
    public class LaborPower
    {
        /*
         *
         */

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public uint Id { get; set; }
        public uint AccountId { get; set; }
        public short Lp { get; set; }
        public DateTime LaborPowerModified { get; set; }
        public int ConsumedLaborPower { get; set; }
        public DateTime Updated { get; set; }
        public LaborPowerTickStartTask LpTickStartTask { get; set; }
        public bool IsStarted { get; set; } = false;

        private const short LpChangePremium = 10; // TODO in config
        private const short LpChange = 5;
        private const short LpMax = 2000;

        public LaborPower()
        {
        }

        public void ChangeLabor(short change, int actabilityId)
        {
            //var actabilityChange = 0;
            //byte actabilityStep = 0;
            //if (actabilityId > 0)
            //{
            //    actabilityChange = Math.Abs(change);
            //    actabilityStep = Actability.Actabilities[(uint)actabilityId].Step;
            //    Actability.AddPoint((uint)actabilityId, actabilityChange);
            //}

            //Lp += change;
            //SendPacket(new SCCharacterLaborPowerChangedPacket(change, actabilityId, actabilityChange, actabilityStep));
        }

        #region Database

        public static LaborPower Load(MySqlConnection connection, uint characterId)
        {
            var character = new LaborPower();

            using (var command = connection.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = "SELECT * FROM characters WHERE `id` = @id";
                command.Parameters.AddWithValue("@id", characterId);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        character.Id = reader.GetUInt32("id");
                        character.AccountId = reader.GetUInt32("account_id");
                        character.Lp = reader.GetInt16("labor_power");
                        character.LaborPowerModified = reader.GetDateTime("labor_power_modified");
                        character.ConsumedLaborPower = reader.GetInt32("consumed_lp");
                        character.Updated = reader.GetDateTime("updated_at");
                    }
                }
            }

            return character;
        }

        public bool Save()
        {
            bool result;
            try
            {
                using (var connection = MySQL.CreateConnection())
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.Connection = connection;
                            command.Transaction = transaction;
                            command.CommandText =
                                "REPLACE INTO `characters` " +
                                "(`id`,`account_id`,`labor_power`,`labor_power_modified`,`consumed_lp`,`updated_at`) " +
                                "VALUES(@id,@account_id,@labor_power,@labor_power_modified,@consumed_lp,@updated_at)";

                            command.Parameters.AddWithValue("@id", Id);
                            command.Parameters.AddWithValue("@account_id", AccountId);
                            command.Parameters.AddWithValue("@labor_power", Lp);
                            command.Parameters.AddWithValue("@labor_power_modified", LaborPowerModified);
                            command.Parameters.AddWithValue("@consumed_lp", ConsumedLaborPower);
                            command.Parameters.AddWithValue("@updated_at", Updated);
                            command.ExecuteNonQuery();
                        }

                        try
                        {
                            transaction.Commit();
                            result = true;
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex);

                            try
                            {
                                transaction.Rollback();
                            }
                            catch (Exception ex2)
                            {
                                Log.Error(ex2);
                            }

                            result = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result = false;
            }

            return result;
        }

        #endregion

        public void SendPacket(GamePacket packet)
        {
            //Connection?.SendPacket(packet);
        }

        public void BroadcastPacket(GamePacket packet, bool self)
        {
            //foreach (var character in WorldManager.Instance.GetAround<Character>(this))
            //    character.SendPacket(packet);
            //if (self)
            //    SendPacket(packet);
        }
    }
}
