# JSON Utils

This asset is a one way bridge from JSON to Unity.
It implements many commonly used types, such as `Color`, `Gradient` etc., as well as some components, such as `Canvas`, `Text` etc.
You could even say that it does what UI Toolkit failed to do, allow users to define (UI) elements via text.

## Usage

This asset is not meant to be used on it's own, it depends on [TextUtils](https://github.com/nnra6864/UnityTextUtils).

### Submodule(recommended)

Navigate to your git repo and run:
```sh
git submodule add ../../nnra6864/NnTextUtils Assets/Modules/NnTextUtils
git submodule update --init --recursive
```

### Direct Clone

In directory of your choice run:
```sh
git clone --recursive https://www.github.com/nnra6864/UnityTextUtils
```

### Dependencies

### [Newtonsoft JSON](https://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@3.2)

Get it by using `Install package by name` in the Unity Package Manager and pasting the following `com.unity.nuget.newtonsoft-json`

