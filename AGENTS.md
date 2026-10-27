# Thiên Kiêu — Project Rules

## Map creation

- Apply the STYLE MASTER below consistently to maps, NPCs, monsters, items, and UI. Keep the English wording exactly as written; do not translate or paraphrase it.
- Build maps from reusable, separately editable assets rather than a single static image.
- Separate background layers such as sky, clouds, distant mountains, and distant forests to create depth.
- Put trees, buildings, rocks, and other scenery behind the characters in the midground.
- Build playable terrain with multiple elevations, slopes, steps, cliffs, pits, and ledges for running and jumping.
- Keep foreground bushes, grass, branches, leaves, and other front-layer objects separate so they can be animated independently.
- Make ground, rock, grass, and other Tilemap surfaces join seamlessly, with supporting base layers and consistent dimensions and scale.
- Give ground, obstacles, cliffs, and standing surfaces accurate collisions.
- Give each map entrances, exits, and spawn points. When the player approaches an exit, show the next map's name.
- Give every NPC a unique ID, position, animation, dialogue, and suitable quest.
- Define each monster's spawn area, stats, behavior, animation, attack, and drops.
- Give every map a purpose, progression, completion conditions, and a link to the story.
- Animate trees, grass, water, NPCs, monsters, and other objects that should move.
- Integrate only approved asset versions. Record missing or awaiting-approval assets in `AI/order.md` and continue other work while they are pending.
- Store AI-generated assets in `AI/.Generated/`. Reuse existing assets and avoid duplicate or scattered generated files.
- Capture screenshots from the playable view during map QA. Save them in `AI/.ScreenShorts/`, review them from a player's perspective, and improve visual or display issues found.
- Coordinate work by role: Design handles visuals, Code handles implementation, and Manager tracks progress. Do not lock exclusive ownership of the whole project.
- Keep Unity scenes visible and editable. Store scenes under `Assets/`; do not add Git ignore rules that hide `.unity` scene files.

### STYLE MASTER

Whimsical hand-drawn storybook illustration style, stylized cartoon aesthetic, soft painterly textures with visible brush strokes, slightly rough sketchy ink linework, subtle shading, soft lighting, warm and harmonious color palette, playful but balanced proportions, simple yet charming shapes, magical and cozy atmosphere, expressive character design, crisp readable silhouettes, high detail, consistent artistic direction.
