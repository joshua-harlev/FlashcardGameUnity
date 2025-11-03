/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * For: PA1
 * Overview:
 * ListTools.cs | Provides utility methods for working with lists.
 */
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides utility methods for working with lists.
/// </summary>
public class ListTools {
    
    /// <summary>
    /// Shuffles the elements of the specified list in random order using the Fisher-Yates algorithm.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the list.</typeparam>
    /// <param name="list">The list to be shuffled.</param>
    public static void ShuffleList<T>(ref List<T> list) {
        for (int i = list.Count - 1; i > 0; i--) {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}