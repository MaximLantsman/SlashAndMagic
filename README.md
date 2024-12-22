# Hack & Slash Game

A dynamic hack and slash game where players face randomly spawning enemies using various weapons. Currently in active development.

## 🎮 Game Overview

Fight your way through waves of randomly spawning enemies using different weapons and combat strategies. Master the combat system and survive against increasingly challenging foes.

### Key Features
- Dynamic combat system with multiple weapon types
- Random enemy spawn system
- Various weapon choices with unique attack patterns
- Health system with observer pattern implementation

## 🔧 Technical Implementation

### Core Systems
- **Input System**: Custom input handling for responsive combat controls
- **State Machine**: 
  - Player states managing different combat and movement conditions
  - Enemy states controlling AI behavior and attack patterns
- **Weapon System**:
  - Scriptable Objects for flexible weapon configuration
  - Interface-based attack system for extensible combat mechanics
- **Health System**: 
  - Observer pattern implementation for health tracking
  - Event-driven damage and healing mechanics

### Architecture Overview
The game uses several design patterns to maintain clean, maintainable code:
- State Pattern for player and enemy behavior management
- Observer Pattern for health and combat events
- Strategy Pattern for weapon system implementation
- Scriptable Objects for data-driven weapon configuration

## 🚧 Development Status

This project is currently in active development. Features and systems are subject to change.

### Current Focus
- Combat system refinement
- Enemy AI improvements
- Weapon balancing
- Performance optimization
