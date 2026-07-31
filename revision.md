# Revision Feedback

## Overview

The submitted demo was reviewed by the team. The current implementation does not meet the expected engineering and implementation standards.

The project should be revised according to the feedback below and then resubmitted for evaluation.

---

# High Priority Issues

## 1. UI Visual Fidelity

### Top Bar
- The top bar visuals and animations do not match the reference game.
- Recreate the animations and transitions closer to the original implementation.

### Inventory
- The inventory system does not visually resemble the reference.
- Improve layout, spacing, transitions, animations, and overall presentation to closely match the reference.

### Wheel Rotation
- The wheel spinning animation appears low quality.
- Improve acceleration, deceleration, easing curves, stopping behavior, and overall polish.

---

# Code Architecture

## 2. Namespaces

### Problem
The project does not use namespaces.

### Required
Organize the project using namespaces, for example:

```text
Game.Core
Game.UI
Game.Wheel
Game.Inventory
Game.Audio
Game.Zones
Game.Utilities
```

---

## 3. State Machine

### Problem

Game flow is controlled using booleans such as:

```csharp
bool isSpinning;
```

### Required

Replace scattered booleans with a proper state machine.

Example:

```text
GameState

Idle
PreparingSpin
Spinning
RewardAnimation
Inventory
Transition
GameOver
```

Each state should own its own behavior and transitions.

---

## 4. Magic Numbers / Magic Strings

### Problem

The project contains many hardcoded values.

Examples:

```text
5
30
100
360f
2f
10f
```

### Required

Move these values into centralized constants or configuration.

Examples:

```text
GameConstants
WheelConstants
AnimationConstants
UIConstants
EconomyConstants
```

Alternatively, use ScriptableObject-based settings where appropriate.

---

## 5. SOLID Principles

The codebase requires refactoring to better follow SOLID principles.

Recommended references:

- Refactoring Guru
- Clean Code
- SOLID Design Principles

---

# Detailed Code Review

---

## 6. Reusability / Maintainability / Scalability

### Current Situation

Using ScriptableObjects for reward data is a good decision and allows data reuse.

However:

- Managers are tightly coupled through serialized concrete references.
- Reward calculation logic is duplicated.

Duplicate logic exists in:

- WheelManager.cs (around lines 151–152)
- WheelSlice.cs (around line 19)

### Problem

This violates the DRY (Don't Repeat Yourself) principle.

### Required

Extract reward calculation into a shared component.

Examples:

```text
RewardCalculator
RewardService
RewardUtility
```

---

## 7. Shared Utility Classes

### Current Situation

No shared helper or utility class exists.

Duplicated logic includes:

- Reward amount calculation
- Reward text formatting

### Required

Introduce reusable utility classes.

Examples:

```text
RewardCalculator
RewardFormatter
TextFormatter
NumberFormatter
```

---

## 8. Consistent Reward Formatting

Reward formatting is inconsistent.

Current examples:

```text
"x" + finalAmount

$"x{amount}"

$"{amount}x {rewardName}"
```

Found in:

- WheelSlice.cs (line 27)
- InventorySlotUI.cs (line 13)
- UIManager.cs (line 75)

### Required

Create a single shared formatter.

Example:

```csharp
RewardFormatter.FormatAmount(amount);

RewardFormatter.FormatReward(reward);
```

The entire project should use a single consistent formatting style.

---

# SOLID Review

---

## 9. SRP (Single Responsibility Principle)

### Positive

Managers are separated by domain:

- WheelManager
- ZoneManager
- InventoryManager
- AccountManager
- AudioManager

This is a good starting point.

### Problems

#### UIManager

The UIManager currently handles too many responsibilities:

- Button wiring
- Popup animations
- Inventory creation
- Economy logic
- Zone text updates

#### WheelManager

The ProcessReward() method currently handles:

- Reward calculation
- Economy multipliers
- UI updates
- Zone progression

### Required

Split responsibilities into focused classes.

Possible architecture:

```text
RewardProcessor

RewardPopupController

InventoryUIController

EconomyController

ZoneUIController

ButtonController
```

---

## 10. OCP (Open Closed Principle)

### Current Situation

Several systems rely on switch or if chains.

Examples:

- ZoneManager.GetZoneType()
- WheelManager.UpdateWheelVisuals()
- Reward multiplier logic
- UIManager.RefreshZoneText()

Adding a new zone requires editing multiple switch statements.

### Required

Replace conditional logic using polymorphism.

Possible patterns:

- Strategy Pattern
- State Pattern
- Abstract Zone classes
- Polymorphism

---

## 11. LSP (Liskov Substitution Principle)

The project currently contains almost no inheritance hierarchy.

No meaningful abstractions exist.

Status:

Not Applicable.

---

## 12. ISP (Interface Segregation Principle)

The project currently contains zero interfaces.

No interface-based architecture is present.

Status:

Not Applicable.

Suggested interfaces:

```text
IRewardService

IAudioService

IInventoryService

IZoneProvider

IWheelAnimator
```

---

## 13. DIP (Dependency Inversion Principle)

### Current Situation

Managers directly reference concrete MonoBehaviour classes.

Examples:

- WheelManager
- ZoneManager
- InventoryManager
- AccountManager

Audio also depends on:

```text
AudioManager.Instance
```

No abstraction layer exists.

No dependency injection is used.

### Required

Depend on interfaces instead of concrete implementations.

Examples:

```text
IRewardService

IAudioService

IZoneService

IInventoryService
```

Inject dependencies instead of referencing MonoBehaviours directly.

---

## 14. Game State System

### Current Situation

Gameplay flow is controlled by:

```csharp
bool isSpinning;
```

and interactable toggles.

Although a Zone enum exists, there is no gameplay state management.

### Required

Implement a dedicated Game State Machine.

Example:

```text
Idle

PreparingSpin

Spinning

Stopping

RewardAnimation

Inventory

Transition

GameOver
```

Each state should own its own transitions and behavior.

---

## 15. Project Structure

### Positive

- Reward data is stored using ScriptableObjects.
- No unnecessary tuple usage.

### Needs Improvement

- UIManager should be split into smaller focused components.
- Introduce interfaces between systems.
- Reduce coupling between managers.

---

## 16. Constants Library

### Current Situation

The project contains numerous hardcoded values.

Examples:

```text
5

30

2f

10f

100

360f
```

### Required

Move all constants into centralized configuration.

Examples:

```text
GameConstants.cs

WheelConstants.cs

AnimationSettings.cs

EconomySettings.cs

UISettings.cs
```

or

ScriptableObject-based configuration.

---

# Visual Improvements

The revised demo should also address the following visual issues:

- Recreate the top bar animations to closely match the reference.
- Improve inventory visuals to resemble the reference implementation.
- Increase the quality of the wheel spinning animation.
- Improve animation timing, easing, polish, and overall game feel.

---

# Overall Refactoring Goals

The revised project should focus on:

- Matching the visual quality of the reference game.
- Introducing namespaces throughout the project.
- Implementing a proper Game State Machine.
- Eliminating magic numbers and magic strings.
- Removing duplicated code (DRY).
- Creating reusable helper and utility classes.
- Centralizing reward calculations.
- Centralizing reward and text formatting.
- Splitting large classes into focused responsibilities.
- Applying SOLID principles consistently.
- Introducing interfaces and abstraction layers.
- Reducing coupling between systems.
- Improving maintainability.
- Improving scalability.
- Improving extensibility.
- Organizing all constants into centralized configuration classes or ScriptableObjects.

---

# Recommended Refactoring Order

1. Add namespaces throughout the project.
2. Implement a proper Game State Machine.
3. Remove all magic numbers and magic strings.
4. Extract RewardCalculator into a shared service.
5. Extract RewardFormatter into a shared utility.
6. Split UIManager into multiple focused controllers.
7. Refactor WheelManager so it only manages wheel behavior.
8. Introduce interfaces and dependency inversion.
9. Replace switch/if chains with Strategy or State patterns.
10. Polish animations and UI to closely match the reference game.