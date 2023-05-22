#include "pch.h"
#include "winrt/AdvancedSharpAdbClient.WinRT.h"
#include "winrt/AdvancedSharpAdbClient.WinRT.DeviceCommands.h"

using namespace winrt;
using namespace Windows::Foundation;
using namespace AdvancedSharpAdbClient::WinRT;
using namespace AdvancedSharpAdbClient::WinRT::DeviceCommands;

int main()
{
    init_apartment();
    auto adbServer = AdbServer::AdbServer();
    adbServer.StartServer(L"C:\\Program Files (x86)\\Android\\android-sdk\\platform-tools\\adb.exe", true);
    auto status = adbServer.GetStatus();
    printf("%ls\n", status.ToString().c_str());
    if (status.IsRunning())
    {
        auto adbClient = AdbClient::AdbClient();
        auto devices = adbClient.GetDevices();
        printf("Devices:\n");
        for (auto device : devices)
        {
            printf("%ls\n", device.ToString().c_str());
            PackageManager packageManager = PackageManager(adbClient, device);
            auto packages = packageManager.Packages();
            printf("Packages:\n");
            for (auto package : packages)
            {
                printf("%ls\n", package.Key().c_str());
            }
        }
        adbClient.KillAdb();
    }
    system("pause");
}