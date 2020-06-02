
#include <iostream>
#include <windows.h>

#include "..\LookingGlass\loadnet.h"


typedef int (*loadcoreclrptr)();

int main(int argc, char* argv[])
{
    HMODULE module = LoadLibraryA("LookingGlass");
    loadcoreclrptr loadcoreclr = (loadcoreclrptr)GetProcAddress(module, "LoadCoreCLR");
    loadcoreclr();
    // ShutdownCoreCLR();
    FreeLibrary(module);
}