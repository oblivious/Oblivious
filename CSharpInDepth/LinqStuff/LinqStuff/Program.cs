using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqStuff
{
	class Program
	{
		static void Main(string[] args)
		{
			var numbers = new List<string>{"one", "two", "three"};
			IEnumerable<SimpleIdentifier> simpleIdentifiers = new List<SimpleIdentifier>
				{
					new SimpleIdentifier{Id = 1, Name = "tim"},
					new SimpleIdentifier{Id = 2, Name = "dave"},
					new SimpleIdentifier{Id = 3, Name = "simon"}
				};
			IEnumerable<SimpleObject> simpleObjects = new List<SimpleObject>
				{
					new SimpleObject{Name = "tim", Data = "This is one's data"},
					new SimpleObject{Name = "dave", Data = "This is two's data"},
					new SimpleObject{Name = "simon", Data = "This is three's data"},
					new SimpleObject{Name="simon", Data = "This is four's data."},
					new SimpleObject{Name = "dave", Data = "This is more data for dave"}
				};

			var query = from ident in simpleIdentifiers
			            join simple in simpleObjects
				            on ident.Name equals simple.Name
			            select new {ident.Id, ident.Name, simple.Data};

			foreach (var entry in query)
			{
				Console.WriteLine("{0}, {1}, {2}", entry.Id, entry.Name, entry.Data);
			}
			Console.WriteLine();

			var newQuery = from ident in simpleIdentifiers
			        join simple in simpleObjects
				        on ident.Name equals simple.Name
				        into groupedObjects
			        select new {ident.Id, ident.Name, Objects = groupedObjects};

			foreach (var entry in newQuery)
			{
				Console.WriteLine("Id: {0}, User: {1}", entry.Id, entry.Name);
				foreach (var data in entry.Objects)
				{
					Console.WriteLine("\tData: {0}", data.Data);
				}
			}
			Console.WriteLine();

			var query3 = from simpleObject in simpleObjects
			             where simpleObject.Name != null
			             group simpleObject by simpleObject.Name;

			foreach (var entry in query3)
			{
				Console.WriteLine("Key: {0}", entry.Key);
				foreach (var item in entry)
				{
					Console.WriteLine("\t({0})", item.Data);
				}
			}
			Console.WriteLine();
		}
	}

	internal class SimpleIdentifier
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	internal class SimpleObject
	{
		public string Name { get; set; }
		public string Data { get; set; }
	}
}
