// TestCppCoreHost.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <windows.h>

// #include <stdio.h>
// #include "..\LookingGlass\framework.h"
#include "..\LookingGlass\loadnet.h"
// #include "..\LookingGlass\coreclrhost.h"
// #include <string>
// 
// #define FS_SEPARATOR "\\"
// #define PATH_DELIMITER ";"
// 
// typedef unsigned int uint;
// 
// // path to the usual install dir
// static LPCSTR coreCLRInstallDirectory = "%programfiles(x86)%\\dotnet\\shared\\Microsoft.NETCore.App\\3.1.4";
// 
// static LPCSTR coreCLRDll = "coreclr.dll";
// 
// HMODULE LoadCoreCLR(LPCSTR directoryPath)
// {
//     std::string coreDllPath(directoryPath);
//     coreDllPath.append(FS_SEPARATOR);
//     coreDllPath.append(coreCLRDll);
//     HMODULE result = LoadLibraryExA(coreDllPath.c_str(), NULL, 0);
//     if (!result)
//     {
//         DWORD errorCode = GetLastError();
//         printf("CoreCLR not loaded from %s. Error code: %d\n", coreDllPath.c_str(), errorCode);
//     }
//     return result;
// }
// 
// typedef void (*managed_direct_method_ptr)();
// 
// HMODULE g_coreCLRModule;
// void* g_runtimeHost;
// uint g_domainId;
// managed_direct_method_ptr g_managedMethod;
// 
// void BuildTPAList(LPCSTR directory, LPCSTR extension, std::string& tpaList)
// {
//     std::string searchPath(directory);
//     searchPath.append(FS_SEPARATOR);
//     searchPath.append("*");
//     searchPath.append(extension);
//     WIN32_FIND_DATAA findData;
//     HANDLE fileHandle = FindFirstFileA(searchPath.c_str(), &findData);
//     if (fileHandle != INVALID_HANDLE_VALUE)
//     {
//         do
//         {
//             tpaList.append(directory);
//             tpaList.append(FS_SEPARATOR);
//             tpaList.append(findData.cFileName);
//             tpaList.append(PATH_DELIMITER);
//         } while (FindNextFileA(fileHandle, &findData));
//         FindClose(fileHandle);
//     }
// }
// 
// extern "C" __declspec(dllexport) int LoadCoreCLR()
// {
//     char currentDirectory[MAX_PATH];
//     GetModuleFileNameA(NULL, currentDirectory, MAX_PATH);
//     size_t idz = strnlen(currentDirectory, MAX_PATH);
//     while (idz > 0 && currentDirectory[idz - 1] != '\\')
//     {
//         --idz;
//     }
//     if (idz > 0)
//     {
//         currentDirectory[idz - 1] = '\0';
//     }
// 
//     char coreRoot[MAX_PATH];
//     size_t outSize;
//     getenv_s(&outSize, coreRoot, MAX_PATH, "CORE_ROOT");
//     g_coreCLRModule = LoadCoreCLR(coreRoot);
//     if (!g_coreCLRModule)
//     {
//         g_coreCLRModule = LoadCoreCLR(currentDirectory);
//     }
//     if (!g_coreCLRModule)
//     {
//         ::ExpandEnvironmentStringsA(coreCLRInstallDirectory, coreRoot, MAX_PATH);
//         g_coreCLRModule = LoadCoreCLR(coreRoot);
//     }
//     if (!g_coreCLRModule)
//     {
//         return -1; // CoreCLR.dll not found
//     }
//     coreclr_initialize_ptr initializeCoreClr = (coreclr_initialize_ptr)GetProcAddress(g_coreCLRModule, "coreclr_initialize");
//     coreclr_create_delegate_ptr createManagedDelegate = (coreclr_create_delegate_ptr)GetProcAddress(g_coreCLRModule, "coreclr_create_delegate");
// 
//     std::string tpaList;
//     BuildTPAList(currentDirectory, ".dll", tpaList);
// 
//     const char* propertyKeys[] = { "TRUSTED_PLATFORM_ASSEMBLIES" };
//     const char* propertyValues[] = { tpaList.c_str() };
// 
//     HRESULT hr = initializeCoreClr(
//         currentDirectory,
//         "InjectedDomain",
//         sizeof(propertyKeys) / sizeof(char*),
//         propertyKeys,
//         propertyValues,
//         &g_runtimeHost,
//         &g_domainId);
//     if (FAILED(hr))
//     {
//         wprintf(L"CoreCLR initilize failed. Error code: 0x%08x\n", hr);
//         return -2;
//     }
// 
//     managed_direct_method_ptr managedDelegate;
//     hr = createManagedDelegate(g_runtimeHost, g_domainId, "Coriander", "Coriander.CnC3EntryPoint", "Run", (void**)&managedDelegate);
//     if (FAILED(hr))
//     {
//         wprintf(L"CoreCLR create delegate failed. Error code: 0x%08x\n", hr);
//         return -3;
//     }
// 
//     managedDelegate();
// 
//     return 0;
// }
// 
// void ShutdownCoreCLR()
// {
//     if (g_runtimeHost)
//     {
//         coreclr_shutdown_ptr shutdownCoreClr = (coreclr_shutdown_ptr)GetProcAddress(g_coreCLRModule, "coreclr_shutdown");
// 
//         HRESULT hr = shutdownCoreClr(g_runtimeHost, g_domainId);
//         if (FAILED(hr))
//         {
//             wprintf(L"CoreCLR shutdown failed. Error code: 0x%08x\n", hr);
//         }
//     }
//     if (g_coreCLRModule)
//     {
//         FreeLibrary(g_coreCLRModule);
//     }
// }

typedef int (*loadcoreclrptr)();

int main(int argc, char* argv[])
{
    HMODULE module = LoadLibraryA("LookingGlass");
    loadcoreclrptr loadcoreclr = (loadcoreclrptr)GetProcAddress(module, "LoadCoreCLR");
    loadcoreclr();
    // std::cout << "Hello World!\n";
    // LoadCoreCLR();
    // if (LoadCoreCLR() == 0)
    // {
    //     RunManaged();
    // }
    // ShutdownCoreCLR();
    // FreeLibrary(module);
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
