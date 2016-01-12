// Gerbil.cpp : Defines the entry point for the console application.
//

#include <iostream>
#include <tchar.h>

// Entry point of the program
int _tmain(int argc, _TCHAR* argv[])
{
	std::cout << "-";
	// Start up networking

	std::cout << "-";
	// Wait for BRCS service

#ifdef DEBUG
	std::cout << "Press enter to quit...";
	std::cin;
#endif
	return 0;
}

