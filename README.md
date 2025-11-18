# CookBookRecipe

Console app for creating and saving cookie recipes. Demonstrates Template Method pattern, Repository pattern, and separation of concerns for future GUI migration.

## Quick Start

```bash
# Run the app
dotnet run

# Run tests
dotnet test

# Change format: Edit Program.cs 
private const FileFormat Format = FileFormat.JSON; // or FileFormat.TXT
```

## My Key Design Decisions

**Why I built categories even just 2 in each category?** Both flours share identical instructions. SpiceIngredient same reason. Trying to make separation JUST IN CASE the ingredient are added. In real case, there are so many categories applied, has so many type of flours, sugar, spice, cream, etc.

### Repository Pattern for File Formats
```
IRecipesCatalog <- this is interface
-> TextRecipesRepository
-> JsonRecipesRepository
```

Why interface Allows switching TXT/JSON without changing business logic. Easy to add XML later - just implement interface, update factory.

### Folder Structure
```
Domain/          - Business logic (Ingredient, Recipe)
Architecture/    - File operations (Catalog)
View/            - Console I/O View, IUserInterface, Console Formatting and Messaging Prompt
Application/     - Application controller and logic
Program.cs       - Entry point 
```
## Architecture

```
Program.cs (Entry Point)
 |
 V
View (CLI / UI)
 |
 V
Application (Use Cases)
 |
 V
Infrastructure (Implementations)
 |
 V
Domain (Entities + Rules)   <- No need dependency 
```

Dependencies flow inward. Domain has no dependencies on UI or files.

## Design Patterns Used

| Pattern | Where | Why |
|---------|-------|-----|
| Template Method | Ingredient.DisplayIngredient() | Consistent format, custom instructions |
| Repository | IRecipesCatalog | Abstract storage mechanism |
| Factory | RecipesCatalogFactory | Centralize format decision |
| Composition | Recipe contains Ingredients | HAS-A, not IS-A |

## Testing

### Unit Tests cover:
- Recipe class (add ingredients, validate empty)
- Ingredient hierarchy (template method, categories work)
- Catalogs (load/save, edge cases)
- IngredientsCatalog (get by ID, invalid IDs)
### Architecture Tests Cover
- Dependency direction (Domain has zero outward dependencies)
- Constructor parameter limits (catches god classes)
- Interface over concrete dependencies (ensures testability)

## (Might be) Future Changes
**Better CLI:**
1. I think CLI must be on loop, not executing manually after finishing program
2. Clear screen makes CLI looks cleaner

## Trade-offs I Made

| Decision | Pro | Con | Verdict |
|----------|-----|-----|---------|
| Category abstractions (Flour, Spice) | Eliminates duplication | More classes |  Worth it because there are real duplication |
| Catalog interface | Flexibility, testability | Extra abstraction |  Required for TXT/JSON |
| Static IngredientsCatalog | Simple, efficient | Harder to test |  Fine - ingredients are constants and not ready to assume adding so much ingredient |
| View layer split | Display format changes shouldn't touch user I/O code | Adding more class on directory, think of each class's responsibility | I think it's worth since i apply interface for view, when GUI really come, every method responsibility not stick each others |
|Ingredient abstraction | Easy to handle repetition if ingredient list grow larger | No cons so far | I utulize more template pattern on ingredient so i can growing the list easily (easy maintainability) |
| Creating Application class `CookBookRecipeApp.cs`, for being orchestrator instead of `Program.cs` | as entry point `Program.cs` shouldn't calling so much processing, so i isolate them to one method `Run()` | No cons so far, maybe add Application directory (more class and directory | `Program.cs` ready to scale if he's clean on first place |
|Adding architecture test | I learn how to utulize arch test as gatekeeper, even im not using dedicated arch test as NetArchTest, to make sure when i scale, refactor and maintain this codebase, i walk on right rail | Adding more time to learn and code the arch test, when testing, there's much violation that i made when coding | Adding arch test keeping me to understand and concern more about architecture consistency |

## What I Learned
1. **Template Method** works best when algorithm is consistent but steps vary between subclass
2. **Abstraction** should eliminate real duplication especially on ingredient and user interface
3. **Separation of concerns** prepares for change without acrobatic change things that works before
4. **Dependency Inversion** makes codebase more flexible and maintainable
5. **Architecture Test** make sure i make architecture consistency

---
