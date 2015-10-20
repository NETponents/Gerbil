#include <iostream>

public static int main()
{
  std::cout << "Gerbil v0.1 Alpha\n";
  std::cout << "Copyright 2015 GPL V3 License\n\n";
  std::cout << "Beginning loading process\n";
  if(dirExists("./store"))
  {
    std::cout << "Found ./store\n";
  }
  else
  {
    std::cout << "ERROR: ./store not found\n";
    return 1;
  }
  return 0;
}
int dirExists(const char *path)
{
  struct stat info;

  if(stat( path, &info ) != 0)
    return 0;
  else if(info.st_mode & S_IFDIR)
    return 1;
  else
    return 0;
}
