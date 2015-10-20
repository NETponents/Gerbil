using System;

namespace Gerbil
{
  class Gerbil_Config
  {
    Dictionary<string,string> configStore;
    
    public Gerbil_Config()
    {
      configStore = new Dictionary<string,string>();
    }
    public int loadConfig(string filepath)
    {
      
    }
    private void storeField(string id, string value)
    {
      configStore.Add(id, value);
    }
    public string getField(string id)
    {
      return configStore.Get(id);
    }
  }
}
