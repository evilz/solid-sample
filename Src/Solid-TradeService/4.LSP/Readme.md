# Step 4 : Liskov Substitution Principle

#### Using dapper as micro ORM

- Try Create a new SQLDealStorage by inheriting from DealStorage
- See that GetFileName method have no sens in this class and that it's a violation of the LSP
- See that path is not really the path anymore and that it's a violation of the LSP