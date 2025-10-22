# 🧱 Mission Demolition (Project 2 – CS382 Game Design, Development, and Technology)

## 🎯 Overview
Project is based on the Mission Demolition tutorial from Bond **Chapter 30**. The game simulates a slingshot that launches projectiles to destroy castles made of blocks. Players progress through multiple levels, each with increasing difficulty.

---

## 🧩 Features & Requirements

### ✅ Tutorial Implementation
- Followed **Mission Demolition** tutorial from Bond Chapter 30.  
- Fully functional slingshot physics with projectile launch and destruction system.

### ✅ Correct Unity Version
- **Unity Version:** `2021.3.33f1 (LTS)`  
- Stable Long-Term Support version compatible with tutorial and assets.

### ✅ Git Setup
- Project uploaded to GitHub with the official **Unity .gitignore** file to exclude library and cache files.

### ✅ Game Over Screen
- Added a **Game Over UI panel** that appears after the final level.  
- Includes a **“Play Again”** button to restart the game.  
- Controlled using `GameOverUI.cs`.

### ✅ Four Custom Levels
- Designed **4 unique castles**, each progressively more challenging.  
- Each castle uses different block layouts and physics to increase difficulty.

### ✅ Line Renderer for Slingshot
- Implemented **Line Renderer** to visually display the slingshot’s rubber band while aiming.  
- The line dynamically stretches between two posts and the projectile.

### ✅ Sound Effect
- Added a **rubber band snapping sound** when the projectile is released from the slingshot.  
- Provides satisfying audio feedback.

### ✅ Enhancement – Make the Game Cooler (2 pts)
#### 💥 Particle Effects on Impact
- Added a **particle explosion / dust cloud** effect whenever the projectile collides with a castle block.  
- Plays once per collision (looping disabled, duration ≈ 1 second).  
- Triggered via the `Projectile` script on all blocks tagged as `Castle`.  
- Makes destruction more dynamic and visually rewarding.

---

## 🧠 How to Play
1. Click and drag the projectile to aim.  
2. Release the mouse button to launch.  
3. Hit the castle blocks and try to destroy the goal structure.  
4. Progress through all 4 levels.  
5. When all levels are completed, the **Game Over** screen appears.  
6. Click **Play Again** to restart.

---
