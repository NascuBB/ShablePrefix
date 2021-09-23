namespace ShablePrefics.ServerIds
{

    class Shable
    {
        public string[] PrefToId { get; set; }

    }
    //!
    static class Ext
    {
        public static T[] Append<T>(this T[] array, T item)
        {
            if (array == null)
            {
                return new T[] { item };
            }

            T[] result = new T[array.Length + 1];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = array[i];
            }

            result[array.Length] = item;
            return result;
        }
    }

}
