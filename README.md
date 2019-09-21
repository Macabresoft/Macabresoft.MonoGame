# Macabre2D

A 2D game engine built on top of the MonoGame framework. This project was started on April 23, 2016.

## Supported Platforms

Currently, the Macabre2D editor only supports Windows. The framework itself is technically setup to work with any platform; however, only Windows has been tested.

## Source Code

The full source code is available here from GitHub:

 * Clone the source: `git clone https://github.com/BrettStory/Macabre2D`
 * Set up the root submodules by calling the following at the base of the repository: `git submodule update --init --recursive`
 * Run MonoGame/Protobuild.exe to generate MonoGame project files and solutions.
 * Install MonoGame for your version of Visual Studio: http://www.monogame.net/downloads/
 * Restore NuGet packages for Macabre2D.sln.
 * Open Macabre2D.sln. From here you can run the editor or example projects.

 ## Using Fonts
 
 When using custom fonts (non-system fonts) make sure to install the font for all users before using it within Macabre2D. This is a limitation of the MonoGame Content Pipeline.