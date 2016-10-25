using System;
using System.Collections.Generic;
using FluentAssertions.Execution;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Dsl;
using Ploeh.AutoFixture.Kernel;

namespace Latsos.Test.Util
{


    namespace System
    {
        public class ReferenceEqualityComparer : EqualityComparer<Object>
        {
            public override bool Equals(object x, object y)
            {
                return ReferenceEquals(x, y);
            }
            public override int GetHashCode(object obj)
            {
                if (obj == null) return 0;
                return obj.GetHashCode();
            }
        }

        namespace ArrayExtensions
        {
            public static class ArrayExtensions
            {
                public static void ForEach(this Array array, Action<Array, int[]> action)
                {
                    if (array.LongLength == 0) return;
                    ArrayTraverse walker = new ArrayTraverse(array);
                    do action(array, walker.Position);
                    while (walker.Step());
                }
            }

            internal class ArrayTraverse
            {
                public int[] Position;
                private int[] maxLengths;

                public ArrayTraverse(Array array)
                {
                    maxLengths = new int[array.Rank];
                    for (int i = 0; i < array.Rank; ++i)
                    {
                        maxLengths[i] = array.GetLength(i) - 1;
                    }
                    Position = new int[array.Rank];
                }

                public bool Step()
                {
                    for (int i = 0; i < Position.Length; ++i)
                    {
                        if (Position[i] < maxLengths[i])
                        {
                            Position[i]++;
                            for (int j = 0; j < i; j++)
                            {
                                Position[j] = 0;
                            }
                            return true;
                        }
                    }
                    return false;
                }
            }
        }

    }
    public static class TestExtensions
    {
        
        public static T Freeze<T>(this IPostprocessComposer<T> composer, Fixture fixture)

        {
            var obj = ((ISpecimenBuilder)composer).Create<T>();
            fixture.Inject<T>(obj);
            return obj;
        }

        /// <summary>
        /// Asserts objects equality using implementation of <see cref="IEquatable{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object1"></param>
        /// <param name="object2"></param>

        public static void ShouldEqual<T>(this T object1, T object2) where T:IEquatable<T>
        {
            if (!object1.Equals(object2))
            {
                throw new AssertionFailedException($"Expected objects to be equal {object1} {object2}");
            }
        }

        /// <summary>
        /// Asserts object inequality using implementation of <see cref="IEquatable{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        public static void ShouldNotEqual<T>(this T object1, T object2) where T : IEquatable<T>
        {
            if (object1.Equals(object2))
            {
                throw new AssertionFailedException("objects are equal but they shouldn't be");
            }
        }
    }
}