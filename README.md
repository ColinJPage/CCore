# CCore

CCore is a custom package for Unity 6 written by Colin Page. It contains utilities and common classes used across projects.

# Highlights
### 📨 VariableSO and VariableReference

Inspired by [a talk by Ryan Hipple](https://www.youtube.com/watch?v=raQ3iHhE_Kk), a VariableSO is a ScriptableObject that contains a piece of data. This value persists, can be easily referenced, and notifies subscribers when changed.
 
### 🧮 ModVariables

A ModVariable consists of a "base value" and a collection of "Modifiers" that alter the returned value. This allows multiple systems to influence the same value without interfering with one another.

### 🚩 Events and EventSOs

The Event class is built off of C# Actions and provides a friendlier interface for subscribing/unsubscribing functions.

### 🔊 SoundSO

A SoundSO represents a sound effect with parameter ranges to add variation when played (volume, pitch, spatial blend, etc.). When a SoundSO is played, SoundManager instantiates a new AudioSource to play it.
