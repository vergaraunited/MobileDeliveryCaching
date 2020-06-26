using MobileDeliveryGeneral.Data;
using MobileDeliveryGeneral.DataManager.Interfaces;
using MobileDeliveryLogger;
using MobileDeliverySettings;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DataCaching.Caching
{
    public class CacheItem<O>  where O : isaCacheItem<O>, System.IComparable<O>, new()
    {
        // readonly SQLiteAsyncConnection _database;
        readonly string dbpath;
        readonly SQLiteConnection _database;

        public CacheItem(string dbPath)
        {
            try
            {
                this.dbpath = dbPath;
                //_database = new SQLiteAsyncConnection(dbPath);
                _database = new SQLiteConnection(dbPath);
                //_database.CreateTableAsync<O>().Wait();
                _database.CreateTable<O>();
            }
            catch (Exception ex) { }

        }

        public void BackupAndClearAll()
        {
            _database.Backup($"{SettingsAPI.TruckCachePath}_bak_{ DateTime.Now.ToString("YYMMdd")}");
            File.Delete(SettingsAPI.TruckCachePath);

            _database.Backup($"{SettingsAPI.OrderCachePath}_bak_{DateTime.Now.ToString("YYMMdd")}");
            File.Delete(SettingsAPI.OrderCachePath);

            _database.Backup($"{SettingsAPI.StopCachePath}_bak_{DateTime.Now.ToString("YYMMdd")}");
            File.Delete(SettingsAPI.StopCachePath);

            _database.Backup($"{SettingsAPI.OrderDetailCachePath}_bak_{DateTime.Now.ToString("YYMMdd")}");
            File.Delete(SettingsAPI.OrderDetailCachePath);
        }

        public List<O> GetItems()
        {
            //return _database.Table<O>().ToListAsync();

            return _database.Table<O>().ToList();
        }
        public List<O> GetItems(O it)
        {
            try
            {
                string sql = "";
                if (typeof(O).Name == "StopData")
                {
                    StopData st = (StopData)(isaCacheItem<O>)it;
                    sql = "select * from " + typeof(O).Name + " where ManifestId=" + st.ManifestId;
                }
                else if (typeof(O).Name == "TruckData")
                {
                    TruckData tr = (TruckData)(isaCacheItem<O>)it;
                    sql = "select * from " + typeof(O).Name + " where " +
                        " ShipDate='" + tr.SHIP_DTE + "'";
                }
                else if (typeof(O).Name == "OrderMasterData")
                {
                    OrderMasterData od = (OrderMasterData)(isaCacheItem<O>)it;
                    sql = "select * from \"" + typeof(O).Name + "\" where " +
                        " ManifestId=" + od.ManId + " AND ORD_NO=" + od.ORD_NO.ToString();
                }
                else if (typeof(O).Name == "OrderOptionsData")
                {
                    OrderOptionsData od = (OrderOptionsData)(isaCacheItem<O>)it;
                    sql = "select * from \"" + typeof(O).Name + "\" where " +
                        " ORD_NO=" + od.ORD_NO + " AND OPT_NUM =" + od.OPT_NUM.ToString() + " AND MDL_NO= " + od.MDL_NO;
                }
                if (sql.Length > 0)
                {
                    List<O> outObj = _database.Query<O>(sql);

                    if (outObj.Count > 0)
                        return outObj;
                }
            }
            catch (Exception ex) { }
            return default(List<O>);
        }
        public TableMapping GetMapping() {
            return _database.GetMapping<O>(CreateFlags.None);
        }
        //public Task<O> GetItemAsync(O it)
        public O GetItem(O it)
        {
            try
            {
                string sql="";
                if (typeof(O).Name == "StopData")
                {
                    if (!FileExists(SettingsAPI.StopCachePath))
                        sql = "";
                    else
                    {

                        StopData st = (StopData)(isaCacheItem<O>)it;
                        sql = "select * from " + typeof(O).Name + " where ManifestId=" + st.ManifestId;
                        if (st.DisplaySeq != 0)
                            sql += " AND DisplaySeq=" + st.DisplaySeq;
                    }
                }
                else if (typeof(O).Name == "TruckData")
                {
                    if (!FileExists(SettingsAPI.StopCachePath))
                        sql = "";
                    else
                    {
                        TruckData tr = (TruckData)(isaCacheItem<O>)it;
                        sql = "select * from " + typeof(O).Name + " where " +
                            " ManifestId=" + tr.ManifestId;
                    }
                }
                else if (typeof(O).Name == "OrderMasterData")
                {
                    if (!FileExists(SettingsAPI.StopCachePath))
                        sql = "";
                    else
                    {
                        OrderMasterData od = (OrderMasterData)(isaCacheItem<O>)it;
                        sql = "select * from " + typeof(O).Name + " where " +
                            " ORD_NO=" + od.ORD_NO;
                    }
                }
                else if (typeof(O).Name == "OrderOptionsData")
                {
                    if (!FileExists(SettingsAPI.StopCachePath))
                        sql = "";
                    else
                    {

                        OrderOptionsData od = (OrderOptionsData)(isaCacheItem<O>)it;
                        sql = "select * from " + typeof(O).Name + " where " +
                            " ORD_NO=" + od.ORD_NO + " AND OPT_NUM=" + od.OPT_NUM + " AND MDL_NO=" + od.MDL_NO;
                    }
                }
                if (sql.Length > 0)
                {
                    if (!FileExists(SettingsAPI.StopCachePath))
                        sql = "";
                    else
                    {
                        List<O> outObj = _database.Query<O>(sql);

                        if (outObj.Count > 0)
                            return outObj[0];
                    }
                }
            }
            catch (Exception ex) { Logger.Debug($"SQLITE Error: {ex.Message}."); }

            return default(O);
        }
        private bool FileExists(string fileName)
        {
            var result = false;
            try
            {
                if (File.Exists(fileName))
                {
                    String sql = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = '{table_name}'";
                    var nm = _database.Query<O>(sql);
                    if (nm == null || nm.Count==0)
                        return false;
                    else
                        return true;
                }
            }
            catch { }
            return result;
        }

        public int SaveItem(O it)
        {
            if (it.Id != 0)
            {
                return _database.Update(it);
            }
            else
            {
                return _database.Insert(it);
            }
        }

        public int DeleteItem(O it)
        {
            return _database.Delete(it);
        }
    }
}
