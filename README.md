# Raylib Rendering Backend

This project provides a [Raylib](https://www.raylib.com/) based rendering backend for the Engine framework. It contains implementations of `IRenderBackend`, `ICamera`, texture and shader loaders, and a DI bundle that wires everything together.

## Purpose

The backend allows applications built with the Engine libraries to use Raylib for drawing and resource management. It offers a simple window creation mechanism and minimal wrappers around Raylib structures so that the rest of the engine can remain framework‑agnostic.

## Setup

This repository is a .NET project targeting **net9.0** and depends on the `Raylib-cs` package. To build it locally use the regular .NET workflow:

```bash
# restore dependencies
 dotnet restore

# build the project
 dotnet build Engine.Rendering.RaylibBackend.csproj
```

If you wish to include it in another solution just add a project reference to `Engine.Rendering.RaylibBackend.csproj`.

## Usage Notes

- The `RaylibRenderingBundle` registers all services required to use the backend. Add this bundle to your DI builder to enable rendering through Raylib.
- `RaylibTextureLoader` expects Raylib to be initialised (a window must be created) before loading textures; otherwise it will throw an exception.
- `RaylibBackend` opens a window sized 800x600 when constructed and exposes methods to begin and end drawing each frame.

This project currently exposes only a minimal feature set but can be extended as needed. Refer to the source code for details.
