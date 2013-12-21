/**
Kirill Melentyev (c) 2013 
hashmap
Ўаблонный класс HashMap, выдел€ющий дополнительную пам€ть при необходимости
*/

#pragma once
#include <algorithm>
#include <functional>
#include <vector>

#define STATISTICS

/**
@detailed ’эш таблица, сама управл€ет пам€тью.
ѕараметры шаблона:
- K - тип ключа
- V - тип значени€
- Comparator - функтор, сравнивает две сущности типа K (отношение "меньше")
*/
template <class K, class V, class Comparator = std::less<K> >
class BSTMap 
{
    struct node 
    {
        K key;
        V value;
        node *left, *right;
#ifdef STATISTICS
        int _height;
#endif
        node(const K &k) {
            left = right = nullptr;
            key = k;
            value = V();
#ifdef STATISTICS
            _height = 1;
#endif      
        }
        ~node() {}
        static node* create(const K &k) {
            return new node(k);
        }
    };
    node *root;
    Comparator comp;
    node* _insert_node (node *newNode) 
    {
        node *p = root, *_prev = nullptr;
        while (p) 
        {
            _prev = p;
            if (!(comp(p->key, newNode->key) ) && !(comp(newNode->key, p->key) ) ) 
            {
                return p;
            }
            else if (comp(newNode->key, p->key) ) 
            {
                p = p->left;
            }
            else 
            {
                p = p->right;
            }
        }
        if (!_prev) 
        {
            root = newNode;
            return root;
        }
        else 
        {
            if (comp(newNode->key, _prev->key) ) 
            {
                return _prev->left = newNode;
            }
            else 
            {
                return _prev->right = newNode;
            }
        }
        return p;
    }
    int _size;
public:
    BSTMap() 
    {
        _size = 0;
        root = nullptr;
    }
    V& insert(const K &k, const V &v);
    node* find(const K &k) 
    {
        node *p = root;
        while (p) 
        {
            if (!(comp(p->key, k) ) && !(comp(k, p->key) ) ) 
            {
                return p;
            }
            else if (comp(k, p->key) ) 
            {
                p = p->left;
            }
            else 
            {
                p = p->right;
            }
        }
        return p;
    }
    void _iter (const std::function<void(node *x)> &f, node *x) 
    {
        if (!x) 
        {
            return;
        }
        _iter (f, x->left);
        f(x);
        _iter (f, x->right);
    }
    void iter(const std::function<void(const K&, V&)> &f) 
    {
        _iter([&f](node *x){ f(x->key, x->value); }, root);
    }
    V& operator[](const K &k) 
    {
        node* p;
        if (p = find(k) ) 
        {
            return p->value;
        }
        node* newNode = node::create(k);
        _size++;
        return _insert_node(newNode)->value;
    }
    unsigned int size() 
    {
        return _size;
    }
    ~BSTMap() 
    {
        
    }
};