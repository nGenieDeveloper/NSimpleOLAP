using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NSimpleOLAP.Common.Utils
{
    internal class KeyStreamer
    {
        public static byte[] TransformKey<T>(KeyValuePair<T,T>[] tuples)
            where T: IComparable
        {
            int size = Marshal.SizeOf(tuples);
            byte[] b_array = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(tuples, ptr, true);
            Marshal.Copy(ptr, b_array, 0, size);
            Marshal.FreeHGlobal(ptr);

            return b_array;
        }
    }
}
