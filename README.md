# Gabe Sample Solution

TL;DR: This project is intended to showcase my general coding style.  It is intentionally simplified for brevity.  

###### Highlights
- Clean Architecture
- Service/Repository Pattern
- Dependency Injection, Inversion of Control
- Testing
- Complete Code Coverage
- KISS, DRY, SOLID Principles.
- Low Cyclical Complexity
- Readability
  - Single level indent
  - Single dot per line
  - No use of 'else' statements
- ValueTask to optimize state machine when Value is available, fallback to Task when await is needed.
- ConcurrentDictionary for thread safety, high performance (optimal blocking).

| Directory | Description |
| --- | --- |
| shared | Shared Items |
| src | Source Code Projects |
| tests | Test Projects |

