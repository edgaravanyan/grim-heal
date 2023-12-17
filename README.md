# Unity Character State Machine with MessagePipe

## Overview

This Unity project demonstrates the implementation of a character state machine using the MessagePipe library for inter-layer communication. The project follows a clean architecture design, separating concerns into different layers such as Core, Application, and View.

## Prerequisites

- **Unity** 20XX.XX.XX or later
- **VContainer** (Unity Dependency Injection Framework)
- **MessagePipe** (Inter-layer Communication Library)
- **UniTask** (Async/Await Library for Unity)

## Installation

1. Clone the repository to your local machine.
2. Open the project in Unity.
3. Install the required dependencies (VContainer, MessagePipe, UniTask) through the package manager.

## Project Structure

The project follows a clean architecture with distinct layers:

- **Core**: Contains fundamental game-related logic such as character states, state machine, and model.
- **Application**: Manages the application-specific logic, including character controllers and animation controllers.
- **View**: Handles Unity-specific view components, like the CharacterView class.

## Usage

1. Ensure all dependencies are installed.
2. Explore the `Assets/Scripts` directory for the main project code.
3. Run the project in Unity to see the character state machine in action.

## Architecture

### State Machine

The character state machine is implemented using the `StateRunner` abstract class, providing a foundation for managing different character states. The `IState` interface defines the contract for specific state behaviors.

### MessagePipe

The MessagePipe library facilitates communication between different layers of the project. Messages, such as `CharacterAnimationMessage`, are used to signal events, and the `CharacterAnimationController` subscribes to these messages for animation control.

### Dependency Injection

VContainer is employed for dependency injection, managing the lifetime and injection of various components throughout the project. The `GameLifeTimeScope` class configures the dependencies and their lifetimes.

## License

This project is licensed under the [MIT License](https://opensource.org/license/mit/).
