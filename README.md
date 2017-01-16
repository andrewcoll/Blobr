# SimpleBlobStorage
A very simple wrapper for Azure blob storage. Only really suitable for cases where

- your data is small, as the entire page is loaded into memory. Querying happens in memory (Linq).
- you are not write intensive, as each write re-writes the entire page. 
