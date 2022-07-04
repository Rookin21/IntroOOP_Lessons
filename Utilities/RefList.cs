﻿//#nullable disable
namespace Utilities;

public class RefList<T>
{
    public class Node
    {
        public T Value { get; set; }

        public Node? Prev { get; internal set; }

        public Node? Next { get; internal set; }

        internal Node(T value) => Value = value;
    }

    private int _Count;

    public Node? First { get; set; }

    public Node? Last { get; set; }

    public Node AddFirst(T value)
    {
        var node = new Node(value);

        _Count++;

        //if (ReferenceEquals(First, null))
        //if (Equals((object)First, null))
        //if (First is not { })
        if (First is null) // Если список был пуст
        {
            First = node;
            Last = node;

            return node;
        }

        // Если в списке что-то уже было, то надо изменить первый узел

        node.Next = First;
        First.Prev = node;
        First = node;

        return node;
    }

    public Node AddLast(T value)
    {
        var node = new Node(value);

        _Count++;

        if (Last is null)
        {
            Last = node;
            First = node;

            return node;
        }

        node.Prev = Last;
        Last.Next = node;
        Last = node;

        return node;
    }

    public Node AddBefore(Node Position, T value)
    {
        if (ReferenceEquals(Position, First))
            return AddFirst(value);

        var node = new Node(value);

        node.Next = Position;
        node.Prev = Position.Prev;

        _Count++;

        Position.Prev = node;
        node.Prev!.Next = node;

        return node;
    }

    public Node AddAfter(Node Position, T value)
    {
        if (ReferenceEquals(Position, Last))
            return AddLast(value);

        var node = new Node(value)
        {
            Prev = Position,
            Next = Position.Next,
        };

        _Count++;

        Position.Next = node;
        node.Next!.Prev = node;

        return node;
    }

    public T Remove(Node node)
    {
        if (ReferenceEquals(First, Last)) // в списке всего один узел
        {
            First = null;
            Last = null;
            _Count = 0;
            return node.Value;
        }

        if (ReferenceEquals(node, First))
        {
            First = node.Next;
            First!.Prev = null;
            _Count--;
            return node.Value;
        }

        if (ReferenceEquals(node, Last))
        {
            Last = node.Prev;
            Last!.Next = null;
            _Count--;
            return node.Value;
        }

        node.Prev!.Next = node.Next;
        node.Next!.Prev = node.Prev;

        node.Next = null;
        node.Prev = null;

        _Count--;
        return node.Value;
    }
}
