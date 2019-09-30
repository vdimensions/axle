using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Axle.IO.Serialization;
using NUnit.Framework;

namespace Axle.Core.Tests.IO.Serialization
{
    [TestFixture]
    public class SerializerTests
    {
        [Serializable]
        public class Person : IEquatable<Person>
        {
            public bool Equals(Person other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return FirstName == other.FirstName && LastName == other.LastName && Age == other.Age;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Person)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = (FirstName != null ? FirstName.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ Age;
                    return hashCode;
                }
            }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
        }

        [Serializable]
        public class PersonWrapper : IEquatable<PersonWrapper>
        {
            [DataMember]
            private Person _person;

            public PersonWrapper(Person person)
            {
                _person = person;
            }

            public bool Equals(PersonWrapper other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Equals(_person, other._person);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((PersonWrapper) obj);
            }

            public override int GetHashCode()
            {
                return (_person != null ? _person.GetHashCode() : 0);
            }
        }


        private static void AssertDeserializedObjectIsTheSame<T>(T data, ISerializer serializer)
        {
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(data, stream);
                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                var deserialized = serializer.Deserialize(stream, typeof(T));

                Assert.AreEqual(data, deserialized, "Deserialized object is incomplete");
            }
        }

        [Test]
        public void TestBinarySerialization()
        {
            var person = new Person
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 32
            };
            var serializer = new BinarySerializer();
            AssertDeserializedObjectIsTheSame(person, serializer);
            AssertDeserializedObjectIsTheSame(new PersonWrapper(person), serializer);
        }

        [Test]
        public void TestXmlContractSerialization()
        {
            var person = new Person
            {
                FirstName = "Lili",
                LastName = "Ivanova",
                Age = int.MaxValue
            };
            var serializer = new XmlContractSerializer();
            AssertDeserializedObjectIsTheSame(person, serializer);
            AssertDeserializedObjectIsTheSame(new PersonWrapper(person), serializer);
        }

        [Test]
        public void TestJsonContractSerialization()
        {
            var person = new Person
            {
                FirstName = "Lili",
                LastName = "Ivanova",
                Age = int.MaxValue
            };
            var serializer = new JsonContractSerializer();
            AssertDeserializedObjectIsTheSame(person, serializer);
            AssertDeserializedObjectIsTheSame(new PersonWrapper(person), serializer);
        }

        [Test]
        public void TestXmlSerialization()
        {
            var person = new Person
            {
                FirstName = "Lili",
                LastName = "Ivanova",
                Age = int.MaxValue
            };
            var serializer = new XmlSerializer();
            AssertDeserializedObjectIsTheSame(person, serializer);
        }
    }
}
