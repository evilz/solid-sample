# Step 4 : Liskov Substitution Principle

- Try resolve GetFileName LSP violation by adding a IFileLocator interface
    - Extract method GetFileName(int id) in interface  IFileLocator
    - rename DealStorage in FileDealStorage
    - implement IFileLocator in FileDealStorage
    - add constuctor to set WorkingDirectory from username value
    - in DealService add new Factory method to get a IFileLocator

- Try resolve other issues
    - extract IDealStorage
    - SQLDealStorage and FileDealStorage now implement IDealStorage
    - Interface must used id not path, we need to hide implementation