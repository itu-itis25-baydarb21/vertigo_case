# 🎡 Spin & Win: Dynamic Reward Economy

A highly optimized, scalable, and modular "Spin the Wheel" game project developed in Unity. This project focuses on risk & reward mechanics, dynamic economy scaling, and professional UI/UX architectures suitable for modern mobile games.

## 📌 Game Overview
Players spin a wheel to earn rewards (Gold, Items, Weapons) while navigating through different zones. The risk increases as bombs are introduced, but the rewards multiply exponentially in higher zones. 

* **Bronze Zone (Standard):** Normal spins, steady reward progression.
* **Silver Zone (Safe):** No bombs. Rewards are multiplied by x2.
* **Golden Zone (Super):** High tier. Rewards are multiplied by x10!
* **Risk/Reward Mechanic:** If a bomb is hit, the player loses all accumulated loot unless they revive using Gold or by watching a Rewarded Video Ad (Mockup).

## 🚀 Key Features
* **Dynamic Economy Scaling:** Rewards are not static. Base amounts increase linearly based on the current zone level, ensuring late-game spins remain exciting.
* **Scriptable Object Architecture:** All rewards are data-driven. Adding a new item, adjusting its drop rate, or changing its zone multiplier requires zero code changes.
* **Mockup Ad Integration:** Ready-to-connect structure for Rewarded Ads (AdMob/AppLovin). Currently implemented as a functional mock button that triggers the `Revive()` flow.
* **Robust Audio System:** A Singleton `AudioManager` handles continuous looping sounds (wheel spinning), UI interactions, and win/lose states without overlapping issues.

## 🛠️ Technical Details & UI Standards
This project strictly adheres to professional mobile game UI/UX and performance guidelines:

* **Sprite Atlas Integration:** All UI sprites are packed into a single Sprite Atlas to minimize Draw Calls and GPU overhead.
* **Optimized UI Components:** `Raycast Target` and `Maskable` properties are disabled on all non-interactive Image and Text components to prevent unnecessary physics calculations.
* **Responsive Design:** Canvas is set to `Scale With Screen Size (Expand)`. Proper Anchors and Pivots are utilized to ensure flawless display across 16:9, 4:3, and ultra-wide mobile aspect ratios.
* **Event-Driven Interactions:** Button clicks and UI events are bound via code (`onClick.AddListener`) in `OnEnable`/`OnDisable` rather than relying on brittle Unity Editor UnityEvents.
* **Procedural Animations:** Unity's native Animator is bypassed in favor of **DOTween** for lightweight, high-performance UI popups, wheel rotations, and layout updates.
* **Strict Naming Conventions:** Components follow logical, specific naming structures (e.g., `ui_image_wheel_spinner`, `ui_text_zone_value`).

## 🏗️ Architecture (Manager Pattern)
The project is divided into single-responsibility managers to ensure clean, readable, and maintainable code:
* `WheelManager`: Handles DOTween rotation math, slice calculations, and reward fetching.
* `ZoneManager`: Tracks progression, zone types (Safe/Super), and triggers economy multipliers.
* `InventoryManager / AccountManager`: Safely stores persistent player data and handles Gold transactions.
* `UIManager`: The central hub for updating all TextMeshPro elements, handling popups, and user inputs.
* `AudioManager`: Singleton class for global SFX and BGM playback.

## 🎮 How to Test
1. Clone the repository.
2. Open the project in **Unity 2021.3.45f2 LTS**.
3. Ensure the UI Sprite Atlas is packed (`Edit -> Project Settings -> Editor -> Sprite Packer: Always Enabled`).
4. Open the main scene and press Play.

## PS
1. I put revive for free for testing to ease your testing phase.
2. Also if you want to use revive with the money you need to get money from the wheel and get to your main account with leave button after that you can use that money to revive.
3. Lastly, There is a script called AccountManager in the WheelManager gameobject. You can switch the Reset Save on the Start. Its simply reset the money you owned when the game stops.
