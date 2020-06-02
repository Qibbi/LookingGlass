
#define WIN32_LEAN_AND_MEAN
#include <Windows.h>
#include "loadnet.h"

#pragma unmanaged
BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        // ShutdownCoreCLR();
        break;
    }
    return TRUE;
}

