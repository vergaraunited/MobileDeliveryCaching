using DataCaching.Data;
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
                this.dbpath = dbpath;
                //_database = new SQLiteAsyncConnection(dbPath);
                _database = new SQLiteConnection(dbPath);
                //_database.CreateTableAsync<O>().Wait();
                _database.CreateTable<O>();
            }
            catch (Exception ex) { }

        }

        public void BackupAndClearAll()
        {
            _database.Backup($"{Settings.TruckCachePath}_bak_{ DateTime.Now.ToString("YYMMdd")}");
            File.Delete(Settings.TruckCachePath);

            _database.Backup($"{Settings.OrderCachePath}_bak_{DateTime.Now.ToString("YYMMdd")}");
            File.Delete(Settings.OrderCachePath);

            _database.Backup($"{Settings.StopCachePath}_bak_{DateTime.Now.ToString("YYMMdd")}");
            File.Delete(Settings.StopCachePath);

            _database.Backup($"{Settings.OrderDetailCachePath}_bak_{DateTime.Now.ToString("YYMMdd")}");
            File.Delete(Settings.OrderDetailCachePath);
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
                if (typeof(O).Name == "Stop")
                {
                    Stop st = (Stop)(isaCacheItem<O>)it;
                    sql = "select * from " + typeof(O).Name + " where ManifestId=" + st.ManifestId;
                }
                else if (typeof(O).Name == "Truck")
                {
                    Truck tr = (Truck)(isaCacheItem<O>)it;
                    sql = "select * from " + typeof(O).Name + " where " +
                        " ShipDate='" + tr.ShipDate + "'";
                }
                else if (typeof(O).Name == "Order")
                {
                    Order od = (Order)(isaCacheItem<O>)it;
                    sql = "select * from \"" + typeof(O).Name + "\" where " +
                        " ManifestId=" + od.ManifestId + " AND DSP_SEQ=" + od.DSP_SEQ.ToString();
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
                if (typeof(O).Name == "Stop")
                {
                    Stop st = (Stop)(isaCacheItem<O>)it;
                    sql = "select * from " + typeof(O).Name + " where ManifestId=" + st.ManifestId;
                    if (st.DisplaySeq !=0)
                        sql += " AND DisplaySeq=" + st.DisplaySeq;
                }
                else if (typeof(O).Name == "Truck")
                {
                    Truck tr = (Truck)(isaCacheItem<O>)it;
                    sql = "select * from " + typeof(O).Name + " where " +
                        " ManifestId=" + tr.ManifestId;
                }
                else if (typeof(O).Name == "Order")
                {
                    Order od = (Order)(isaCacheItem<O>)it;
                    sql = "select * from " + typeof(O).Name + " where " +
                        " DSP_SEQ=" + od.DSP_SEQ + " AND ORD_NO=" + od.ORD_NO + " AND MDL_NO=" + od.MDL_NO;
                }
                if (sql.Length > 0)
                {
                    List<O> outObj = _database.Query<O>(sql);

                    if (outObj.Count >0)
                        return outObj[0];
                }
            }
            catch (Exception ex) { }

            return default(O);
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
