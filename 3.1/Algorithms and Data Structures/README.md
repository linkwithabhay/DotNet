###### .Net Core 3.1

# Algorithms and Data Structures

---

### Key Terms

- [Abstract Data Type (ADT)](https://www.google.com/search?q=abstract+data+type) : It is a mathematical model for data types, where a data type is defined by its behaviour (semantics) from the point of a user of the data, specifically in terms of possible values, possible operations on data of this type, and the behaviour of these operations.

* [Data Structure](https://www.google.com/search?q=data+structure) : It is a data organization, management and storage format that enables efficient access and modification. More precisely, a data structure is a collection of data values, the relationships among them, and the functions or operations that can be applied to the data.

---

## [Arrays](./Arrays/)

- One Dimensional Arrays

* Two Dimensional Arrays
  - Jagged Arrays

- Iterating over Arrays
  - Displaying in Console
  - Iterating using pointer address

* Arrays in memory
  - Arrays contain
    - SyncBlockIndex (4 bytes)
    - Ref. to method table (4 bytes)
    - Length (4 bytes)
  - String Arrays also contain
    - TypeHandle (4 bytes)
  - Integer Array Size: (4 + 4 + 4) = 12 bytes
  - String Array Size: (12 + 4) = 16 bytes
