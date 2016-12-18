using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTask4
{
    internal sealed class CoupleAttributeEnumerator: IEnumerator
    {
        private CoupleAttribute[] attributes;
        private int position = -1;

        internal CoupleAttributeEnumerator(Human h): this(h.GetType()) {}
        internal CoupleAttributeEnumerator(Type type)
        {
            attributes = (CoupleAttribute[])Attribute.GetCustomAttributes(type, typeof(CoupleAttribute));
        }

        public bool MoveNext()
        {
            position++;
            return (position < attributes.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public CoupleAttribute Current
        {
            get
            {
                try
                {
                    return attributes[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
