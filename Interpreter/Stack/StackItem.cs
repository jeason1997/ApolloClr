using System;
using System.Runtime.InteropServices;

namespace ApolloClr
{
#if !JS
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
#endif
    public
#if !JS
        unsafe struct
#else
        class
#endif
        StackItem
    {
#if JS
        public static StackItem SPtrEmpty = null;
        public object SPtr;
#else
        public static StackItem* SPtrEmpty = null;
        public int* VPoint;
        public GCHandle Ptr;
#endif
        public StackValueType ValueType;
        public int IntValue;
        public int LValue;
        public int Index;

        public object Value
        {
            get
            {
                switch (ValueType)
                {
                    case StackValueType.i4:
                    {
                        return IntValue;
                    }
                    case StackValueType.r4:
                    {
                        return ValueFloat;
                    }
                    case StackValueType.i8:
                    {
                        return ValueLong;
                    }
                    case StackValueType.r8:
                    {
                        return ValueDouble;
                    }
                }

                return (Ptr.Target as StackObject).Object;
            }
        }

        public void SetValue(StackValueType vtype, object value)
        {
            switch (vtype)
            {
                case StackValueType.i4:
                {
                    IntValue = (int) value;
                    break;
                }
                case StackValueType.r4:
                {
                    var v = (float) value;
                    *(float*) VPoint = v;
                    break;
                }
                case StackValueType.i8:
                {
                    var v = (long) value;
                    *(long*) VPoint = v;
                    break;
                }
                case StackValueType.r8:
                {
                    var v = (double) value;
                    *(double*) VPoint = v;
                    break;
                }
            }
        
        }

        public int ValueInt
        {
            get { return *VPoint; }
        }
       
       
        public long ValueLong
        {
            get { return *(long*)VPoint; }
        }
        public float ValueFloat
        {
            get { return *(float*)VPoint; }
        }

        public double ValueDouble
        {
            get { return *(double*)VPoint; }
        }

       
        public static bool operator ==(StackItem s1, StackItem s2)
        {
            if (s1.IntValue == s2.IntValue && s1.Index == s2.Index )
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(StackItem s1, StackItem s2)
        {
            return !(s1 == s2);
        }

        public static bool operator >(StackItem s1, StackItem s2)
        {
            return s1.IntValue > s2.IntValue;
        }

        public static bool operator <(StackItem s1, StackItem s2)
        {
            return s1.IntValue < s2.IntValue;
        }

        public static bool operator >=(StackItem s1, StackItem s2)
        {
            return s1.IntValue >= s2.IntValue;
        }

        public static bool operator <=(StackItem s1, StackItem s2)
        {
            return s1.IntValue <= s2.IntValue;
        }

   
    }
}