using System;

namespace NSimpleOLAP.Common.Hashing
{
    internal class MurmurHash2
    {
        const UInt32 _m = 0x5bd1e995;
        const UInt64 _m2 = 0xc6a4a7935bd1e995;
        const Int32 _r = 24;
        const Int32 _r2 = 47;

        public UInt32 Hash(Byte[] data)
        {
            return Hash(data, 0xc58f1a7b);
        }
        
        public UInt32 Hash(Byte[] data, UInt32 seed)
        {
            Int32 length = data.Length;
            if (length == 0)
                return 0;
            UInt32 h = seed ^ (UInt32)length;
            Int32 currentIndex = 0;
            while (length >= 4)
            {
                UInt32 k = (UInt32)(data[currentIndex++] | data[currentIndex++] << 8 | data[currentIndex++] << 16 | data[currentIndex++] << 24);
                k *= _m;
                k ^= k >> _r;
                k *= _m;

                h *= _m;
                h ^= k;
                length -= 4;
            }
            switch (length)
            {
                case 3:
                    h ^= (UInt16)(data[currentIndex++] | data[currentIndex++] << 8);
                    h ^= (UInt32)(data[currentIndex] << 16);
                    h *= _m;
                    break;
                case 2:
                    h ^= (UInt16)(data[currentIndex++] | data[currentIndex] << 8);
                    h *= _m;
                    break;
                case 1:
                    h ^= data[currentIndex];
                    h *= _m;
                    break;
                default:
                    break;
            }

            // Do a few final mixes of the hash to ensure the last few
            // bytes are well-incorporated.

            h ^= h >> 13;
            h *= _m;
            h ^= h >> 15;

            return h;
        }
        
        
        public UInt64 Hash(Byte[] data, UInt64 seed)
        {
            Int64 length = data.Length;
            if (length == 0)
                return 0;
            UInt64 h = seed ^ (UInt64)length;
            Int64 currentIndex = 0;
            
            while (length >= 8)
            {
                UInt64 k = (UInt64)(data[currentIndex++] | data[currentIndex++] << 8 | data[currentIndex++] << 16 | data[currentIndex++] << 24
            	                   | data[currentIndex++] << 32 | data[currentIndex++] << 40 | data[currentIndex++] << 48 
            	                  | data[currentIndex++] << 56 );
                k *= _m2;
                k ^= k >> _r2;
                k *= _m2;

                h *= _m2;
                h ^= k;
                length -= 8;
            }
            switch (length)
            {
            	case 7:
                    h ^= (UInt16)(data[currentIndex++] | data[currentIndex++] << 8);
                    h ^= (UInt32)(data[currentIndex] << 16 | data[currentIndex++] << 24);
                    h ^= (UInt64)(data[currentIndex] << 32 | data[currentIndex] << 40 | data[currentIndex] << 48);
                    h *= _m2;
                    break;
            	case 6:
                    h ^= (UInt16)(data[currentIndex++] | data[currentIndex++] << 8);
                    h ^= (UInt32)(data[currentIndex] << 16 | data[currentIndex++] << 24);
                    h ^= (UInt64)(data[currentIndex] << 32 | data[currentIndex] << 40);
                    h *= _m2;
                    break;
            	case 5:
                    h ^= (UInt16)(data[currentIndex++] | data[currentIndex++] << 8);
                    h ^= (UInt32)(data[currentIndex] << 16 | data[currentIndex++] << 24);
                    h ^= (UInt64)(data[currentIndex] << 32);
                    h *= _m2;
                    break;
            	case 4:
                    h ^= (UInt16)(data[currentIndex++] | data[currentIndex++] << 8);
                    h ^= (UInt32)(data[currentIndex] << 16 | data[currentIndex++] << 24);
                    h *= _m2;
                    break;
                case 3:
                    h ^= (UInt16)(data[currentIndex++] | data[currentIndex++] << 8);
                    h ^= (UInt32)(data[currentIndex] << 16);
                    h *= _m2;
                    break;
                case 2:
                    h ^= (UInt16)(data[currentIndex++] | data[currentIndex] << 8);
                    h *= _m2;
                    break;
                case 1:
                    h ^= data[currentIndex];
                    h *= _m2;
                    break;
                default:
                    break;
            }

            // Do a few final mixes of the hash to ensure the last few
            // bytes are well-incorporated.

            h ^= h >> _r2;
            h *= _m2;
            h ^= h >> _r2;

            return h;
        }
    }
}