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
    - Create a folder Storage and move 'storage' file in

- Apply ISP on Caching
    - extract interface from DealCaching
    - Create a caching folder and move file in it

- Apply ISP on Serializer
    - extract interface from DealSerializer
    - Create a Serializer folder and move file in it

- Apply ISP on Logger
    - extract interface from DealServiceLogger
    - Create a Logger folder and move files in it

- Use interface in DealService
    - use interface everywhere
    - clean code and refactoring naming
    - remove all call to FileLocator, change query signature to return Maybe

- Try to use same method signature when you can
    - Storage.WriteDeal => Storage.Save
    - Cache.AddOrUpdate = > Cache.Save
    - Cache.GetOrAdd => Cache.Load
    - Storage.ReadDeal => Storage.Load