# CCore

CCore (for "Colin Core") is a custom package for Unity 6 written by Colin Page. It contains utilities and common classes used across projects.

This package was originally created for private use. Please be aware that many included classes and functions lack documentation, and adding clarity is an ongoing process.
Anyone may view, use, modify, and distribute this package for non-commercial use as specified in the license. 

# Highlighted Systems
### 📨 VariableSO and VariableReference

Inspired by [a talk by Ryan Hipple](https://www.youtube.com/watch?v=raQ3iHhE_Kk), a VariableSO is a ScriptableObject that contains a piece of data. This value persists, can be easily referenced, and notifies subscribers when changed.
 
### 🧮 ModVariables

A ModVariable consists of a "base value" and a collection of "Modifiers" that alter the returned value. This allows multiple systems to influence the same value without interfering with one another.

### 🚩 Events and EventSOs

The Event class is built off of C# Actions and provides a friendlier interface for subscribing/unsubscribing functions.

### 🔊 SoundSO

A SoundSO is a ScriptableObject that represents a sound effect with parameter ranges to add random variation when played (volume, pitch, spatial blend, etc.). When a SoundSO is played, SoundManager instantiates a new AudioSource to play it.

### ☑️ State Machines

An abstract state machine and states are defined with the hope of being highly reusable.

# Installation
### Readonly Installation
If you only wish to use the features of this package without making changes, do the following:
1. Open the Unity project in which you want this package installed
2. Open the Package Manager (via Window > Package Manager)
3. Click the + button in the top-left and select "Install package from git URL"
4. Paste the URL of this github repository

### Github Submodule Installation
If you wish to use this package while retaining the possibility of modifying the files locally, do the following:
1. Clone this repository into the "Packages" folder of your desired Unity project

### Depenencies
This package references DOTween and Text Mesh Pro. Those assembly definitions may need to be added to this package's assembly definition.
