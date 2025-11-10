# CookBookRecipe

Console app for creating and saving cookie recipes. Demonstrates Template Method pattern, Repository pattern, and separation of concerns for future GUI migration.

## Quick Start

```bash
# Run the app
dotnet run

# Run tests
dotnet test

# Change format: Edit Program.cs line 16
private const FileFormat Format = FileFormat.JSON; // or FileFormat.TXT
```

## My Key Design Decisions

### Template Method Pattern 
```
Ingredient (abstract)
├── DisplayIngredient() ← template method
└── GetInstructions() ← hook method
    ├── FlourIngredient ← eliminates "sieve" duplication
    │   ├── WheatFlour
    │   └── CoconutFlour
    ├── SpiceIngredient ← eliminates "teaspoon" duplication
    │   ├── Cardamom
    │   └── Cinnamon
    └── Direct subclasses for unique ingredients
        ├── Butter, Chocolate, Sugar, CocoaPowder
```

**Why I built categories even just 2 in each category?** Both flours share identical instructions. SpiceIngredient same reason. Trying to make separation JUST IN CASE the ingredient are added. In real case, there are so many categories applied, has so many type of flours, sugar, spice, cream, etc.

### Repository Pattern for File Formats
```
IRecipesCatalog <- this is interface
-> TextRecipesRepository
->JsonRecipesRepository
```

Why interface Allows switching TXT/JSON without changing business logic. Easy to add XML later - just implement interface, update factory.

### Folder Structure
```
Domain/          - Business logic (Ingredient, Recipe)
DataController/  - File operations (Catalog)
View/            - Console I/O View (RecipesConsoleView)
Program.cs       - Orchestrating / assemble everything together
```
Just in case when GUI comes, i only changing view. Domain and DataAccess remain untouch.
## Architecture

```
Program.cs (user entry point)
 |
 V
View (CLI)
 |
 V
DataController (Catalog)
 |
 V
Domain (Ingredient, Recipe) <- No dependencies
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

Tests cover:
- Recipe class (add ingredients, validate empty)
- Ingredient hierarchy (template method, categories work)
- Catalogs (load/save, edge cases)
- IngredientsCatalog (get by ID, invalid IDs)

## (Might be) Future Changes

**Better CLI:**
1. I think CLI must be on loop, not executing manually after finishing program
2. Clear screen makes CLI looks cleaner

## Trade-offs I Made

| Decision | Pro | Con | Verdict |
|----------|-----|-----|---------|
| Category abstractions (Flour, Spice) | Eliminates duplication | More classes |  Worth it because there are real duplication |
| Catalog interface | Flexibility, testability | Extra abstraction |  Required for TXT/JSON |
| No IUserInterface | Simpler for make program works | Need to build later |  YAGNI - wait for possible GUI |
| Static IngredientsCatalog | Simple, efficient | Harder to test |  Fine - ingredients are constants and not ready to assume adding so much ingredient |

## What I Learned

1. **Template Method** works best when algorithm is consistent but steps vary between subclass
2. **Abstraction** should eliminate real duplication
3. **Separation of concerns** prepares for change without acrobatic change things that works before
4. **YAGNI** principle prevents complexity creep

---
