# Network Monitor (UPM)

## Usage

```csharp
using Astra.Network;

NetworkMonitor.OnChanged += status =>
{
    Debug.Log(status);
};