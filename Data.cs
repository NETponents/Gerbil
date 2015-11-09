using System;
using System.Collections.Generic;

namespace Gerbil
{
    namespace Data
    {
        namespace Models
        {
            public partial class Model
            {
                public Model()
                {

                }
                public virtual DataActionResult Read()
                {
                    return new DataActionResult(DataResult.unknown);
                }
                public virtual DataActionResult Update()
                {
                    return new DataActionResult(DataResult.unknown);
                }
            }
        }
        public class Database<T>
        {
            public int itemcount
            {
                get
                {
                    return rowList.Count;
                }
            }
            Random rd = new Random();
            string dbName;
            Dictionary<string, T> rowList = new Dictionary<string, T>();
            public Database(string name)
            {
                dbName = name;
            }
            public DataActionResult Create(T item)
            {
                string newID;
                do
                {
                    newID = rd.Next(100000, 999999).ToString();
                } while (ContainsID(newID));
                try
                {
                    rowList.Add(newID, item);
                }
                catch
                {
                    return new DataActionResult(DataResult.failed);
                }
                return new DataActionResult(DataResult.success, newID);
            }
            public T Read(string id)
            {
                if(!ContainsID(id))
                {
                    //TODO: use specific exception
                    throw new Exception();
                }
                return rowList[id];
            }
            public DataActionResult Update(string id, T item)
            {
                if (!ContainsID(id))
                {
                    //TODO: use specific exception
                    throw new Exception();
                }
                try
                {
                    rowList[id] = item;
                }
                catch
                {
                    return new DataActionResult(DataResult.failed, id);
                }
                return new DataActionResult(DataResult.success, id);
            }
            public DataActionResult Delete(string id)
            {
                if (!ContainsID(id))
                {
                    //TODO: use specific exception
                    throw new Exception();
                }
                try
                {
                    rowList.Remove(id);
                }
                catch
                {
                    return new DataActionResult(DataResult.failed, id);
                }
                return new DataActionResult(DataResult.success, id);
            }
            public string[] getAllIDs()
            {
                List<string> resultarray = new List<string>();
                foreach (KeyValuePair<string, T> i in rowList)
                {
                    resultarray.Add(i.Key);
                }
                return resultarray.ToArray();
            }
            public bool ContainsID(string id)
            {
                if(rowList.ContainsKey(id))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public class DataActionResult
        {
            private DataResult _DR;
            private string idUsed;

            public DataActionResult()
            {
                _DR = DataResult.unknown;
                idUsed = "";
            }
            public DataActionResult(DataResult DR)
            {
                _DR = DR;
                idUsed = "";
            }
            public DataActionResult(DataResult DR, string id)
            {
                _DR = DR;
                idUsed = id;
            }
            public DataResult getResult()
            {
                return _DR;
            }
            public string getItemID()
            {
                return idUsed;
            }
        }
        public enum DataResult
        {
            unknown,
            failed,
            success
        };
    }
}
