using System;
using UnityEngine;

namespace uguimvvm
{
    /// <summary>
    /// Attribute to mark a serialized property/field as pointing to a component.
    /// </summary>
    public class ComponentReferenceAttribute : PropertyAttribute
    {

    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class MvvmDataContextAttribute : PropertyAttribute
    {

    }
}