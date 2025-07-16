# Pooling System

This folder contains a generic pooling system.
Pooling allows you to reuse objects (e.g., projectiles, enemies, visual effects) to optimize performance and reduce frequent allocations/deallocations.

## Structure

- `Pooler.cs`: Manages a pool of Gameobjects from prefabs.
- `PoolManager.cs`: Singleton centralizing and manages multiple pools.
- `IPoolable.cs`: Optional interface to be implemented to get events raised when an item is used or sent back to the pool.

## Usage

It is required to have the `PoolManager` loaded. The provided PoolManager prefab is already in the "Core" scene that is loaded at runtime.

To create a pool, just add a `Pooler` component to a GameObject and set the prefab to pool.
It will automatically register and unregister from the manager.
You have to set an ID for the pool, which is a unique identifier used to retrieve the pool later.

To use the pool, you can call `PoolManager.Instance.Spawn` method, providing the ID of the wanted pool, 
the position at witch to spawn, and optionally a parent transform to set the spawned object under a hierarchy.

When you are done with the object, call `PoolManager.Instance.Despawn` to return it to the pool.

## IPoolable Interface

To get notified when an object is spawned or despawned, you can implement the `IPoolable` interface on your pooled objects.
Each component implementing this interface will receive `OnSpawned` and `OnDespawned` events when the object is spawned or returned to the pool.