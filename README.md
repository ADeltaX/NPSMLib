# NPSMLib (.NET Standard 1.1, 2.0, .NET 4.5)
### A NowPlayingSessionManager private API wrapper library.
Provides access to playback sessions throughout the system that have integrated with SystemMediaTransportControls to provide playback info and allow remote control.
For example it allows controlling the playback of Groove Music remotely.

[![nuget](https://img.shields.io/nuget/v/NPSMLib?style=for-the-badge)](https://www.nuget.org/packages/NPSMLib)

## Alternatives
[GlobalSystemMediaTransportControlsSessionManager](https://docs.microsoft.com/en-us/uwp/api/windows.media.control.globalsystemmediatransportcontrolssessionmanager) is a WinRT API, available since 17763 (Windows 10 RS5 - version 1809). GSMTC is a wrapper around NPSM.

## Supported versions:
Windows 10 1511 (10586) or newer

## Note about UWP compatibility
Make sure you have proper capability set in your app manifest, which is required since 17763, otherwise you'll get an access denied exception.
## Events "\*Changed" will not work for partial trust/AppContainer executables ("pure" UWP) but it will for full trust.

#### Required capability:
<uap7:Capability Name="globalMediaControl" />
