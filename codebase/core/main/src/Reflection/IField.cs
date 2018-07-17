namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a reflected field member.
    /// </summary>
    /// <seealso cref="System.Reflection.FieldInfo"/>
    public interface IField : IReadWriteMember, IAttributeTarget
    {
        /// <summary>
        /// Gets a value determining if the field represented by the current <see cref="IField"/> instance 
        /// is declared as read-only, i.e. it's value can be set only within the body of a constructor 
        /// or initialization expression.
        /// <para>
        /// In C# read-only fields are declared using the <c>readonly</c> keyword.
        /// </para>
        /// <para>
        /// In VisualBasic.NET read-only fields are declared using the <c>ReadOnly</c> keyword.
        /// </para>
        /// </summary>
        /// <seealso cref="System.Reflection.FieldInfo.IsInitOnly"/>
        bool IsReadOnly { get; }
    }
}
