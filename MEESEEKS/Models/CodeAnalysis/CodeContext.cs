using System;
using System.Collections;
using System.Collections.Generic;

namespace MEESEEKS.Models.CodeAnalysis
{
    /// <summary>
    /// Represents the semantic context extracted from analyzed code.
    /// </summary>
    public class CodeContext : IDictionary<string, object>
    {
        private readonly Dictionary<string, object> _context = new();

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        public object this[string key]
        {
            get => _context[key];
            set => _context[key] = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets the collection of keys in the context.
        /// </summary>
        public ICollection<string> Keys => _context.Keys;

        /// <summary>
        /// Gets the collection of values in the context.
        /// </summary>
        public ICollection<object> Values => _context.Values;

        /// <summary>
        /// Gets the number of elements in the context.
        /// </summary>
        public int Count => _context.Count;

        /// <summary>
        /// Gets a value indicating whether the context is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Adds a key-value pair to the context.
        /// </summary>
        public void Add(string key, object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            _context.Add(key, value);
        }

        /// <summary>
        /// Adds a key-value pair to the context.
        /// </summary>
        public void Add(KeyValuePair<string, object> item)
        {
            if (item.Value == null) throw new ArgumentNullException(nameof(item.Value));
            ((IDictionary<string, object>)_context).Add(item);
        }

        /// <summary>
        /// Removes all elements from the context.
        /// </summary>
        public void Clear() => _context.Clear();

        /// <summary>
        /// Determines whether the context contains a specific key-value pair.
        /// </summary>
        public bool Contains(KeyValuePair<string, object> item) => ((IDictionary<string, object>)_context).Contains(item);

        /// <summary>
        /// Determines whether the context contains a specific key.
        /// </summary>
        public bool ContainsKey(string key) => _context.ContainsKey(key);

        /// <summary>
        /// Copies the elements to an array, starting at a particular index.
        /// </summary>
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) => 
            ((IDictionary<string, object>)_context).CopyTo(array, arrayIndex);

        /// <summary>
        /// Returns an enumerator that iterates through the context.
        /// </summary>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => _context.GetEnumerator();

        /// <summary>
        /// Removes the element with the specified key from the context.
        /// </summary>
        public bool Remove(string key) => _context.Remove(key);

        /// <summary>
        /// Removes the first occurrence of a specific key-value pair from the context.
        /// </summary>
        public bool Remove(KeyValuePair<string, object> item) => ((IDictionary<string, object>)_context).Remove(item);

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        public bool TryGetValue(string key, out object value) => _context.TryGetValue(key, out value!);

        /// <summary>
        /// Returns an enumerator that iterates through the context.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Implicitly converts a Dictionary to CodeContext.
        /// </summary>
        public static implicit operator CodeContext(Dictionary<string, object> dictionary)
        {
            var context = new CodeContext();
            foreach (var kvp in dictionary)
            {
                context.Add(kvp.Key, kvp.Value);
            }
            return context;
        }

        /// <summary>
        /// Explicitly converts a CodeContext to Dictionary.
        /// </summary>
        public static explicit operator Dictionary<string, object>(CodeContext context)
        {
            var dictionary = new Dictionary<string, object>();
            foreach (var kvp in context)
            {
                dictionary.Add(kvp.Key, kvp.Value);
            }
            return dictionary;
        }

        /// <summary>
        /// Gets the underlying dictionary representation of the context.
        /// </summary>
        public Dictionary<string, object> ToDictionary()
        {
            return (Dictionary<string, object>)this;
        }
    }
}
