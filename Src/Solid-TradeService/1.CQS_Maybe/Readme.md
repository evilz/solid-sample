# Step 1 : Apply Command Query Segregation and Handle invariant

- Check that all queries return something ! and have no side effect
- Check that command do not return data
- Then refactor if needed
- Handle pre-condition
    - Add Guard clause in constructor that throw ArgumentNullException
    - Add Guard clause for directory to fail fast
    - Add Guard clause in methods for file to fail fast
- Handle post-condition
    - Use Maybe monad to garanted output (not null)