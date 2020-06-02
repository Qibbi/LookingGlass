#include <stdio.h>
#define WIN32_LEAN_AND_MEAN
#include <Windows.h>
#include "loadnet.h"
#include "coreclrhost.h"
#include <string>

#define FS_SEPARATOR "\\"
#define PATH_DELIMITER ";"

typedef unsigned int uint;

// path to the usual install dir
static LPCSTR coreCLRInstallDirectory = "%programfiles(x86)%\\dotnet\\shared\\Microsoft.NETCore.App\\3.1.4";

static LPCSTR coreCLRDll = "coreclr.dll";

HMODULE LoadCoreCLR(LPCSTR directoryPath)
{
	std::string coreDllPath(directoryPath);
	coreDllPath.append(FS_SEPARATOR);
	coreDllPath.append(coreCLRDll);
	HMODULE result = LoadLibraryExA(coreDllPath.c_str(), NULL, 0);
	if (!result)
	{
		DWORD errorCode = GetLastError();
		printf("CoreCLR not loaded from %s. Error code: %d\n", coreDllPath.c_str(), errorCode);
	}
	return result;
}

typedef void (*managed_direct_method_ptr)();

HMODULE g_coreCLRModule;
void* g_runtimeHost;
uint g_domainId;
managed_direct_method_ptr g_managedMethod;

void BuildTPAList(LPCSTR directory, LPCSTR extension, std::string& tpaList)
{
	std::string searchPath(directory);
	searchPath.append(FS_SEPARATOR);
	searchPath.append("*");
	searchPath.append(extension);
	WIN32_FIND_DATAA findData;
	HANDLE fileHandle = FindFirstFileA(searchPath.c_str(), &findData);
	if (fileHandle != INVALID_HANDLE_VALUE)
	{
		do
		{
			if (strcmp(findData.cFileName, "LookingGlass.dll") != 0)
			{
				tpaList.append(directory);
				tpaList.append(FS_SEPARATOR);
				tpaList.append(findData.cFileName);
				tpaList.append(PATH_DELIMITER);
			}
		} while (FindNextFileA(fileHandle, &findData));
		FindClose(fileHandle);
	}
}

extern "C" __declspec(dllexport) int LoadCoreCLR()
{
	char currentDirectory[MAX_PATH];
	GetModuleFileNameA(NULL, currentDirectory, MAX_PATH);
	size_t idz = strnlen(currentDirectory, MAX_PATH);
	while (idz > 0 && currentDirectory[idz - 1] != '\\')
	{
		--idz;
	}
	if (idz > 0)
	{
		currentDirectory[idz - 1] = '\0';
	}

	char coreRoot[MAX_PATH];
	size_t outSize;
	getenv_s(&outSize, coreRoot, MAX_PATH, "CORE_ROOT");
	g_coreCLRModule = LoadCoreCLR(coreRoot);
	if (!g_coreCLRModule)
	{
		g_coreCLRModule = LoadCoreCLR(currentDirectory);
	}
	if (!g_coreCLRModule)
	{
		::ExpandEnvironmentStringsA(coreCLRInstallDirectory, coreRoot, MAX_PATH);
		g_coreCLRModule = LoadCoreCLR(coreRoot);
	}
	if (!g_coreCLRModule)
	{
		return -1; // CoreCLR.dll not found
	}
	coreclr_initialize_ptr initializeCoreClr = (coreclr_initialize_ptr)GetProcAddress(g_coreCLRModule, "coreclr_initialize");
	coreclr_create_delegate_ptr createManagedDelegate = (coreclr_create_delegate_ptr)GetProcAddress(g_coreCLRModule, "coreclr_create_delegate");

	std::string tpaList;
	BuildTPAList(currentDirectory, ".dll", tpaList);

	const char* propertyKeys[] = { "TRUSTED_PLATFORM_ASSEMBLIES" };
	const char* propertyValues[] = { tpaList.c_str() };

	HRESULT hr = initializeCoreClr(
		currentDirectory,
		"InjectedDomain",
		sizeof(propertyKeys) / sizeof(char*),
		propertyKeys,
		propertyValues,
		&g_runtimeHost,
		&g_domainId);
	if (FAILED(hr))
	{
		wprintf(L"CoreCLR initilize failed. Error code: 0x%08x\n", hr);
		return -2;
	}

	managed_direct_method_ptr managedDelegate;
	hr = createManagedDelegate(g_runtimeHost, g_domainId, "Coriander", "Coriander.CnC3EntryPoint", "Run", (void**)&managedDelegate);
	if (FAILED(hr))
	{
		wprintf(L"CoreCLR create delegate failed. Error code: 0x%08x\n", hr);
		return -3;
	}

	return 0;
}

void ShutdownCoreCLR()
{
	if (g_runtimeHost)
	{
		coreclr_shutdown_ptr shutdownCoreClr = (coreclr_shutdown_ptr)GetProcAddress(g_coreCLRModule, "coreclr_shutdown");

		HRESULT hr = shutdownCoreClr(g_runtimeHost, g_domainId);
		if (FAILED(hr))
		{
			wprintf(L"CoreCLR shutdown failed. Error code: 0x%08x\n", hr);
		}
	}
	if (g_coreCLRModule)
	{
		FreeLibrary(g_coreCLRModule);
	}
}