# NativeUI-Example
This is an example code for a NativeUI mod menu for GTA5. You can use this to create your own menu.
It's a small part of code from my own mod menu (Atomic SP Beta).

It has some basic features built in to show you how to add them.







Requirements:

- Basic knowledge of C#

- Know how to work with Visual Studio

- Visual Studio 2019 or higher

- NativeUI

- ScriptHookVDotNet 3 (Version 3.1.0 is recommended)

To-do:

- Add NativeUI.dll & ScriptHookVDotNet 3.dll to the references!!! Else the menu wan't be able to compile!
  If you don't know how to do this than you are at the wrong place. It's the simpliest step of all!
  Search for it on the internet if you don't know how to do this!
  
- Go into "Properties" --> "Application" and change "Assembly name" & "Default namespace" to whatever your
  mod menu name is!
  
- Go into "Properties" --> "Application" --> "Assembly Information..." and change Title, Company, Product,
  Assembly version and File version to whatever you want. You can also change the other stuff if you want.
  
- On the top of your screen (in Visual Studio) you'll see something that says "Debug", change this to "Releas.
  And also change the box right next to that one from "Any CPU" to "X64". This should fix some warnings.
  If you don't have "X64" in that box than leave it as it is or create "X64" (leave it alone if you don't
  know what I'm talking of).

- Change the menu name to whatever you like and don't forget to change the dev name!

- Change the numbers in the menu that tell you what feature you have selected of an amount of features.
  They aren't correct because I just cut some code of the Atomic SP Beta menu and put it in the example.
  They look like this: "1/5". You can't miss them, they are next to the name of the feature and will appear
  at the bottom of the menu when a feature is selected!
  
You are free to modify and change this example to whatever you like!
