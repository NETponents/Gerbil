using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

  namespace Gerbil
  {
    namespace Gerbil_PasswordServices
    {
      public class SimplePasswordCracker
      {
        private int charCtr = 31;
        private int maxCrackLength;
        private char[] passwordStorage = new char[1];
        public SimpleAuthCracker(int maxLength)
        {
          // Default constructor
          passwordStorage[0] = 31;
          maxCrackLength = maxLength;
        }
        public string getNextKey()
        {
          passwordStorage.increment();
          if(passwordStorage > maxCrackLength)
          {
            throw new PasswordTableExhaustedException();
          }
          return passwordStorage.ToString();
        }
        private void increment()
        {
          charCtr = (char)((int)charCtr + 1);
          passwordStorage[passwordStorage.Length - 1] = charCtr;
          for(int i = passwordStorage.Length - 1; i >= 0; i--)
          {
            if((int)passwordStorage[i] > 126)
            {
              passwordStorage[i] = 32;
              if(i > 0)
              {
                passwordStorage[i - 1] = (char)((int)passwordStorage[i - 1] + 1);
              }
              else
              {
                extendStringLength();
              }
            }
          }
        }
        private void extendStringLength()
        {
          passwordStorage = new char[passwordStorage.Length + 1];
          for(int i = 0; i < passwordStorage.length; i++)
          {
            passwordStorage[i] = (char)(32);
          }
        }
      }
      public class PasswordTableExhaustedException : Exception
      {
        
      }
    }
  }
