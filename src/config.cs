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
      string[] lines = System.IO.File.ReadAllLines(filepath);
      foreach(string line in lines)
      {
        string[] args = line.split("=");
        this.storeField(args[0], args[1]);
      }
    }
    private void storeField(string id, string value)
    {
      configStore.Add(id, value);
    }
    public string getField(string id)
    {
      if(configStore.ContainsKey(id))
      {
        return configStore.Get(id);
      }
      return "default";
    }
  }
}
