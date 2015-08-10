# Step 2 : Single Responsability Principle

- Use specific type (fileinfo)
- Extract DealServiceLogger. (If log lib change, we now don't need to edit DealService)
- Extract Cache. (If cache use other data structure than ImmutableDictionary, we now don't need to edit DealService)
- Extract Serializer.  (If we want to serializer in Xml or Protobuf we can without editing DealService))
- Extract Storage. (If we want to store in SQL Database we can without editing DealService))

- Make deal immutable