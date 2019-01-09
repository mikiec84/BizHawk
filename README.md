# BizHawk

[![GitHub latest release](https://img.shields.io/github/release/TASVideos/BizHawk.svg?logo=github&style=flat)](https://github.com/TASVideos/BizHawk/releases/latest) ![Unique systems emulated | 22](https://img.shields.io/badge/Unique_systems_emulated-22-darkgreen.svg?style=flat)

A multi-system emulator written in C#. As well as quality-of-life features for casual players, it also has recording/playback and debugging tools, making it the first choice for TASers (Tool-Assisted Speedrunners).

## Core feature matrix

Most games for every system in the table will run perfectly, and every system supports fast-forward, frame advance, and rewind.

System | Play+FF/ADV/RW | Debugging | (Input) Recording
--:|:-:|:-:|:-:
Apple II | ✓ | ? | ?
Atari 2600 | ✓ | ? | ?
Atari 7800 | ✓ | ? | ?
Atari Lynx | ✓ | ? | ?
ColecoVision | ✓ | ? | ?
Commodore 64 | ✓ | ? | ?
Game Boy / GBC | ✓ | ? | ?
GBA | ✓ | ? | ?
Genesis | ✓ | ? | ?
IntelliVision | ✓ | ? | ?
Master System | ✓ | ? | ?
N64 | ✓ | ? | ?
Neo Geo Pocket | ✓ | ? | ?
NES | ✓ | ? | ?
PSX | ✓ | ? | ?
Saturn | ✓ | ? | ?
SNES | ✓ | ? | ?
TI-83 | ✓ | ? | ?
TurboGrafx-16 | ✓ | ? | ?
Virtual Boy | ✓ | ? | ?
WonderSwan | ✓ | ? | ?
ZX Spectrum | ✓ | ? | ?

EmuHawk's frontend also has a comprehensive input mapper, with keyboard and controller support (even simultaneously), hotkeys, and turbo/rapid fire.

In addition to input recording (with TAStudio), you can also record the emulated system's audio and video, either of your own gameplay or of a prerecorded movie (TAS). See the *Usage (advanced)* section below for more info about the many debugging and recording features.

Most of the frontend can be controlled with Lua scripts (only on Windows for now). These scripts can draw overlays for casual play or for RTA speedruns, change the running game to add complex cheats, assist TASers and romhackers in debugging a game, and more.

## Installing — Windows 7/8.1/10

Released binaries can be found right here on GitHub:

[![Windows | binaries](https://img.shields.io/badge/Windows-binaries-%230078D6.svg?logo=windows&logoColor=0078D6&style=popout)](https://github.com/TASVideos/BizHawk/releases/latest)

Click `BizHawk-<version>.zip` to download it. Also note the changelog, the full version of which is [on tasvideos.org](http://tasvideos.org/Bizhawk/ReleaseHistory.html). **Don't mix different versions** of BizHawk, keep each version in its own folder.

Before you start (by running `EmuHawk.exe`), make sure you have the Windows-only prerequisites installed: .NET Framework 4.6.1; Visual C++ 2010 SP1, 2012, and 2015; and Direct3D 9. If you have a few Steam games, chances are these are already installed, otherwise you can get them all at once with [this program](https://github.com/TASVideos/BizHawk-Prereqs/releases/latest).

BizHawk functions like a "portable" program, you may move or rename the folder containing `EmuHawk.exe`, even to another drive, as long as you keep all the files together and the prerequisites are installed when you go to run it.

Win7 is supported from SP1, Win8 is supported from 8.1, and Win10 is supported from 1709 "Redstone 3", following [Microsoft's support lifecycle](https://support.microsoft.com/en-us/help/13853/windows-lifecycle-fact-sheet).

## Installing — GNU+Linux and macOS

Install BizHawk with your distro's package manager. The package name is given on each button below, and some buttons are links. For the changelog, [see tasvideos.org](http://tasvideos.org/Bizhawk/ReleaseHistory.html).

[![Arch Linux (AUR) | bizhawk](https://img.shields.io/badge/Arch_Linux_(AUR)-bizhawk-%231793D1.svg?logo=arch-linux&style=popout)](https://aur.archlinux.org/packages/bizhawk)
[![CentOS | bizhawk](https://img.shields.io/badge/CentOS-bizhawk-%23941D7A.svg?logo=centos&style=popout)](https://example.com/bizhawk)
[![Debian (Launchpad) | bizhawk](https://img.shields.io/badge/Debian_(Launchpad)-bizhawk-%23A81D33.svg?logo=debian&style=popout)](https://example.com/bizhawk)
[![elementaryOS (Launchpad) | bizhawk](https://img.shields.io/badge/elementaryOS-bizhawk-%2364BAFF.svg?logo=elementary&style=popout)](https://example.com/bizhawk)
[![Fedora | bizhawk](https://img.shields.io/badge/Fedora-bizhawk-%23294172.svg?logo=fedora&style=popout)](https://example.com/bizhawk)
[![Gentoo | bizhawk](https://img.shields.io/badge/Gentoo-bizhawk-%234E4371.svg?logo=gentoo&style=popout)](https://example.com/bizhawk)
[![Hyperbola | bizhawk](https://img.shields.io/badge/Hyperbola-bizhawk-black.svg?logo=hyperbola&style=popout)](https://example.com/bizhawk)
[![Linux Mint (Launchpad) | bizhawk](https://img.shields.io/badge/Linux_Mint_(Launchpad)-bizhawk-%2387CF3E.svg?logo=linux-mint&style=popout)](https://example.com/bizhawk)
[![macOS (Homebrew) | bizhawk](https://img.shields.io/badge/macOS_(Homebrew)-bizhawk-%23999999.svg?logo=apple&style=popout)](https://example.com/bizhawk)
[![Manjaro (AUR) | bizhawk](https://img.shields.io/badge/Manjaro_(AUR)-bizhawk-%2335BF5C.svg?logo=manjaro&style=popout)](https://aur.archlinux.org/packages/bizhawk)
[![MX Linux | bizhawk](https://img.shields.io/badge/MX_Linux-bizhawk-%23506E88.svg?logo=mx-linux&style=popout)](https://example.com/bizhawk)
[![NixOS | bizhawk](https://img.shields.io/badge/NixOS-bizhawk-%234F73BC.svg?colorA=7EB5E0&logo=nixos&style=popout)](https://example.com/bizhawk)
[![openSUSE | bizhawk](https://img.shields.io/badge/openSUSE-bizhawk-%236DA741.svg?logo=opensuse&style=popout)](https://example.com/bizhawk)
[![Solus | bizhawk](https://img.shields.io/badge/Solus-bizhawk-%235294E2.svg?colorA=4C5164&logo=solus&style=popout)](https://example.com/bizhawk)
[![SteamOS (Launchpad) | bizhawk](https://img.shields.io/badge/SteamOS-bizhawk-%231B2838.svg?logo=steam&logoColor=1B2838&style=popout)](https://example.com/bizhawk)
[![Ubuntu (Launchpad) | bizhawk](https://img.shields.io/badge/Ubuntu_(Launchpad)-bizhawk-%23E95420.svg?colorA=772953&logo=ubuntu&style=popout)](https://example.com/bizhawk)

If you run `EmuHawkMono.sh` from a terminal, note that `File > Exit (Alt+F4)` doesn't terminate the process correctly, you'll need to send SIGINT (`^C`).

Is your distro not there? Released binaries can be found right here on GitHub (same download as for Windows):

[![Misc. Linux | binaries](https://img.shields.io/badge/Misc._Linux-binaries-%23FCC624.svg?logo=linux&logoColor=black&style=popout)](https://github.com/TASVideos/BizHawk/releases/latest)

If you download BizHawk this way, **don't mix different versions**, keep each version in its own folder. Run `EmuHawkMono.sh` to give Mono the library and executable paths — you can run it from anywhere, so putting it in a .desktop file is fine. If running the script doesn't start EmuHawk, you may need to edit it (if you use a terminal, it will say so in the output).

Linux distros are supported if the distributor is still supporting your version, you're using Linux 4.4/4.9/4.14/4.19 LTS or 4.20, and there are no updates available in your package manager. *Please* update and reboot.

macOS is supported from 10.11 "El Capitan" (Darwin 15.6). Apple doesn't seem to care about lifecycles, so we'll go with 6 months from the last security update.

## Building

If you want to test the latest changes without building BizHawk yourself, grab the developer build from [AppVeyor](https://ci.appveyor.com/project/zeromus/bizhawk-udexo/history). Pick the topmost one that doesn't say "Pull request", then click "Artifacts" and download `BizHawk_Developer-<datetime>-#<long hexadecimal>.zip`.

If you use GNU+Linux, there might be a `bizhawk-git` package or similar in the same repo as the main package. If it's available, installing it will automate the build process.

### GNU+Linux and macOS

*Compiling* requires MSBuild and *running* requires Mono and WINE, but **BizHawk does not run under WINE** — only the bundled libraries are required.

Building is as easy as:
```sh
git clone https://github.com/TASVideos/BizHawk.git BizHawk_master && cd BizHawk_master
# or ssh: git clone git@github.com:TASVideos/BizHawk.git BizHawk_master && cd BizHawk_master
msbuild BizHawk.sln
```

Running is even easier, just execute `EmuHawkMono.sh` in the repo's `output` folder (this folder is what gets distributed in a Release build, you can move/rename it).

If your distro isn't listed under *Installing* above, you might get an "Unknown distro" warning in the terminal, and BizHawk may not open or may show the missing dependencies dialog. You may need to add your distro to the case statement in the script, setting `libpath` to the location of `d3dx9_43.dll.so` (please do share if you get it working).

Again, if your distro isn't listed above, `libblip_buf` probably isn't in your package repos. You can easily [build it yourself](https://gitlab.com/TASVideos/libblip_buf/blob/unified/README.md).

Once built, see the *Installing* section above.

### Windows 7/8.1/10

TODO

[Compiling](http://tasvideos.org/Bizhawk/Compiling.html)

## Usage (casual)

TODO

## Usage (advanced)

TODO

[Commandline](http://tasvideos.org/Bizhawk/CommandLine.html)

[CompactDiscInfoDump](http://tasvideos.org/Bizhawk/CompactDiscInfoDump.html)

[Rerecording](http://tasvideos.org/Bizhawk/Rerecording.html)

[TAS movie file format](http://tasvideos.org/Bizhawk/TASFormat.html)

## Troubleshooting and support

TODO

[BizHawk forum](http://tasvideos.org/forum/viewforum.php?f=64)

[BizHawk wiki](http://tasvideos.org/Bizhawk.html)

[FAQ](http://tasvideos.org/Bizhawk/FAQ.html)

## Contributing

BizHawk is Open Source Software, so you're free to modify it however you please, and if you do, we invite you to share! Under the MIT license, this is *optional*, just be careful with reusing cores as some have copyleft licenses.

If you've already made changes, the best way to contribute is with a Pull Request here on GitHub. If you're looking for something to do, check the issue tracker here on GitHub:

[![GitHub open issues counter](https://img.shields.io/github/issues-raw/TASVideos/BizHawk.svg?logo=github&style=flat)](https://github.com/TASVideos/BizHawk/issues)

It's a good idea to check if anyone else is working on an issue by asking on IRC:

[![IRC | chat.freenode.net #bizhawk](https://img.shields.io/badge/IRC-chat.freenode.net_%23bizhawk-black.svg?style=flat)](ircs://chat.freenode.net:6697/bizhawk)
[![Matrix (IRC bridge) | #freenode_#bizhawk:matrix.org](https://img.shields.io/badge/Matrix_(IRC_bridge)-%23freenode__%23bizhawk:matrix.org-black.svg?logo=matrix&style=flat)](https://matrix.to/#/#freenode_#bizhawk:matrix.org)
[![freenode webchat | #bizhawk](https://img.shields.io/badge/freenode_webchat-%23bizhawk-black.svg?style=flat)](http://webchat.freenode.net/?channels=bizhawk)

And of course, open a "feature request" issue before adding features. No point making something that no-one's going to use.

## License

From the [full text](https://github.com/TASVideos/BizHawk/blob/master/LICENSE):
> This repository contains original work chiefly in c# by the BizHawk team (which is all provided under the MIT License), embedded submodules from other authors with their own licenses clearly provided, other embedded submodules from other authors WITHOUT their own licenses clearly provided, customizations by the BizHawk team to many of those submodules (which is provided under the MIT license), and compiled binary executable modules from other authors without their licenses OR their origins clearly indicated.

In short, the frontend is MIT, and past that you're on your own.
