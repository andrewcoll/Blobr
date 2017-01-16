# SimpleBlobStorage
A very simple wrapper for Azure blob storage. Only really suitable for cases where your data storage needs are very simple. 

Conceptually a page represents a single blob, which contains a collection of entities of one particular type. This solution loads all those entities for every call so is only suitable when your data set is small enough to be held in memory for the lifetime of your app. 
