namespace OurForum.Backend.Extensions
{
    public static partial class TypeExtensions
    {
        public static bool HasProperty(this Type obj, string propertyName)
        {
            return obj.GetProperty(propertyName) != null;
        }

        public static bool HasField(this Type obj, string propertyName)
        {
            return obj.GetField(propertyName) != null;
        }
    }
}
