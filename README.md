Farmer Simulator is an interactive simulation that trains aspiring Rwandan farmers in sustainable carrot cultivation (other crops to be added) — without the cost, time, or risk of working real soil.


GCGO Statement
Grand Challenge Global Objective: Zero Hunger & Quality Education

Mission Statement: To make sustainable, region-specific agricultural training accessible and affordable for Rwandan youth and aspiring farmers, equipping them with the practical knowledge to grow successful, climate-resilient crops without requiring physical land, expensive inputs, or formal apprenticeships.

Problem Context
The Challenge
Smallholder farmers in Rwanda — particularly young people entering agriculture — often lack access to structured, hands-on training. Traditional learning requires:
- Physical fieldwork that risks an entire growing season if mistakes are made
- Expensive inputs (seeds, fertilizer, water) that punish trial-and-error learning
- Geographic limitations — training centers are not equally available in Northern and Eastern Provinces, despite the regions having drastically different climates
- Mentorship gaps — experienced farmers cannot personally coach everyone who wants to learn

Why It Matters
Agriculture employs over 70% of Rwanda's workforce and is central to national food security. Yet poor farming decisions — overwatering, chemical fertilizer misuse, ignoring weed control — destroy crops every season, deepening poverty and food insecurity. A safe, free, and engaging environment where learners can experience consequences without real-world loss addresses this gap directly.

Connection to GCGO
This simulation directly supports:
- Zero Hunger — by training farmers to produce healthier, higher-yield crops through balanced soil, water, and pest management
- Quality Education — by providing an accessible, interactive learning tool that teaches real agronomy principles through gameplay

---

Simulation Overview

What It Does
Farmer Simulator is a 3D voxel-style farming simulation set in a stylized Rwandan landscape. Players choose their province (Northern or Eastern), their crop, and a time limit, then learn the realities of farming through two modes:

1. Manual Mode — Walk the field, water plants by hand, throw fertilizer and compost bags, and watch carrots grow through 8 visual stages in real-time. Make mistakes? Watch your crops die — then reset and try again.
2. Auto-Simulate Mode — Pre-configure your farming approach (water amount, fertilizer choice, compost use), hit Simulate, and watch the entire growing season play out day-by-day. Educational popups teach you exactly why each outcome happened.

Target Users
- Aspiring Rwandan farmers (ages 14+) who want hands-on practice before working real fields
- Agricultural education programs in secondary schools and youth centers
- Anyone interested in sustainable farming principles in East African climates

Key Interactions
- Watering — Click and hold to spray water from your hand to crops via a visible line renderer (the water stream)
- Fertilizing — Press G (or tap mobile button) to throw a fertilizer bag in an aimed arc. Hits the field's collision trigger to absorb nutrients.
- Composting — Press C to throw organic compost. Unlimited use, suppresses weeds, improves soil health.
- Skip Day — Fast-forward time to see consequences of your decisions accumulate.
- Harvest & Economy — Sell mature carrots for RWF, buy fertilizer bags from the in-game market.
- Reset — Clear failed crops and try again with the same settings.
- Educational Popups — Real-time guidance: every action triggers a teaching moment explaining the agronomy behind it.



Unity Mechanics Implemented

UI (User Interface)
The simulation features a layered UI built entirely with Unity's UI Toolkit and TextMeshPro:
- Main Menu with dropdowns for Province, Crop, and Time Limit
- Mode Picker screen separating Manual and Auto playthroughs
- Setup Panel with sliders and toggles for Auto-Simulate configuration
- Live Stats Panel displaying real-time moisture, fertilizer, compost, weed, and plant health percentages plus current growth stage
- Educational Popups that pause gameplay and deliver actionable advice
- Platform-aware UI that swaps PC keyboard hints for mobile touch buttons depending on the running device

Scripting
The project uses clean, modular C# architecture with each script responsible for one concern:
- MenuNavigation.cs — Manages menu flow between welcome, configuration, mode picker, and simulation views
- PlayerController.cs — Handles movement, look, throwing actions, and water raycast
- CarrotSim.cs — Stores plant state and handles collision-based fertilizer/compost reactions
- DayCycle.cs — Processes day-by-day environmental drain and growth decisions
- FailureTracker.cs — Tracks drought, flood, chemical burn, and weed-choke consecutive-day counters for branching outcomes
- AutoSim.cs — Coroutine-based automated playthrough engine
- EconomyManager.cs — Money tracking, fertilizer inventory, and market transactions
- EduPopup.cs — Modal popup system that pauses time and delivers educational content
- PlatformUI.cs — Detects mobile vs. PC and toggles appropriate control sets

Collision
Collision-based interactions drive the resource system:
- The carrot patch parent has a Box Collider with Is Trigger enabled that acts as an absorption zone
- Thrown fertilizer and compost bags have standard colliders + Rigidbody, allowing them to physically arc, fall, and contact the trigger
- OnTriggerEnter() on the carrot patch inspects the colliding object's name ("Fertilizer" or "Compost") and applies the corresponding stat changes, then destroys the bag

Raycasting
Raycasting powers the watering mechanic:
- A ray is cast from the center of the player's camera viewport every frame the player holds left-click
- Physics.Raycast checks if the ray hits a carrot patch; if so, moisture increases in real-time
- Raycasting is also used for aimed throwing — fertilizer and compost bags launch toward whatever point the camera's center ray hits, creating an intuitive aim-and-throw feel
- This satisfies the rubric requirement and enables precise, skill-based interaction

Line Renderer
A LineRenderer component renders the water stream from the player's hand to the target point in real-time:
- Position 0 is anchored to the player's hand transform
- Position 1 updates each frame to the raycast hit point
- The line uses a custom blue material with a slim width, simulating a hose stream
- When the player releases the mouse button, the line is cleared and the water audio cuts off instantly



Additional Features (Beyond Module Scope)

1. Hybrid Simulation Modes — Two completely separate gameplay loops (Manual interactive farming AND Auto-Simulate day-by-day playthrough) sharing the same plant state system. This gives the player both an action-driven and observational learning experience.

2. Province-Aware Climate System — Northern and Eastern Provinces have distinct drain rates, drought thresholds, and waterlogging risks. Tomatoes drown in the wet North; carrots wither in the dry East. The dropdown selection meaningfully changes how the simulation plays.

3. Branching Failure Outcomes — Four distinct failure paths (drought, waterlogging, chemical burn, weed choke), each with multi-day grace periods that escalate from warning popup, to health loss, to death. Players must correct course or face consequences.

4. In-Game Economy — Money (RWF), fertilizer inventory, and a harvest-to-sell cycle that rewards patient, balanced farming with profit.

5. Real-Time Educational Popups with Action Suggestions — Every warning popup names a specific corrective action (e.g., "Press C to throw a Compost bag — it kills 15% of weeds instantly").

6. Cross-Platform Adaptive UI — PlatformUI.cs automatically hides PC keyboard hints on mobile and replaces them with touch buttons, with no manual switching needed.

7. Reset Field Mechanic — Players can wipe a dead field and replant instantly, enabling iterative trial-and-error learning.

8. Audio Feedback — Looping water-stream audio that starts and stops with click input.



Instructions for Running

WebGL (PC/Mac browser):
1. Open the WebGL link in Chrome or Edge
2. Wait for assets to load
3. Use WASD or Arrow keys to move, mouse drag to look around
4. Click and hold to water, G to throw fertilizer, C to throw compost
5. Configure your farm and choose Manual or Auto Simulate

Android:
1. Download the APK to your phone
2. Enable "Install from Unknown Sources" in your phone settings
3. Install the APK
4. Open the app and use the on-screen touch controls

Running from source (Unity Editor):
1. Clone the GitHub repository
2. Open the project in Unity 6 (6000.0.74f1 or newer)
3. Open Assets/Scenes/SampleScene.unity
4. Press Play



Project Structure

Assets/
- Scenes/SampleScene.unity
- Scripts/MenuNavigation.cs
- Scripts/PlayerController.cs
- Scripts/CarrotSim.cs
- Scripts/DayCycle.cs
- Scripts/FailureTracker.cs
- Scripts/AutoSim.cs
- Scripts/EconomyManager.cs
- Scripts/HarvestHandler.cs
- Scripts/EduPopup.cs
- Scripts/HUDManager.cs
- Scripts/SimulationSetup.cs
- Scripts/PlatformUI.cs
- Prefabs/FertiliserBag.prefab
- Prefabs/Compost.prefab
- Audio/water_stream.wav