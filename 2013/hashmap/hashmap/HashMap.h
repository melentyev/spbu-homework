/**
Kirill Melentyev (c) 2013 
hashmap
Ўаблонный класс HashMap, выдел€ющий дополнительную пам€ть при необходимости
*/


#pragma once
#include <algorithm>
#include <functional>
#include <vector>
/**
@detailed ’эш таблица, сама управл€ет пам€тью.
ѕараметры шаблона:
- K - тип ключа
- V - тип значени€
- HashFunction - функтор, возвращает хэш дл€ сущности типа K
- Comparator - функтор, сравнивает две сущности типа K, возвращает true (если равны) / false (иначе)
*/
template <class K, class V, class HashFunction = AddressHash<K>, class Comparator = std::equal_to<K> >
class HashMap {
    static const unsigned int initialCapacity = 4;
    unsigned int capacity, _size, used_cells;
    struct element {
        K *keys;
        V *values;
        unsigned int count;
    };
    bool reallocating;
    element *data;
    HashFunction hash;
    Comparator comp;
    V& _insert_key(int cell, const K &newKey, bool default_constructor_call = true);
    V& _insert_list(int cell, const K &newKey, const V &newVal);
    void allocate_more(int oldCapacity);
    V& insert_key(const K &k);
public:
    HashMap();
    V& insert(const K &k, const V &v);
    V* find(const K &k, unsigned int *calculatedHash = NULL);
    void iter(std::tr1::function<void(K, V)> f);
    V& operator[](const K &k); 
    unsigned int size() {
        return _size;
    };
    ~HashMap();
};

template <class K, class V, class HashFunction, class Comparator>
HashMap<K, V, HashFunction, Comparator>::HashMap() {
    reallocating = false;
    capacity = initialCapacity;
    data = new element[capacity];
    for(unsigned int i = 0; i < capacity; i++) {
        data[i].values = NULL; 
        data[i].keys = NULL;
        data[i].count = 0; 
    }
    _size = 0;
    used_cells = 0;
}

template <class K, class V, class HashFunction, class Comparator>
V& HashMap<K, V, HashFunction, Comparator>::insert_key(const K &k) {
    V* found = find(k);
    if(!found) { 
        if (used_cells >= capacity / 2) {
            allocate_more(capacity);
        }
        unsigned int h = hash(k) % capacity;
        found = &_insert_key(h, k);
        if(!reallocating) {
            _size++;
        }
    }
    return *found;
}

template <class K, class V, class HashFunction, class Comparator>
V* HashMap<K, V, HashFunction, Comparator>::find(const K &k, unsigned int *calculatedHash = NULL) {
    element *e = &data[ (calculatedHash == NULL ? hash(k) : *calculatedHash) % capacity];
    for(unsigned int i = 0; i < e->count; i++) {
        if (comp(e->keys[i], k) ) {
            return e->values + i;
        }
    }
    return NULL;
}

template <class K, class V, class HashFunction, class Comparator>
void HashMap<K, V, HashFunction, Comparator>::allocate_more(int oldCapacity) {
    reallocating = true;
    capacity *= 2;
    element *oldData = data, *newData = new element[capacity];
    std::for_each(newData, newData + capacity, [](element &e) { 
        e.values = NULL; 
        e.keys = NULL;
        e.count = 0; 
    });
    data = newData;
    std::for_each(oldData, oldData + oldCapacity, [this](element &e) {
        for(unsigned int i = 0; i < e.count; i++) {
            insert(e.keys[i], e.values[i]);
        }
        delete[] e.values;
        delete[] e.keys;
    });
    delete oldData;
    reallocating = false;
}

template <class K, class V, class HashFunction, class Comparator>
V& HashMap<K, V, HashFunction, Comparator>::_insert_key(int cell, const K &newKey, bool default_constructor_call = true) {
    HashMap<K, V, HashFunction, Comparator>::element *e = this->data + cell;
    V *newValues = new V[e->count + 1];
    K *newKeys = new K[e->count + 1];
    for (unsigned int i = 0; i < e->count; i++) { 
        newValues[i] = e->values[i];
        newKeys[i] = e->keys[i];
    }
    delete[] e->values;
    delete[] e->keys;
    e->values = newValues;
    e->keys = newKeys;
    e->keys[e->count] = newKey;
    if (default_constructor_call && std::is_default_constructible<V>::value) {
        e->values[e->count] = V();
    }
    if (e->count == 0) {
        used_cells++;
    }
    return e->values[e->count++];
}

template <class K, class V, class HashFunction, class Comparator>
V& HashMap<K, V, HashFunction, Comparator>::_insert_list(int cell, const K &newKey, const V &newVal) {
    return (_insert_key(cell, newKey) = newVal);
}

template <class K, class V, class HashFunction, class Comparator>
V& HashMap<K, V, HashFunction, Comparator>::insert(const K &k, const V &v) {
    return (insert_key(k) = v);
}

template<class K, class V, class HashFunction, class Comparator>
void HashMap<K, V, HashFunction, Comparator>::iter(std::tr1::function<void(K, V)> f) {
    element *e;
    for(unsigned int i = 0; i < capacity; i++) {
        e = data + i;
        for(unsigned int j = 0; j < e->count; j++) {
            f(e->keys[j], e->values[j]);
        }
    }
}

template<class K, class V, class HashFunction, class Comparator>
V& HashMap<K, V, HashFunction, Comparator>::operator[](const K &k) {
    unsigned int index = hash(k) % capacity;
    element *e = data + index;
    for(unsigned int i = 0; i < e->count; i++) {
        if (comp(e->keys[i], k) ) {
            return *(e->values + i);
        }
    }
    return insert_key(k);
}

template<class K, class V, class HashFunction, class Comparator>
HashMap<K, V, HashFunction, Comparator>::~HashMap() {
    for(unsigned int i = 0; i < capacity; i++) {
        delete[] data[i].values;
        delete[] data[i].keys;
    }
    delete data;
}
