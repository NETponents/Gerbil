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
    public void loadConfig(string filepath)
    {
      string[] lines = System.IO.File.ReadAllLines(filepath);
      foreach(string line in lines)
      {
        string[] args = line.split("=");
        configStore.Add(args[0], args[1]);
      }
    }
    public void reloadConfig(string filepath)
    {
      string[] lines = System.IO.File.ReadAllLines(filepath);
      foreach(string line in lines)
      {
        string[] args = line.split("=");
        if(configStore.ContainsKey(args[0]))
        {
          configStore[args[0]] = args[1];
        }
        else
        {
          configStore.Add(args[0], args[1]);
        }
      }
    }
    public void commitConfig(string filepath)
    {
      List<string> tempFile = new List<string>();
      foreach(KeyValuePair<string,string> sets in configStore)
      {
        tempFile.add(sets.Key + "=" + sets.Value);
      }
      //TODO: commit tempFile to actual settings file
      this.reloadConfig(filepath);
    }
    public string getField(string id)
    {
      if(configStore.ContainsKey(id))
      {
        return configStore[id];
      }
      return "default";
    }
  }
}
