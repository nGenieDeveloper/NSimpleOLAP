using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NSimpleOLAP.Common.Hashing
{
    internal class KeyStreamer
    {
        public static IEnumerable<byte[]> TransformKeys<T>(KeyValuePair<T,T>[] tuples)
            where T: IComparable
        {
        	foreach (KeyValuePair<T,T> item in tuples)
        	{
            	yield return TransformKey<T>(item);
        	}
        }
        
        public static byte[] TransformKey<T>(KeyValuePair<T,T> tuple)
            where T: IComparable
        {
            return TransformKey(tuple);
        }
        
        private static byte[] TransformKey(object structure)
        {
        	int size = Marshal.SizeOf(structure);
            byte[] b_array = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(structure, ptr, true);
            Marshal.Copy(ptr, b_array, 0, size);
            Marshal.FreeHGlobal(ptr);
            
            return b_array;
        }
    }
}
