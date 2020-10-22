namespace Axle.Data.Records.Mapping.Accessors.ObjectMembers
{
    public interface IObjectMemberAccessor<in T, TMember>
    {
        TMember GetValue(T obj);
        void SetValue(T obj, TMember value);
        
        string FieldName { get; }
    }
}